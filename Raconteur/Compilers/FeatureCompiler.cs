using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Raconteur.Helpers;
using Raconteur.IDE;

namespace Raconteur.Compilers
{
    public interface FeatureCompiler
    {
        void Compile(Feature Feature, FeatureItem FeatureItem);
    }

    public class FeatureCompilerClass : FeatureCompiler 
    {
        public TypeResolver TypeResolver { get; set; }

        List<string> Assemblies;

        Feature Feature { get; set; }

        public void Compile(Feature Feature, FeatureItem FeatureItem)
        {
            if (Feature is InvalidFeature) return;

            this.Feature = Feature;

            LoadAssemblies(FeatureItem);
            CompileFeature();
            CompileSteps();
        }

        void LoadAssemblies(FeatureItem FeatureItem) 
        {
            Assemblies = new List<string> {FeatureItem.Assembly};
            Assemblies.AddRange(Settings.Libraries);
        }

        void CompileSteps()
        {
            System.Console.WriteLine("CompileSteps " + Feature.StepDefinitions.Count);
            foreach (var Step in Feature.Steps)
                Step.Implementation = Feature.StepDefinitions
                    .SelectMany(l => l.GetMethods())
                    .Where(Method => Matches(Method, Step))
                    .FirstOrDefault();
        }

        bool Matches(MethodInfo Method, Step Step)
        {
            return 
                MatchesName(Method, Step) && 
                MatchesArgs(Method, Step);
        }

        bool MatchesArgs(MethodInfo Method, Step Step)
        {
            return 
                MatchesSimpleArgs(Method, Step) || 
                MatchesObjectArg(Method, Step);
        }

        bool MatchesSimpleArgs(MethodInfo Method, Step Step)
        {
            return (MatchesArgsCount(Method, Step) && MatchesArgsTypes(Method, Step));
        }

        bool MatchesName(MethodInfo Method, Step Step)
        {
            return Method.Name == Step.Name;
        }

        bool MatchesArgsCount(MethodInfo Method, Step Step)
        {
            return Method.GetParameters().Count() == Step.ArgsCount;
        }

        bool MatchesArgsTypes(MethodInfo Method, Step Step)
        {
            if (Step.ArgsCount == 0) return true;

            return Step.HasTable && !Step.Table.HasHeader ?
                Method.HasTableArg() : !Method.HasTableArg();
        }

        bool MatchesObjectArg(MethodInfo Method, Step Step)
        {
            if (!Step.HasTable || !Step.Table.HasHeader) return false;

            var Args = Method.GetParameters();
            if (Args.Length == 0) return false;

            var Type = Args[0].ParameterType;
            if (!Type.IsClass) return false;

            var ArgsNames = Type.GetProperties().Select(x => x.Name)
                .Union(Type.GetFields().Select(x => x.Name));

            return Step.Table.Header.All(h => ArgsNames.Any(a => a == h));
        }

        void CompileFeature() 
        {
            Feature.StepDefinitions = StepDefinitions;
        }

        List<Type> StepDefinitions
        {
            get 
            {
                return new List<string> { Feature.Name }
                    .Union(Feature.DeclaredStepDefinitions)
                    .Union(Settings.StepDefinitions)
                    .Select(TypeOfStepDefinitions)
                    .Where(Type => Type != null)
                    .ToList();
            }
        }

        Type TypeOfStepDefinitions(string ClassName)
        {
            return Assemblies
                .Select(Assembly => TypeResolver.TypeOf(ClassName, Assembly))
                .FirstOrDefault(Type => Type != null);
        }
    }
}
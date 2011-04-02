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

        void CompileSteps()
        {
            foreach (var Step in Feature.Steps)
                Step.Implementation = Feature.StepDefinitions
                    .SelectMany(l => l.GetMethods())
                    .Where(Method => Matches(Method, Step))
                    .FirstOrDefault();
        }

        bool Matches(MethodInfo Method, Step Step)
        {
            return Method.Name == Step.Name && 
                Method.GetParameters().Count() == Step.ArgsCount;
        }

        void CompileFeature() 
        {
            Feature.StepDefinitions = StepDefinitions;
            Feature.DefaultStepDefinitions = DefaultStepDefinitions;
        }

        void LoadAssemblies(FeatureItem FeatureItem) 
        {
            Assemblies = new List<string> {FeatureItem.Assembly};
            Assemblies.AddRange(Settings.Libraries);
        }

        Type DefaultStepDefinitions 
        { 
            get 
            {
                try { return TypeOfStepDefinitions(Feature.Name); } 
                catch { return null; }
            }
        }

        List<Type> StepDefinitions
        {
            get 
            {
                return Feature.DeclaredStepDefinitions
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
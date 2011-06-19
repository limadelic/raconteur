using System;
using System.Collections.Generic;
using System.Reflection;
using EnvDTE;
using Raconteur.Compilers;
using Raconteur.Generators;
using Raconteur.Generators.Steps;
using Raconteur.IDE;
using Raconteur.Parsers;
using Raconteur.Refactorings;

namespace Raconteur.Helpers
{
    public static class ObjectFactory
    {
        public static RaconteurGenerator NewRaconteurGenerator(FeatureItem FeatureItem)
        {
            return new RaconteurGeneratorClass
            {
                FeatureItem = FeatureItem,
                FeatureParser = NewFeatureParser,
                FeatureCompiler = NewFeatureCompiler
            };
        }

        public static FeatureParser NewFeatureParser
        {
            get 
            {
                return new FeatureParserClass
                {
                    ScenarioTokenizer = new ScenarioTokenizerClass
                    {
                        ScenarioParser = new ScenarioParserClass
                        {
                            StepParser = new StepParserClass()
                        }
                    }
                };
            }
        }

        public static FeatureCompiler NewFeatureCompiler
        {
            get 
            {
                return new FeatureCompilerClass
                {
                    TypeResolver = new TypeResolverClass(),
                    StepCompiler = new StepCompiler()
                };
            }
        }

        public static FeatureItem FeatureItemFrom(ProjectItem FeatureFile)
        {
            return new VsFeatureItem(FeatureFile);
        }

        public static CodeGenerator NewRunnerGenerator(Feature Feature)
        {
            if (Feature is InvalidFeature) return new InvalidRunnerGenerator(Feature as InvalidFeature);

            return new RunnerGenerator(Feature);
        }

        public static CodeGenerator NewStepDefinitionsGenerator(Feature Feature, string ExistingStepDefinitions)
        {
            if (Feature is InvalidFeature) return new InvalidStepDefinitionsGenerator(ExistingStepDefinitions);

            return new StepDefinitionsGenerator(Feature, ExistingStepDefinitions);
        }

        public static CodeGenerator NewStepRunnerGenerator(Step Step)
        {
            var StepRunnerGenerator = Step.IsImplemented ? 
                new StepGeneratorForImplementedStep(Step) as StepGenerator:
                new StepGeneratorForUnimplementedStep(Step);

            StepRunnerGenerator.CodeGenerator = NewStepCodeGenerator(Step, StepRunnerGenerator);

            return StepRunnerGenerator;
        }

        static CodeGenerator NewStepCodeGenerator(Step Step, StepGenerator StepRunnerGenerator)
        {
            switch (Step.Type)
            {
                case StepType.Table: return new StepWithSimpleTableGenerator(StepRunnerGenerator);
                case StepType.HeaderTable: return new StepWithHeaderTableGenerator(StepRunnerGenerator);
                case StepType.ObjectTable: return Step.Method.LastArg().IsArray ?
                    new StepWithSimpleTableGenerator(StepRunnerGenerator) as CodeGenerator :
                    new StepWithHeaderTableGenerator(StepRunnerGenerator);
                default: return new SimpleStepGenerator(StepRunnerGenerator);
            }
        }

        public static Refactor<Step> NewRenameStep(Step Step, string NewName) {
            return Object<RenameStep, Refactor<Step>>() ??
                new RenameStep(Step, NewName);
        }

        public static Refactor<MethodInfo> NewRenameMethod(MethodInfo Method, string NewName) {
            return Object<RenameMethod, Refactor<MethodInfo>>() ??
                new RenameMethod(Method, NewName);
        }

        #region Poor man IoC

        static readonly Dictionary<Type, object> Objects = new Dictionary<Type, object>();

        static TResult Object<T, TResult>() where TResult : class {
            return Objects.ContainsKey(typeof(T)) ?
                Objects[typeof(T)] as TResult : null;
        }

        public static void Return<T>(object NewObject) where T : class {
            Objects[typeof(T)] = NewObject;
        }

        public static void ReturnNew<T>() where T : class {
            if (!Objects.ContainsKey(typeof(T))) return;

            Objects.Remove(typeof(T));
        }

        #endregion
    }
}
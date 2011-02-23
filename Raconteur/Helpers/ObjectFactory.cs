using EnvDTE;
using Raconteur.Generators;
using Raconteur.IDE;
using Raconteur.Parsers;

namespace Raconteur
{
    public static class ObjectFactory
    {
        public static RaconteurGenerator NewRaconteurGenerator(FeatureItem FeatureItem)
        {
            return new RaconteurGeneratorClass
            {
                FeatureItem = FeatureItem,
                FeatureParser = NewFeatureParser,
            };
        }

        public static FeatureParser NewFeatureParser
        {
            get 
            {
                return new FeatureParserClass
                {
                    TypeResolver = new TypeResolverClass { Assembly = "Common" },
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
    }
}
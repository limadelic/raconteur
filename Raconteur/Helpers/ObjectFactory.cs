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
                RunnerGenerator = new RunnerGenerator(),
                StepDefinitionsGenerator = new StepDefinitionsGenerator(),
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

        public static FeatureItem FeatureItemFrom(ProjectItem FeatureFile)
        {
            return new VsFeatureItem(FeatureFile);
        }
    }
}
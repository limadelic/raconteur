using Raconteur.Generators;
using Raconteur.IDE;
using Raconteur.Parsers;

namespace Raconteur
{
    public static class ObjectFactory
    {
        public static RaconteurGenerator NewRaconteurGenerator(Project Project)
        {
            return new RaconteurGeneratorClass
            {
                Project = Project,
                FeatureParser = NewFeatureParser
            };
        }

        public static FeatureParser NewFeatureParser
        {
            get 
            {
                return new FeatureParserClass
                {
                    ScenarioParser = new ScenarioParserClass()
                };
            }
        }

        public static StepDefinitionsGenerator NewStepDefinitionsGenerator
        {
            get
            {
                return null;
            } 
        }
    }
}
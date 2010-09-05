using Raconteur.Generators;
using Raconteur.Parsers;

namespace Raconteur
{
    public static class ObjectFactory
    {
        public static RunnerGenerator NewRunnerGenerator
        {
            get
            {
                return new RunnerGenerator();
            }
        }

        public static RaconteurGenerator NewRaconteurGenerator(Project Project)
        {
            return new RaconteurGenerator(Project, NewFeatureParser);
        }

        public static FeatureParser NewFeatureParser
        {
            get 
            {
                return new FeatureParserClass 
                { ScenarioParser = new ScenarioParserClass() };
            }
        }
    }
}
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
                return new RunnerGenerator{ Parser = new FeatureParserClass() };
            }
        }
    }
}
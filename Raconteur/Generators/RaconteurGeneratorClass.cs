using Raconteur.IDE;
using Raconteur.Parsers;

namespace Raconteur.Generators
{
    public class RaconteurGeneratorClass : RaconteurGenerator
    {
        public Project Project { get; set; }
        public FeatureParser FeatureParser { get; set; }
        public RunnerGenerator RunnerGenerator { get; set; }

        public string Generate(string FeatureFilePath, string Content)
        {
            var FeatureFile = new FeatureFile(FeatureFilePath){Content = Content};
            
            var Feature = FeatureParser.FeatureFrom(FeatureFile, Project);

            return RunnerGenerator.RunnerFor(Feature);
        }
    }
}
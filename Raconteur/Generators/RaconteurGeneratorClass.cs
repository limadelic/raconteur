using Raconteur.IDE;
using Raconteur.Parsers;

namespace Raconteur.Generators
{
    public class RaconteurGeneratorClass : RaconteurGenerator
    {
        public Project Project { get; set; }
        public FeatureParser FeatureParser { get; set; }
        public RunnerGenerator RunnerGenerator { get; set; }
        public StepDefinitionsGenerator StepDefinitionsGenerator { get; set; }

        public string Generate(string FeatureFilePath, string Content)
        {
            var Feature = FeatureFrom(FeatureFilePath, Content);

            var Runner = RunnerGenerator.RunnerFor(Feature);
            var StepDefinitions = StepDefinitionsGenerator.StepDefinitionsFor(Feature);

            if (!Project.ContainsStepDefinitions)
                Project.AddStepDefinitions(StepDefinitions);

            return Runner;
        }

        Feature FeatureFrom(string FeatureFilePath, string Content)
        {
            var FeatureFile = new FeatureFile(FeatureFilePath){Content = Content};
            
            return FeatureParser.FeatureFrom(FeatureFile, Project);
        }
    }
}
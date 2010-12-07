using Raconteur.IDE;
using Raconteur.Parsers;

namespace Raconteur.Generators
{
    public class RaconteurGeneratorClass : RaconteurGenerator
    {
        public FeatureItem FeatureItem { get; set; }
        public FeatureParser FeatureParser { get; set; }
        public RunnerGenerator RunnerGenerator { get; set; }
        public StepDefinitionsGenerator StepDefinitionsGenerator { get; set; }

        public string Generate(string FeatureFilePath, string Content)
        {
            Project.LoadFrom(FeatureItem);

            var Feature = FeatureFrom(FeatureFilePath, Content);

            var StepDefinitions = ObjectFactory.NewStepDefinitionsGenerator
            (
                Feature, FeatureItem.ExistingStepDefinitions
            ).Code;

            FeatureItem.AddStepDefinitions(StepDefinitions);

            return ObjectFactory.NewRunnerGenerator(Feature).Code;
        }

        Feature FeatureFrom(string FeatureFilePath, string Content)
        {
            var FeatureFile = new FeatureFile(FeatureFilePath){Content = Content};
            
            return FeatureParser.FeatureFrom(FeatureFile, FeatureItem);
        }
    }
}
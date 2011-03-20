using Raconteur.Compilers;
using Raconteur.Helpers;
using Raconteur.IDE;
using Raconteur.Parsers;

namespace Raconteur.Generators
{
    public interface RaconteurGenerator
    {
        string Generate(string FeatureFilePath, string Content);
    }

    public class RaconteurGeneratorClass : RaconteurGenerator
    {
        public FeatureItem FeatureItem { get; set; }
        public FeatureParser FeatureParser { get; set; }
        public FeatureCompiler FeatureCompiler { get; set; }
        public RunnerGenerator RunnerGenerator { get; set; }
        public StepDefinitionsGenerator StepDefinitionsGenerator { get; set; }

        public string Generate(string FeatureFilePath, string Content)
        {
            Project.LoadFrom(FeatureItem);

            var FeatureFile = new FeatureFile(FeatureFilePath){Content = Content};

            var Feature = FeatureParser.FeatureFrom(FeatureFile, FeatureItem);

            FeatureCompiler.Compile(Feature, FeatureItem);

            GenerateStepDefinitions(Feature);

            return ObjectFactory.NewRunnerGenerator(Feature).Code;
        }

        // Tech Debt: this is side effect to generate
        void GenerateStepDefinitions(Feature Feature) 
        {
            var StepDefinitions = ObjectFactory.NewStepDefinitionsGenerator
            (
                Feature, FeatureItem.ExistingStepDefinitions
            ).Code;

            FeatureItem.AddStepDefinitions(StepDefinitions);
        }
    }
}
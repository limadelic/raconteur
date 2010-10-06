﻿using Raconteur.IDE;
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
            var Feature = FeatureFrom(FeatureFilePath, Content);

            var StepDefinitions = StepDefinitionsGenerator
                .StepDefinitionsFor(Feature, FeatureItem.ExistingStepDefinitions);

            FeatureItem.AddStepDefinitions(StepDefinitions);

            return new RunnerGenerator(Feature).Code;
        }

        Feature FeatureFrom(string FeatureFilePath, string Content)
        {
            var FeatureFile = new FeatureFile(FeatureFilePath){Content = Content};
            
            return FeatureParser.FeatureFrom(FeatureFile, FeatureItem);
        }
    }
}
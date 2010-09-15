using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Raconteur.IDE;
using Raconteur.IDEIntegration;
using Specs;


namespace Features 
{
    public partial class GenerateStepDefinitionsFile 
    {
        RaconteurSingleFileGenerator Generator;

        FeatureItem FeatureItem;

        const string StepDefinitions = Actors.StepDefinitionsForFeatureWithOneScenario;

        [TestInitialize]
        public void SetUp()
        {
            FeatureItem = Substitute.For<FeatureItem>();

            Generator = new RaconteurSingleFileGenerator
            {
                FeatureItem = FeatureItem, 
                CodeFilePath = "Feature"
            };
        }

        public void When_a_Feature_is_declared_for_the_first_time()
        {
            Generator.GenerateCode(Actors.FeatureWithOneScenario);
        }

        public void The_StepDefinitions_file_should_be_created()
        {
            FeatureItem.Received().AddStepDefinitions(StepDefinitions);
        }
    }
}

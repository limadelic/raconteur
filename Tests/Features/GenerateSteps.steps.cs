using Common;
using FluentSpec;
using MbUnit.Framework;
using Raconteur;
using Raconteur.IDE;
using Raconteur.IDEIntegration;


namespace Features 
{
    public partial class GenerateStepDefinitionsFile 
    {
        RaconteurSingleFileGenerator Generator;

        FeatureItem FeatureItem;

        const string StepDefinitions = Actors.StepDefinitionsForFeatureWithOneScenario;

        [SetUp]
        public void SetUp()
        {
            FeatureItem = Create.TestObjectFor<FeatureItem>();

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
            FeatureItem.Should().AddStepDefinitions(StepDefinitions);
        }
    }
}

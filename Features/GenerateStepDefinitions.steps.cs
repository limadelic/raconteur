using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Raconteur.IDE;
using Raconteur.IDEIntegration;
using Specs;


namespace Features 
{
    public partial class GenerateStepDefinitions 
    {
        RaconteurSingleFileGenerator Generator;

        Project Project;

        const string StepDefinitions = Actors.StepDefinitionsForFeatureWithOneScenario;

        [TestInitialize]
        public void SetUp()
        {
            Project = Substitute.For<Project>();

            Generator = new RaconteurSingleFileGenerator
            {
                Project = Project, 
                CodeFilePath = "Feature"
            };
        }

        public void When_a_Feature_is_declared_for_the_first_time()
        {
            Generator.GenerateCode(Actors.FeatureWithOneScenario);
        }

        public void The_StepDefinitions_file_should_be_created()
        {
            Project.Received().AddStepDefinitions("Feature", StepDefinitions);
        }

        void When_the_Feature_file_is_updated()
        {
            Project.ContainsStepDefinitions("Feature").Returns(true);
            Generator.GenerateCode(Actors.FeatureWithOneScenario);
        }

        void The_StepDefinitions_file_should_not_be_recreated()
        {
            Project.DidNotReceive().AddStepDefinitions("Feature", StepDefinitions);
        }
    }
}

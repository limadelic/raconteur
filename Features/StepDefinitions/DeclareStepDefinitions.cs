using NSubstitute;
using Raconteur.IDE;
using Raconteur.IDEIntegration;
using Specs;

namespace Features.StepDefinitions
{
    public class DeclareStepDefinitions
    {
        readonly RaconteurSingleFileGenerator Generator;

        readonly Project Project;

        const string StepDefinitions = Actors.StepDefinitionsForFeatureWithOneScenario;

        public DeclareStepDefinitions()
        {
            Project = Substitute.For<Project>();
            Generator = new RaconteurSingleFileGenerator(Project);
        }

        public void When_a_Feature_is_declared_for_the_first_time()
        {
            Generator.CodeFilePath = "Feature";
            Generator.GenerateCode(Actors.FeatureWithOneScenario);
        }

        public void The_StepDefinitions_file_should_be_created()
        {
            Project.Received().AddStepDefinitions("Feature", StepDefinitions);
        }

        public void and_should_have_a_method_per_Step()
        {
        }
    }
}
using FluentSpec;
using Raconteur;
using Raconteur.Generators;

namespace Features.StepDefinitions
{
    public class GenerateFeatureRunner
    {
        protected string Runner;

        public void When_the_Runner_for_a_Feature_is_generated()
        {
            var FeatureFile = new FeatureFile
            {
                Name = "RaconteurFeature1",
                Namespace = "Features",
                Content = "Feature: Feature Name",
            };

            var RunnerGenerator = ObjectFactory.NewRunnerGenerator;

            Runner = RunnerGenerator.RunnerFor(FeatureFile);
        }

        public void Then_it_should_be_a_TestClass()
        {
            Runner.ShouldContain("[TestClass]");
        }

        public void And_it_should_be_named_FeatureFileNameRunner()
        {
            Runner.ShouldContain("public class RaconteurFeature1Runner");
        }

        public void And_it_should_be_on_the_Feature_Namespace()
        {
            Runner.ShouldContain("namespace Features");
        }

        public void Then_it_should_generate_a_class_reference_named_FeatureName_under_StepDefinitions()
        {
            Runner.ShouldContain("StepDefinitions.FeatureName Steps " +
                "= new StepDefinitions.FeatureName();");
        }
    }
}
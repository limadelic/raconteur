using FluentSpec;
using Raconteur;
using Raconteur.Generators;
using Raconteur.IDE;

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
                Content = "Feature: Feature Name",
            };

            var Project = new VsProject
            {
                DefaultNamespace = "Features",
            };

            var Parser = ObjectFactory.NewFeatureParser;

            var RunnerGenerator = new RunnerGenerator();

            var Feature = Parser.FeatureFrom(FeatureFile, Project);

            Runner = RunnerGenerator.RunnerFor(Feature);
        }

        public void Then_it_should_be_a_TestClass()
        {
            Runner.ShouldContain("[TestClass]");
        }

        public void And_it_should_be_named_FeatureFileName()
        {
            Runner.ShouldContain("public partial class RaconteurFeature1");
        }

        public void And_it_should_be_on_the_Feature_Namespace()
        {
            Runner.ShouldContain("namespace Features");
        }
    }
}
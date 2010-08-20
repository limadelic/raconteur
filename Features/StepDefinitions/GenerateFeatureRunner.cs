using FluentSpec;
using Raconteur;
using Raconteur.Generators;
using TechTalk.SpecFlow;

namespace Features.StepDefinitions
{
    [Binding]
    public class GenerateFeatureRunner
    {
        protected string Runner;

        [When(@"the Runner for a Feature is generated")]
        public void WhenTheRunnerForAFeatureIsGenerated()
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

        [Then(@"it should be a TestClass")]
        public void ThenItShouldBeATestClass()
        {
            Runner.ShouldContain("[TestClass]");
        }

        [Then(@"it should be named FeatureFileNameRunner")]
        public void ThenItShouldBeNamedFeatureFileNameRunner()
        {
            Runner.ShouldContain("public class RaconteurFeature1Runner");
        }

        [Then(@"it should be on the Feature Namespace")]
        public void ThenItShouldBeOnTheFeatureNamespace()
        {
            Runner.ShouldContain("namespace Features");
        }

        [Then(@"it should generate a class reference named FeatureName under StepDefinitions")]
        public void ThenItShouldGenerateAClassReferenceNamedFeatureNameUnderStepDefinitions()
        {
            Runner.ShouldContain("StepDefinitions.FeatureName Steps " +
                "= new StepDefinitions.FeatureName();");
        }
    }
}
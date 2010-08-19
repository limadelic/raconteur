using FluentSpec;
using Raconteur;
using TechTalk.SpecFlow;

namespace Features.StepDefinitions
{
    [Binding]
    public class GenerateFeatureRunner
    {
        string Runner;

        [When(@"the Runner for a Feature is generated")]
        public void WhenTheRunnerForAFeatureIsGenerated()
        {
            var FeatureFile = new FeatureFile
            {
                Name = "RaconteurFeature1",
                Namespace = "Features"
            };

            var RunnerGenerator = new RunnerGenerator();

            Runner = RunnerGenerator.RunnerFor(FeatureFile);
        }

        [Then(@"it should be a TestClass")]
        public void ThenItShouldBeATestClass()
        {
            Runner.ShouldContain("[TestClass]");
        }

        [Then(@"it should be named Feature File \+ Runner")]
        public void ThenItShouldBeNamedFeatureFileRunner()
        {
            Runner.ShouldContain("public class RaconteurFeature1Runner");
        }

        [Then(@"it should be on the Feature Namespace")]
        public void ThenItShouldBeOnTheFeatureNamespace()
        {
            Runner.ShouldContain("namespace Features");
        }
    }
}
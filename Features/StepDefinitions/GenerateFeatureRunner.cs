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
                Name = "RaconteurFeature1.feature"
            };

            var RunnerGenerator = new RunnerGenerator();

            Runner = RunnerGenerator.RunnerFrom(FeatureFile);
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
    }
}
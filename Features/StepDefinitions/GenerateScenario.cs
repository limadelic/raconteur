using FluentSpec;
using Raconteur;
using Raconteur.Generators;
using TechTalk.SpecFlow;

namespace Features.StepDefinitions
{
    [Binding]
    public class GenerateScenario
    {
        private string Runner;

        [When(@"the Scenario for a feature is generated")]
        public void TheScenarioForAFeatureIsGenerated()
        {
            var featureFile = new FeatureFile
            {
                Content =  
                    @"Feature: Feature Name
                        In order to do something
                        Another thing should happen

                    Scenario: Scenario Name"
            };

            Runner = ObjectFactory.NewRunnerGenerator
                .RunnerFor(featureFile);
        }

        [Then(@"it should be a Test Method")]
        public void ThenItShouldBeATestMethod()
        {
            Runner.ShouldContain("[TestMethod]");
        }

        [Then(@"it should be named After the Scenario name")]
        public void ThenItShouldBeNamedAfterTheScenarioName()
        {
            Runner.ShouldContain(@"public void ScenarioName()");
        }
    }
}
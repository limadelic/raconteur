using FluentSpec;
using Raconteur;
using Raconteur.Generators;
using TechTalk.SpecFlow;

namespace Features.StepDefinitions
{
    [Binding]
    public class GenerateScenario
    {
        private string runner;

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

            var runnerGenerator = ObjectFactory.NewRunnerGenerator;
            runner = runnerGenerator.RunnerFor(featureFile);
        }

        [Then(@"it should be a Test Method")]
        public void ThenItShouldBeATestMethod()
        {
            runner.ShouldContain("[TestMethod]");
        }

        [Then(@"it should be named After the Scenario name")]
        public void ThenItShouldBeNamedAfterTheScenarioName()
        {
            runner.ShouldContain(@"public void ScenarioName()
        {

        }");
        }
    }
}
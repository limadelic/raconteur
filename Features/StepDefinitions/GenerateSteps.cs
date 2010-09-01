using System.Linq;
using FluentSpec;
using Raconteur;
using Raconteur.Generators;
using TechTalk.SpecFlow;

namespace Features.StepDefinitions
{
    [Binding]
    public class GenerateSteps
    {
        private string runner;

        [When(@"a Scenario with steps is generated")]
        public void WhenAScenarioWithStepsIsGenerated()
        {
            var featureFile = new FeatureFile
            {
                Content = 
@"Feature: Feature Name

Scenario: Scenario Name
    If something happens
    Then something else should happen
    And another thing too"
            };

            var runnerGenerator = ObjectFactory.NewRunnerGenerator;
            runner = runnerGenerator.RunnerFor(featureFile);
        }

        [Then(@"it should call each step in order")]
        public void ThenItShouldCallEachStepInOrder()
        {
            runner.ShouldContainInOrder(
                "Steps.If_something_happens();",
                "Steps.Then_something_else_should_happen();",
                "Steps.And_another_thing_too();");
        }
    }

    static class ShouldAssertions
    {
        public static void ShouldContainInOrder(this string Whole, params string[] Parts)
        {
            var remainder = Whole;
            Parts.ToList().ForEach(Part =>
            {
                remainder.ShouldContain(Part);
                remainder = remainder.Substring(remainder.IndexOf(Part) + Part.Length);
            });
        }
    }
}
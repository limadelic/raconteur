using Raconteur;
using Raconteur.Generators;

namespace Features.StepDefinitions
{
    public class GenerateSteps
    {
        private string Runner;

        public void When_a_Scenario_with_steps_is_generated()
        {
            var featureFile = new FeatureFile
            {
                Content = 
                @"
                    Feature: Feature Name

                    Scenario: Scenario Name
                        If something happens
                        Then something else should happen
                        And another thing too
                "
            };

            var Parser = ObjectFactory.NewFeatureParser;
            
            Runner = new RunnerGenerator().RunnerFor(
                Parser.FeatureFrom(featureFile, new Project()));
        }

        public void it_should_call_each_step_in_order() 
        {
            Runner.ShouldContainInOrder(
                "If_something_happens();",
                "Then_something_else_should_happen();",
                "And_another_thing_too();");
        }
    }
}
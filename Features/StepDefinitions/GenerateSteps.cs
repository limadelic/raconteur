using Raconteur;
using Raconteur.Generators;

namespace Features.StepDefinitions
{
    public class GenerateSteps
    {
        string Runner;

        RunnerGenerator RunnerGenerator;

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
                        If something happens
                        And another thing too
                "
            };

            var Parser = ObjectFactory.NewFeatureParser;

            RunnerGenerator = new RunnerGenerator(
                Parser.FeatureFrom(featureFile, new Project()));

            Runner = RunnerGenerator.Runner;
        }

        public void it_should_call_each_step_in_order() 
        {
            Runner.ShouldContainInOrder(
                "If_something_happens();",
                "Then_something_else_should_happen();",
                "If_something_happens();",
                "And_another_thing_too();");
        }

        public void and_declare_the_StepDefinition()
        {
            Runner.ShouldContainInOrder(
                RunnerGenerator.DeclareStep("If_something_happens"),
                RunnerGenerator.DeclareStep("Then_something_else_should_happen"),
                RunnerGenerator.DeclareStep("And_another_thing_too"));
        }
    }
}
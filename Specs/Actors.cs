namespace Specs
{
    public class Actors
    {
        public const string FeatureWithThreeScenarios = 
        @"
            Feature: has three scenarios
            Scenario: First Scenario
            Scenario: Second Scenario
            Scenario: Third Scenario
        ";

        public const string StepDefinitionsForFeatureWithOneScenario = "";

        public const string FeatureWithOneScenario = 
        @"
            Feature: has one scenario
            Scenario: One Scenario
        ";

        public const string FeatureWithNoScenarios = @"Feature: Feature Name";

        public const string ScenarioWithNoSteps = @"Scenario: Scenario Name";

        public const string ScenarioWithOneStep = 
        @"
            Scenario: has one step
            This is a step
        ";

        public const string ScenarioWithThreeSteps = 
        @"
            Scenario: has three steps
            This is one step
            This is another step
            This is the last step
        ";

        public const string DefinitionCode =
        @"
            namespace Feature.StepDefinitions
            {
                public class Definition
                {

                }
            }
        ";
    }
}
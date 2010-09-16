using Raconteur;

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

        public const string StepDefinitionsForFeatureWithOneScenario = 
        @"
namespace  
{
    public partial class HasOneScenario 
    {
    }
}
";

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

        public readonly static Feature Feature = new Feature
        {
            Name = "Name",
            FileName = "File Name",
            Namespace = "Features",
            Scenarios =
                {
                    new Scenario
                    {
                        Name = "Scenario 1",
                        Steps = { "Unique step", "Repeated step" }
                    },                            
                    new Scenario
                    {
                        Name = "Scenario 2",
                        Steps = { "Repeated step", "Another unique step" }
                    },                            
                }
        };

        public static class DefinedFeature
        {
            public const string FeatureDefintion =
            @"
                Feature: Feature Name
                Scenario: has one step
                This is a step
            ";

            public const string StepsDefintion =
            @"
                public partial class FeatureName 
                {
                    public void This_is_one_step()
                    {
                        var thing = string.Empty;
                    }
                }
            ";
        }
    }
}
using System;
using System.Collections.Generic;
using NSubstitute;
using Raconteur;
using Raconteur.IDE;

namespace Common
{
    public class Actors
    {
        public static FeatureItem FeatureItem(string Assembly, string DefaultNamespace = null)
        {
            var FeatureItem = Substitute.For<FeatureItem>();
            FeatureItem.DefaultNamespace = DefaultNamespace ?? Assembly;
            FeatureItem.Assembly.Returns(Assembly);
            return FeatureItem;
        }

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

        public static readonly List<string> ScenarioWithNoSteps = new List<string>
        {
            "Scenario: Scenario Name"
        };

        public static readonly List<string> ScenarioWithTwoSteps = new List<string>
        {
            "Scenario: has one step",
            "This is a step",
            "this is another step"
        };

        public const string DefinitionCode =
        @"
            namespace Feature.StepDefinitions
            {
                public class Definition
                {

                }
            }
        ";

        public static Feature Feature 
        { 
            get
            {
                return new Feature
                {
                    Name = "Name",
                    FileName = "File Name",
                    Namespace = "Features",
                    Scenarios =
                        {
                            new Scenario
                            {
                                Name = "Scenario1",
                                Steps = 
                                { 
                                    new Step{ Name = "Unique_step" }, 
                                    new Step{ Name = "Repeated_step" }
                                }
                            },                            
                            new Scenario
                            {
                                Name = "Scenario2",
                                Steps =
                                {
                                    new Step{ Name = "Repeated_step" }, 
                                    new Step{ Name = "Another_unique_step" }
                                }
                            },                            
                        }
                };
            } 
        } 

        public static Feature FeatureWithStepDefinitions 
        { 
            get
            {
                return new Feature
                {
                    Name = "StepDefinitions",
                    Scenarios = { new Scenario { Steps = { new Step { Name = "Step" } } } }
                };
            } 
        }


        public static Table ObjectTable
        {
            get
            {
                return new Table
                {
                    HasHeader = true,
                    Rows = 
                    {
                        new List<string> {"UserName", "Password"},
                        new List<string> {"lola", "run"},
                        new List<string> {"mani", "dumb"}
                    }
                };
            }
        }

        public static Feature FeatureWithArgs = new Feature
        {
            Name = "Name",
            FileName = "File Name",
            Namespace = "Features",
            Scenarios =
            {
                new Scenario
                {
                    Name = "Scenario_1",
                    Steps = 
                    { 
                        new Step
                        { 
                            Name = "If__happens",
                            Args = new List<string>{"X"},
                        },
                        new Step
                        { 
                            Name = "If__and__happens",
                            Args = new List<string>{"X", "Y"}
                        },
                    }
                }
            }                            
        };

        public static class DefinedFeature
        {
            public const string FeatureDefinition =
            @"
                Feature: Feature Name
                Scenario: has one step
                This is a step
            ";

            public const string StepsDefinition =
            @"
                namespace Features 
                {
                    public partial class FeatureName
                    {

                        public void This_is_one_step() {

                            var thing = FeatureName;
                        }
                    }
                }
            ";

            public const string StepsDefinitionWithBase =
            @"
                namespace Features 
                {
                    public partial class FeatureName : BaseClass
                    {

                        public void This_is_one_step() {

                            var thing = FeatureName;
                        }
                    }
                }
            ";

            public const string RenamedFeatureDefinition =
            @"
                Feature: Renamed Feature
                Scenario: has one step
                    This is a step
            ";

            public const string RenamedStepsDefinition =
            @"
                namespace Features 
                {
                    public partial class RenamedFeature
                    {

                        public void This_is_one_step() {

                            var thing = FeatureName;
                        }
                    }
                }
            ";
        }
    }
}
using System;
using System.Collections.Generic;
using Common;
using FluentSpec;
using MbUnit.Framework;
using NSubstitute;
using Raconteur;
using Raconteur.Generators.Steps;
using Raconteur.Helpers;
using Raconteur.IDE;
using Raconteur.Parsers;

namespace Specs
{
    [TestFixture]
    public class When_testing_with_simple_steps
    {
        [TestFixture]
        public class A_feature_parser : BehaviorOf<FeatureParserClass>
        {
            readonly FeatureFile FeatureFile = new FeatureFile();
            readonly FeatureItem FeatureItem = Substitute.For<FeatureItem>();

            [Test]
            public void should_read_the_name()
            {
                FeatureFile.Content = Actors.FeatureWithNoScenarios + Environment.NewLine + "whatever";
                
                The.FeatureFrom(FeatureFile, FeatureItem).Name
                    .ShouldBe("FeatureName");

                FeatureFile.Content = Actors.FeatureWithNoScenarios;
                
                The.FeatureFrom(FeatureFile, FeatureItem).Name
                    .ShouldBe("FeatureName");
            }

            [Test]
            public void should_camel_case_the_name()
            {
                FeatureFile.Content = "Feature: feature name";
                
                The.FeatureFrom(FeatureFile, FeatureItem).Name
                    .ShouldBe("FeatureName");
            }
            
            [Test]
            public void should_ensure_the_name_is_a_valid_identifier()
            {
                FeatureFile.Content = "Feature: feature + name";
                
                The.FeatureFrom(FeatureFile, FeatureItem).Name
                    .ShouldBe("Feature_Name");
            }
            
            [Test]
            public void should_build_scenarios()
            {
                var Scenarios = new List<Scenario>();

                FeatureFile.Content = Actors.FeatureWithThreeScenarios;
                
                Given.ScenarioTokenizer
                    .ScenariosFrom(Actors.FeatureWithThreeScenarios)
                    .Are(Scenarios);

                When.FeatureFrom(FeatureFile, FeatureItem)
                    .Scenarios.ShouldBe(Scenarios);
            }

            [Test]
            public void should_extract_namespace_and_file_name()
            {
                FeatureItem.DefaultNamespace = "MyNamespace";
                FeatureFile.Name = "MyFileName";

                The.FeatureFrom(FeatureFile, FeatureItem).Namespace
                    .ShouldBe("MyNamespace");

                The.FeatureFrom(FeatureFile, FeatureItem).FileName
                    .ShouldBe("MyFileName");
            }
        }

        [TestFixture]
        public class A_scenario_tokenizer : BehaviorOf<ScenarioTokenizerClass>
        {
            [Test]
            public void should_create_scenarios()
            {
                The.ScenariosFrom(Actors.FeatureWithNoScenarios)
                    .Count.ShouldBe(0);

                The.ScenariosFrom(Actors.FeatureWithOneScenario)
                    .Count.ShouldBe(1);

                The.ScenariosFrom(Actors.FeatureWithThreeScenarios)
                    .Count.ShouldBe(3);
            }

            [Test]
            public void should_ensure_valid_identifier_names()
            {
                "1 invalid scenario".ToValidIdentifier()
                    .ShouldBe("_1_invalid_scenario");

                "Is this scenario invalid?".ToValidIdentifier()
                    .ShouldBe("Is_this_scenario_invalid_");

                "This scenario shouldn't be valid!".ToValidIdentifier()
                    .ShouldBe("This_scenario_shouldn_t_be_valid_");

                "false".ToValidIdentifier()
                    .ShouldBe("@false");

                "  this is a pretty valid scenario   ".ToValidIdentifier()
                    .ShouldBe("this_is_a_pretty_valid_scenario");
            }
        }

        [TestFixture]
        public class A_scenario_parser : BehaviorOf<ScenarioParserClass>
        {
            List<string> Definition = new List<string>();

            [Test]
            public void should_name_steps()
            {
                var Step = new Step();
                Given.StepParser.IgnoringArgs().StepFrom(null).Is(Step);

                Definition = new List<string>
                {
                    "Scenario: Scenario Name",
                      "Do what you like"
                };

                The.ScenarioFrom(Definition.AsLocations())
                    .Steps[0].ShouldBe(Step);
            }

            [Test]
            public void should_name_scenarios()
            {
                Definition = new List<string>
                {
                    "Scenario: Scenario Name"
                };

                The.ScenarioFrom(Definition.AsLocations())
                    .Name.ShouldBe("ScenarioName");
            }

            [Test]
            public void should_ensure_valid_identifier_fo_scenario_names()
            {
                Definition = new List<string>
                {
                    "Scenario: Scenario + Name"
                };

                The.ScenarioFrom(Definition.AsLocations())
                    .Name.ShouldBe("Scenario_Name");
            }

            [Test]
            public void should_create_steps()
            {
                The.ScenarioFrom(Actors.ScenarioWithNoSteps.AsLocations())
                    .Steps.Count.ShouldBe(0);

                The.ScenarioFrom(Actors.ScenarioWithTwoSteps.AsLocations())
                    .Steps.Count.ShouldBe(2);

            }
        }

        [TestFixture]
        public class A_step_parser : BehaviorOf<StepParserClass>
        {
            [Test]
            public void should_build_steps_from_sentences()
            {
                The.StepFrom("Do what you like".AsLocation())
                    .Name.ShouldBe("Do_what_you_like");
            }
        }

        static string StepDefinitions;

        [TestFixture]
        public class for_the_first_time 
        {
            [FixtureSetUp]
            public void SetUp()
            {
                StepDefinitions = ObjectFactory.NewStepDefinitionsGenerator(Actors.Feature, null).Code;
            }

            [Test]
            public void should_generate_the_StepDefinitions_namespace()
            {
                StepDefinitions.ShouldContain("namespace Features");
            }

            [Test]
            public void should_generate_the_StepDefinitions_class()
            {
                StepDefinitions.ShouldContain("public partial class Name");
            }
        }

        [TestFixture]
        public class with_existing_steps : BehaviorOf<StepDefinitionsGenerator>
        {
            [FixtureSetUp]
            public void SetUp()
            {
                var Feature = Actors.Feature;
                Feature.Namespace = "NewFeatures";

                StepDefinitions = ObjectFactory.NewStepDefinitionsGenerator
                (
                    Feature, Actors.DefinedFeature.StepsDefinitionWithBase
                ).Code;
            }

            [Test]
            public void should_change_the_namespace()
            {
                StepDefinitions.ShouldContain("namespace NewFeatures");
            }

            [Test]
            public void should_change_the_class_name()
            {
                StepDefinitions.ShouldContain("public partial class Name");
                StepDefinitions.ShouldNotContain("public partial class FeatureName");
            }

            [Test]
            public void should_keep_defined_content()
            {
                StepDefinitions.ShouldContain("var thing = FeatureName");
            }

            [Test]
            public void should_keep_inheritance()
            {
                StepDefinitions.ShouldContain(": BaseClass");
            }
        }
    }
}
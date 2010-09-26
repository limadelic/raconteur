using System;
using FluentSpec;
using MbUnit.Framework;
using Raconteur;
using Raconteur.Generators;
using Raconteur.IDE;
using Raconteur.Parsers;

namespace Specs
{
    [TestFixture]
    public class When_parsing_a_FeatureFile
    {
        [TestFixture]
        public class A_feature_parser : BehaviorOf<FeatureParserClass>
        {
            readonly FeatureFile FeatureFile = new FeatureFile();
            readonly VsFeatureItem VsFeatureItem = new VsFeatureItem();

            [Test]
            public void should_read_the_name()
            {
                FeatureFile.Content = Actors.FeatureWithNoScenarios + Environment.NewLine + "whatever";
                
                The.FeatureFrom(FeatureFile, VsFeatureItem).Name
                    .ShouldBe("FeatureName");

                FeatureFile.Content = Actors.FeatureWithNoScenarios;
                
                The.FeatureFrom(FeatureFile, VsFeatureItem).Name
                    .ShouldBe("FeatureName");
            }

            [Test]
            public void should_camel_case_the_name()
            {
                FeatureFile.Content = "Feature: feature name";
                
                The.FeatureFrom(FeatureFile, VsFeatureItem).Name
                    .ShouldBe("FeatureName");
            }
            
            [Test]
            public void should_build_scenarios()
            {
                FeatureFile.Content = Actors.FeatureWithThreeScenarios;

                When.FeatureFrom(FeatureFile, VsFeatureItem);

                Then.ScenarioParser.Should().ScenariosFrom(Actors.FeatureWithThreeScenarios);
            }

            [Test]
            public void should_extract_namespace_and_file_name()
            {
                VsFeatureItem.DefaultNamespace = "MyNamespace";
                FeatureFile.Name = "MyFileName";

                The.FeatureFrom(FeatureFile, VsFeatureItem).Namespace
                    .ShouldBe("MyNamespace");

                The.FeatureFrom(FeatureFile, VsFeatureItem).FileName
                    .ShouldBe("MyFileName");
            }
        }

        [TestFixture]
        public class A_scenario_parser : BehaviorOf<ScenarioParserClass>
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
            public void should_name_scenarios()
            {
                The.ScenariosFrom("Scenario: Scenario Name")[0]
                    .Name.ShouldBe("ScenarioName");
            }

            [Test]
            public void should_create_steps()
            {
                The.ScenariosFrom(Actors.ScenarioWithNoSteps)[0]
                    .Steps.Count.ShouldBe(0);

                The.ScenariosFrom(Actors.ScenarioWithOneStep)[0]
                    .Steps.Count.ShouldBe(1);

                The.ScenariosFrom(Actors.ScenarioWithThreeSteps)[0]
                    .Steps.Count.ShouldBe(3);
            }

            [Test]
            public void should_name_steps()
            {
                var Step = TestObjectFor<Step>();
                Given.StepParser.StepFrom("Do what you like").Is(Step);

                The.ScenariosFrom(
                @"
                    Scenario: Scenario Name
                        Do what you like
                ")[0].Steps[0].ShouldBe(Step);
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
        public class A_step_parser : BehaviorOf<StepParserClass>
        {
            [Test]
            public void should_build_steps_from_sentences()
            {
                The.StepFrom("Do what you like")
                    .Name.ShouldBe("Do_what_you_like");
            }
        }
    }
}
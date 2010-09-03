using System;
using System.Collections.Generic;
using EnvDTE80;
using FluentSpec;
using MbUnit.Framework;
using Raconteur;
using Raconteur.Generators;
using Raconteur.Parsers;

namespace Specs
{
    [TestFixture]
    public class When_generating_the_feature_runner
    {
        [TestFixture]
        public class A_runner_generator : BehaviorOf<RunnerGenerator>
        {
            [Test]
            public void should_generate_the_step_definition_reference() 
            {
                var FeatureFile = new FeatureFile { Content = "FeatureFileContent" };
                var Feature = new Feature { Name = "FeatureName", Scenarios = new List<Scenario>() };

                Given.Parser.FeatureFrom("FeatureFileContent").Is(Feature);

                The.RunnerFor(FeatureFile).ShouldContain("StepDefinitions" +
                ".FeatureName Steps = new StepDefinitions.FeatureName();");
            }

            [Test]
            public void it()
            {
                var dte2 = (DTE2) System.Runtime.InteropServices.Marshal.GetActiveObject("VisualStudio.DTE.10.0");

                var clazz = dte2.ActiveDocument.ProjectItem.FileCodeModel.CodeElements.Item("A_runner_generator") as CodeElement2;

                clazz.RenameSymbol("A_runner_generation");

                dte2.ShouldNotBeNull();
            }
        }

        [TestFixture]
        public class A_feature_parser : BehaviorOf<FeatureParserClass>
        {
            [Test]
            public void should_read_the_name()
            {
                The.FeatureFrom(Actors.FeatureWithNoScenarios + Environment.NewLine + "whatever").Name
                    .ShouldBe("FeatureName");

                The.FeatureFrom(Actors.FeatureWithNoScenarios).Name
                    .ShouldBe("FeatureName");
            }

            [Test]
            public void should_camel_case_the_name()
            {
                The.FeatureFrom("Feature: feature name").Name
                    .ShouldBe("FeatureName");
            }
            
            [Test]
            public void should_build_scenarios()
            {
                When.FeatureFrom(Actors.FeatureWithThreeScenarios);
                Then.ScenarioParser.Should().ScenariosFrom(Actors.FeatureWithThreeScenarios);
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
                The.ScenariosFrom(
                @"
                    Scenario: Scenario Name
                        Do what you like
                ")[0].Steps[0]
                .ShouldBe("Do_what_you_like");
            }
        }
    }
}
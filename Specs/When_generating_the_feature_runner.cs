using System;
using System.Collections.Generic;
using System.Linq;
using EnvDTE;
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
                var Feature = new Feature { Name = "FeatureName", Scenarios = new List<Scenario>() };

                The.RunnerFor(Feature).ShouldContain("StepDefinitions" +
                ".FeatureName Steps = new StepDefinitions.FeatureName();");
            }

            [Test]
            public void it()
            {
                var dte2 = (DTE2)System.Runtime.InteropServices.Marshal.GetActiveObject("VisualStudio.DTE.10.0");
                var CodeElements = dte2.ActiveDocument.ProjectItem.FileCodeModel.CodeElements;

                var namespazz = CodeElements.Cast<CodeElement>().Where(CodeElement =>
                {
                    try
                    {
                        return !string.IsNullOrEmpty(CodeElement.Name);
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }).FirstOrDefault();

                var clazz = namespazz.Children.Cast<CodeElement2>().Where(CodeElement =>
                {
                    try
                    {
                        return !string.IsNullOrEmpty(CodeElement.Name);
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }).FirstOrDefault();

                var fullname = clazz.FullName;
                //                clazz.RenameSymbol("When_generating_the_number");

                namespazz.ShouldNotBeNull();
            }
        }

        [TestFixture]
        public class A_feature_parser : BehaviorOf<FeatureParserClass>
        {
            readonly FeatureFile FeatureFile = new FeatureFile();

            [Test]
            public void should_read_the_name()
            {
                FeatureFile.Content = Actors.FeatureWithNoScenarios + Environment.NewLine + "whatever";
                
                The.FeatureFrom(FeatureFile).Name
                    .ShouldBe("FeatureName");

                FeatureFile.Content = Actors.FeatureWithNoScenarios;
                
                The.FeatureFrom(FeatureFile).Name
                    .ShouldBe("FeatureName");
            }

            [Test]
            public void should_camel_case_the_name()
            {
                FeatureFile.Content = "Feature: feature name";
                
                The.FeatureFrom(FeatureFile).Name
                    .ShouldBe("FeatureName");
            }
            
            [Test]
            public void should_build_scenarios()
            {
                FeatureFile.Content = Actors.FeatureWithThreeScenarios;

                When.FeatureFrom(FeatureFile);

                Then.ScenarioParser.Should().ScenariosFrom(Actors.FeatureWithThreeScenarios);
            }

            [Test]
            public void should_extract_namespace_and_file_name()
            {
                FeatureFile.Namespace = "MyNamespace";
                FeatureFile.Name = "MyFileName";

                The.FeatureFrom(FeatureFile).Namespace
                    .ShouldBe("MyNamespace");

                The.FeatureFrom(FeatureFile).FileName
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
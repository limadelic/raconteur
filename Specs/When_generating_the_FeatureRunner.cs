using System;
using EnvDTE80;
using FluentSpec;
using MbUnit.Framework;
using Raconteur.Generators;
using Raconteur.IDE;
using Raconteur.IDEIntegration;
using Raconteur.Parsers;

namespace Specs
{
    [TestFixture]
    public class When_generating_the_FeatureRunner
    {
        [Test]
        public void it()
        {
            var Dte2 = (DTE2)System.Runtime.InteropServices.Marshal.GetActiveObject("VisualStudio.DTE.10.0");
            var projectItem = Dte2.Solution.FindProjectItem(@"A:\dev\raconteur\Features\StepDefinitions\GenerateFeatureRunner.cs");
            var FileContents = DteProjectReader.GetFileContent(projectItem);
            FileContents.ShouldNotBeNull();
        }

        [TestFixture]
        public class A_feature_parser : BehaviorOf<FeatureParserClass>
        {
            readonly FeatureFile FeatureFile = new FeatureFile();
            readonly ProjectClass ProjectClass = new ProjectClass();

            [Test]
            public void should_read_the_name()
            {
                FeatureFile.Content = Actors.FeatureWithNoScenarios + Environment.NewLine + "whatever";
                
                The.FeatureFrom(FeatureFile, ProjectClass).Name
                    .ShouldBe("FeatureName");

                FeatureFile.Content = Actors.FeatureWithNoScenarios;
                
                The.FeatureFrom(FeatureFile, ProjectClass).Name
                    .ShouldBe("FeatureName");
            }

            [Test]
            public void should_camel_case_the_name()
            {
                FeatureFile.Content = "Feature: feature name";
                
                The.FeatureFrom(FeatureFile, ProjectClass).Name
                    .ShouldBe("FeatureName");
            }
            
            [Test]
            public void should_build_scenarios()
            {
                FeatureFile.Content = Actors.FeatureWithThreeScenarios;

                When.FeatureFrom(FeatureFile, ProjectClass);

                Then.ScenarioParser.Should().ScenariosFrom(Actors.FeatureWithThreeScenarios);
            }

            [Test]
            public void should_extract_namespace_and_file_name()
            {
                ProjectClass.DefaultNamespace = "MyNamespace";
                FeatureFile.Name = "MyFileName";

                The.FeatureFrom(FeatureFile, ProjectClass).Namespace
                    .ShouldBe("MyNamespace");

                The.FeatureFrom(FeatureFile, ProjectClass).FileName
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
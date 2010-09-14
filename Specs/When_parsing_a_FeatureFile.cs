using System;
using System.Linq;
using EnvDTE;
using EnvDTE80;
using FluentSpec;
using MbUnit.Framework;
using Raconteur.Generators;
using Raconteur.IDE;
using Raconteur.Parsers;
using Raconteur;

namespace Specs
{
    [TestFixture]
    public class When_parsing_a_FeatureFile
    {
        string FeatureFile;

        [Test]
        public void it()
        {
//            var Dte2 = (DTE2)System.Runtime.InteropServices.Marshal.GetActiveObject("VisualStudio.DTE.10.0");
//
//            FeatureFile = @"A:\dev\raconteur\Features\RaconteurFeature2.feature";
//
//            var ProjectItem = Dte2.Solution.FindProjectItem(FeatureFile);
//
//            var Project = ObjectFactory.ProjectFrom(ProjectItem);
//
//            Project.AddStepDefinitions(null);
        }

        [TestFixture]
        public class A_feature_parser : BehaviorOf<FeatureParserClass>
        {
            readonly FeatureFile FeatureFile = new FeatureFile();
            readonly VsProject VsProject = new VsProject();

            [Test]
            public void should_read_the_name()
            {
                FeatureFile.Content = Actors.FeatureWithNoScenarios + Environment.NewLine + "whatever";
                
                The.FeatureFrom(FeatureFile, VsProject).Name
                    .ShouldBe("FeatureName");

                FeatureFile.Content = Actors.FeatureWithNoScenarios;
                
                The.FeatureFrom(FeatureFile, VsProject).Name
                    .ShouldBe("FeatureName");
            }

            [Test]
            public void should_camel_case_the_name()
            {
                FeatureFile.Content = "Feature: feature name";
                
                The.FeatureFrom(FeatureFile, VsProject).Name
                    .ShouldBe("FeatureName");
            }
            
            [Test]
            public void should_build_scenarios()
            {
                FeatureFile.Content = Actors.FeatureWithThreeScenarios;

                When.FeatureFrom(FeatureFile, VsProject);

                Then.ScenarioParser.Should().ScenariosFrom(Actors.FeatureWithThreeScenarios);
            }

            [Test]
            public void should_extract_namespace_and_file_name()
            {
                VsProject.DefaultNamespace = "MyNamespace";
                FeatureFile.Name = "MyFileName";

                The.FeatureFrom(FeatureFile, VsProject).Namespace
                    .ShouldBe("MyNamespace");

                The.FeatureFrom(FeatureFile, VsProject).FileName
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
using System.Collections.Generic;
using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raconteur;
using Raconteur.IDE;

namespace Features 
{
    public partial class UserSettings
    {
        readonly Project Project = Create.TestObjectFor<Project>();

        readonly List<dynamic> backup = new List<dynamic>();

        [TestInitialize]
        public void SetUp()
        {
            backup.Add(Settings.XUnit);    
            backup.Add(Settings.Language);    
            backup.Add(Settings.StepDefinitions);    
        }

        void Given_the_settings(string Settings)
        {
            Given.That(Project).HasSettingsFile.Is(true);
            Given.That(Project).SettingsFileContent.Is(Settings);
        }

        void When_the_project_is_loaded()
        {
            Project.Load();
        }

        void The_Settings_should_be_(string xUnit, string Language)
        {
            Settings.XUnit.Name.ShouldBe(xUnit);
            Settings.Language.Name.ShouldBe(Language);
        }

        [TestCleanup]
        public void TearDown()
        {
            Settings.XUnit = backup[0];
            Settings.Language = backup[1];
            Settings.StepDefinitions = backup[2];
        }

        void The_Step_Definitions_should_be_(params string[][] StepDefinitions)
        {
            Settings.StepDefinitions.Count.ShouldBe(StepDefinitions.Length);
            foreach (var StepDefinition in StepDefinitions)
                Settings.StepDefinitions.ShouldContain(StepDefinition[0]);
        }
    }
}

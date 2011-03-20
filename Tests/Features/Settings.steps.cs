using System.Collections.Generic;
using FluentSpec;
using MbUnit.Framework;
using Raconteur;
using Raconteur.Helpers;
using Raconteur.IDE;

namespace Features 
{
    public partial class UserSettings
    {
        readonly SettingsLoader SettingsLoader = 
            Create.TestObjectFor<SettingsLoader>(new Project());

        readonly List<dynamic> backup = new List<dynamic>();

        [SetUp]
        public void SetUp()
        {
            backup.Add(Settings.XUnit);    
            backup.Add(Settings.Language);    
            backup.Add(Settings.StepDefinitions);    
            backup.Add(Settings.Libraries);
        }

        void Given_the_settings(string Settings)
        {
            Given.That(SettingsLoader).HasSettingsFile.Is(true);
            Given.That(SettingsLoader).SettingsFileContent.Is(Settings);
        }

        void When_the_project_is_loaded()
        {
            SettingsLoader.Load();
        }

        void The_Settings_should_be_(string xUnit, string Language)
        {
            Settings.XUnit.Name.ShouldBe(xUnit);
            Settings.Language.Name.ShouldBe(Language);
        }

        [TearDown]
        public void TearDown()
        {
            Settings.XUnit = backup[0];
            Settings.Language = backup[1];
            Settings.StepDefinitions = backup[2];
            Settings.Libraries = backup[3];
        }

        void The_Step_Definitions_should_be_(string[] StepDefinitions)
        {
            Settings.StepDefinitions.Count.ShouldBe(StepDefinitions.Length);
            foreach (var StepDefinition in StepDefinitions)
                Settings.StepDefinitions.ShouldContain(StepDefinition);
        }

        void The_Libraries_should_be_(string[] Libraries) 
        {
            Settings.Libraries.Count.ShouldBe(Libraries.Length);
            foreach (var Library in Libraries)
                Settings.Libraries.ShouldContain(Library);
        }
    }
}

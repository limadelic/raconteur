using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raconteur;
using Raconteur.IDE;

namespace Features 
{
    public partial class UserSettings
    {
        readonly Project Project = Create.TestObjectFor<Project>();

        dynamic backup;

        [TestInitialize]
        public void SetUp()
        {
            backup = Settings.XUnit;    
        }

        void Given_the_settings(string AppConfig)
        {
            Given.That(Project).HasAppConfig.Is(true);
            Given.That(Project).AppConfig
                .Is(XmlDocument.Load(AppConfig.ToLower()));
        }

        void When_the_project_is_loaded()
        {
            Project.Load();
        }

        void The_xUnit_runner_should_be(string xUnit)
        {
            Settings.XUnit.ShouldBe(xUnit.ToUpper());
        }

        [TestCleanup]
        public void TearDown()
        {
            Settings.XUnit = backup;    
        }
    }
}

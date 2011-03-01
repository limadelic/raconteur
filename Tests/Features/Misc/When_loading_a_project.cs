using System.IO;
using System.Runtime.InteropServices;
using EnvDTE;
using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raconteur;
using Project = Raconteur.IDE.Project;

namespace Features.Misc
{
    [TestClass]
    public class When_loading_a_project
    {
        dynamic backup;

        [TestInitialize]
        public void SetUp() { backup = Settings.Project;}

        [TestCleanup]
        public void TearDown() { Settings.Project = backup; }

        [TestMethod]
        public void should_index_settings_by_Project()
        {
            Settings.RestoreDefaults();
            Settings.XUnit.Name.ShouldBe("MsTest");
            
            Settings.Project = new object();
            Settings.XUnit = XUnits.Framework["mbunit"];
            Settings.XUnit.Name.ShouldBe("MbUnit");

            Settings.RestoreDefaults();
            Settings.XUnit.Name.ShouldBe("MsTest");
        }

        // needs to b run from IDE 
        public void should_load_Settings()
        {
            var Dte = Marshal.GetActiveObject("VisualStudio.DTE") as DTE;

            var DteProject = Dte.SelectedItems.Item(1).ProjectItem.ContainingProject;

            string fullPath = DteProject.Properties.Item("FullPath").Value.ToString();
            string outputPath = DteProject.ConfigurationManager.ActiveConfiguration.Properties.Item("OutputPath").Value.ToString();
            string outputDir = Path.Combine(fullPath, outputPath);
            string outputFileName = DteProject.Properties.Item("OutputFileName").Value.ToString();
            string assemblyPath = Path.Combine(outputDir, outputFileName);

            DteProject.Name.ShouldBe("Features");


            (assemblyPath).ShouldNotBeNull();

            var Project = new Project { DTEProject = DteProject };

            Project.Load();

            Settings.XUnit.Name.ShouldBe("MsTest");
        }
    }
}
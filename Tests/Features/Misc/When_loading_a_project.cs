using System.Runtime.InteropServices;
using EnvDTE;
using FluentSpec;
using Raconteur;
using Project = Raconteur.IDE.Project;

namespace Features.Misc
{
    public class When_loading_a_project
    {
        public void should_load_Settings()
        {
            var Dte = Marshal.GetActiveObject("VisualStudio.DTE") as DTE;

            var DteProject = Dte.SelectedItems.Item(1).ProjectItem.ContainingProject;

            DteProject.Name.ShouldBe("Features");

            var Project = new Project { DTEProject = DteProject };

            Project.Load();

            Settings.XUnit.ShouldBe("MBUNIT");
        }
    }
}
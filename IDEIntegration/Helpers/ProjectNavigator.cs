using System.IO;
using System.Runtime.InteropServices;
using EnvDTE;

namespace Raconteur.IDEIntegration.Helpers
{
    public class ProjectNavigator
    {
        private static Project GetCurrentProject()
        {
            var Dte = Marshal.GetActiveObject("VisualStudio.DTE") as DTE;
            return Dte.ActiveDocument.ProjectItem.ContainingProject;
        }

        public static string ActiveAssemblyPath
        {
            get
            {
                var DteProject = GetCurrentProject();
                string fullPath = DteProject.Properties.Item("FullPath").Value.ToString();
                string outputPath = DteProject.ConfigurationManager.ActiveConfiguration.Properties.Item("OutputPath").Value.ToString();
                string outputDir = Path.Combine(fullPath, outputPath);
                string outputFileName = DteProject.Properties.Item("OutputFileName").Value.ToString();
                return Path.Combine(outputDir, outputFileName);
            }
        }

        public static string DefaultNamespace
        {
            get
            {
                return GetCurrentProject().Properties.Item("DefaultNamespace").Value as string;
            }
        }
    }
}
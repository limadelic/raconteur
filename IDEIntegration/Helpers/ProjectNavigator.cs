using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using EnvDTE;
using Raconteur.Helpers;
using Project = EnvDTE.Project;

namespace Raconteur.IDEIntegration.Helpers
{
    public class ProjectNavigator
    {
        private static DTE dte;
        private static DTE Dte {get { return dte ?? LoadDte(); } }
        private static DTE LoadDte()
        {
            return dte = Marshal.GetActiveObject("VisualStudio.DTE") as DTE;
        }

        private static string CurrentFilePath
        {
            get
            {
                var fullPath = Dte.ActiveDocument.ProjectItem.Properties.Item("FullPath").Value.ToString();
                var fileName = Dte.ActiveDocument.Name;

                return fullPath.Replace(fileName, string.Empty);
            }
        }
        private static string ProjectPath { get { return CurrentProject.Properties.Item("FullPath").Value.ToString(); } }
        private static string ProjectOutputPath { get { return CurrentProject.ConfigurationManager.ActiveConfiguration.Properties.Item("OutputPath").Value.ToString(); } }
        private static ProjectItem ProjectItem { get { return Dte.ActiveDocument.ProjectItem; } }

        private static Project CurrentProject
        {
            get { return Dte.ActiveDocument.ProjectItem.ContainingProject; }
        }

        private static string ActiveAssemblyPath
        {
            get
            {
                var DteProject = CurrentProject;
                string outputDir = Path.Combine(ProjectPath, ProjectOutputPath);
                string outputFileName = DteProject.Properties.Item("OutputFileName").Value.ToString();
                return Path.Combine(outputDir, outputFileName);
            }
        }

        private static string DefaultNamespace
        {
            get
            {
                var currentDefault = ProjectItem.ContainingProject.Properties.Item("DefaultNamespace").Value.ToString();
                var currentFilePath = CurrentFilePath;
                var projectPath = ProjectPath;

                return currentFilePath == projectPath ? currentDefault :
                    currentDefault + "." + currentFilePath.Replace(projectPath, string.Empty).Replace(@"\", ".").TrimEnd('.');
            }
        }

        public static Type TypeInCurrentProject(string className)
        {
            var typeName = DefaultNamespace + "." + className;

            return Assembly.LoadFrom(ActiveAssemblyPath)
                    .GetType(typeName);
        }
    }
}
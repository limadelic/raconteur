using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EnvDTE;
using Microsoft.VisualStudio;
using Raconteur.Generators;

namespace Raconteur.IDEIntegration
{
    class DteProjectReader
    {
        public static List<ProjectItem> GetAllProjectItem(EnvDTE.Project Project)
        {
            var retval = new List<ProjectItem>();
            var items = new Queue<ProjectItem>();
            foreach (ProjectItem item in Project.ProjectItems) items.Enqueue(item);
            while (items.Count != 0)
            {
                var item = items.Dequeue();
                retval.Add(item);
                if (item.ProjectItems != null) foreach (ProjectItem subitem in item.ProjectItems) items.Enqueue(subitem);
            }
            return retval;
        }

        public static Project LoadProjectFrom(EnvDTE.Project project)
        {
            var ProjectFolder = Path.GetDirectoryName(project.FullName);

            var Project = new Project
            {
                ProjectFolder = ProjectFolder,
                ProjectName = Path.GetFileNameWithoutExtension(project.FullName),
                AssemblyName = project.Properties.Item("AssemblyName").Value as string,
                DefaultNamespace = project.Properties.Item("DefaultNamespace").Value as string
            };

            foreach (var projectItem in GetAllProjectItem(project).Where(IsPhysicalFile))
            {
                var fileName = GetRelativePath(GetFileName(projectItem), ProjectFolder);

                var extension = Path.GetExtension(fileName);
                if (extension.Equals(".feature", StringComparison.InvariantCultureIgnoreCase))
                {
                    var featureFile = new FeatureFile(fileName);
                    var ns = projectItem.Properties.Item("CustomToolNamespace").Value as string;
                    featureFile.Namespace = Project.DefaultNamespace;
                    if (!String.IsNullOrEmpty(ns)) featureFile.CustomNamespace = ns;
                    Project.FeatureFiles.Add(featureFile);
                }
            }
            return Project;
        }

        static bool IsPhysicalFile(ProjectItem projectItem)
        {
            return string.Equals(projectItem.Kind, VSConstants.GUID_ItemType_PhysicalFile.ToString("B"),
                StringComparison.InvariantCultureIgnoreCase);
        }

        public static string GetFileContent(ProjectItem projectItem)
        {
            if (projectItem.IsOpen[Constants.vsViewKindAny])
            {
                var TextDoc = (TextDocument) projectItem.Document.Object("TextDocument");
                var Start = TextDoc.StartPoint.CreateEditPoint();
                return Start.GetText(TextDoc.EndPoint);
            }

            using (TextReader File = new StreamReader(GetFileName(projectItem))) 
                return File.ReadToEnd();
        }

        static string GetFileName(ProjectItem ProjectItem) { return ProjectItem.FileNames[1]; }

        public static string GetRelativePath(string path, string basePath)
        {
            path = Path.GetFullPath(path);
            basePath = Path.GetFullPath(basePath);
            if (string.Equals(path, basePath, StringComparison.OrdinalIgnoreCase)) return "."; // the "this folder"

            if (path.StartsWith(basePath + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase)) return path.Substring(basePath.Length + 1);

            //handle different drives
            var pathRoot = Path.GetPathRoot(path);
            if (!string.Equals(pathRoot, Path.GetPathRoot(basePath), StringComparison.OrdinalIgnoreCase)) return path;

            //handle ".." pathes
            var pathParts = path.Substring(pathRoot.Length).Split(Path.DirectorySeparatorChar,
                Path.AltDirectorySeparatorChar);
            var basePathParts = basePath.Substring(pathRoot.Length).Split(Path.DirectorySeparatorChar,
                Path.AltDirectorySeparatorChar);

            var commonFolderCount = 0;
            while (commonFolderCount < pathParts.Length && commonFolderCount < basePathParts.Length &&
                string.Equals(pathParts[commonFolderCount], basePathParts[commonFolderCount],
                    StringComparison.OrdinalIgnoreCase)) commonFolderCount++;

            var result = new StringBuilder();
            for (var i = 0; i < basePathParts.Length - commonFolderCount; i++)
            {
                result.Append("..");
                result.Append(Path.DirectorySeparatorChar);
            }

            if (pathParts.Length - commonFolderCount == 0) return result.ToString().TrimEnd(Path.DirectorySeparatorChar);

            result.Append(string.Join(Path.DirectorySeparatorChar.ToString(), pathParts, commonFolderCount,
                pathParts.Length - commonFolderCount));
            return result.ToString();
        }
    }
}
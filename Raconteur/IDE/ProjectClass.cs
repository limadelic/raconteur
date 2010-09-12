using System.IO;

namespace Raconteur.IDE
{
    public class ProjectClass : Project
    {
        public string ProjectName { get; set; }
        public string AssemblyName { get; set; }
        public string ProjectFolder { get; set; }
        public string DefaultNamespace { get; set; }

        public static ProjectClass LoadProjectFrom(EnvDTE.Project Project)
        {
            return new ProjectClass
            {
                ProjectFolder = Path.GetDirectoryName(Project.FullName),
                ProjectName = Path.GetFileNameWithoutExtension(Project.FullName),
                AssemblyName = Project.Properties.Item("AssemblyName").Value as string,
                DefaultNamespace = Project.Properties.Item("DefaultNamespace").Value as string
            };
        }

        public void AddStepDefinitions(string FeatureFile, string Content)
        {
        }

        public bool ContainsStepDefinitions(string FeatureFile) 
        { 
            return false;
        }
    }
}
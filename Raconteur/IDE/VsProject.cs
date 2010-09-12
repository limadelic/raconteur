using System.IO;
using System.Linq;
using EnvDTE;

namespace Raconteur.IDE
{
    public class VsProject : Project
    {
        public readonly EnvDTE.Project Project;
        public string DefaultNamespace { get; set; }
        
        public string ProjectName
        {
            get { return Path.GetFileNameWithoutExtension(Project.FullName); }
        }

        public string AssemblyName
        {
            get { return Project.Properties.Item("AssemblyName").Value as string; }
        }

        public string ProjectFolder
        {
            get { return Path.GetFileNameWithoutExtension(Project.FullName); }
        }

        public VsProject() {}

        public VsProject(EnvDTE.Project Project)
        {
            this.Project = Project;
            DefaultNamespace = Project.Properties.Item("DefaultNamespace").Value as string;
        }
        
        public virtual void AddStepDefinitions(string FeatureFile, string Content)
        {
            var FeatureFileItem = Project.ProjectItems.Cast<ProjectItem>()
                .FirstOrDefault(Item => Item.Name == FeatureFile + ".feature");

            var DefinitionPath = FeatureFileItem.FileNames[0]
                .Replace(".feature", ".steps.cs");

            AddFile(DefinitionPath, Content);

            FeatureFileItem.ProjectItems.AddFromFile(DefinitionPath);
        }

        void AddFile(string DefinitionPath, string Content) 
        {
            using (var FileWriter = new StreamWriter(DefinitionPath))
                FileWriter.Write(Content);
        }

        public bool ContainsStepDefinitions(string FeatureFile) 
        { 
            return File.Exists(FeatureFile + ".steps.cs");
        }
    }
}
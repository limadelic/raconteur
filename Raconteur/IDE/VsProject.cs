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

        string FeatureFile { get; set; }

        public virtual void AddStepDefinitions(string FeatureFile, string Content)
        {
            this.FeatureFile = FeatureFile;

            AddFile(StepsFile, Content);

            FeatureFileItem.ProjectItems.AddFromFile(StepsFile);
        }

        ProjectItem FeatureFileItem
        {
            get
            {
                return Project.ProjectItems.Cast<ProjectItem>()
                    .FirstOrDefault(Item => Item.Name == FeatureFile + ".feature");
            }
        }

        void AddFile(string Name, string Content) 
        {
            using (var FileWriter = new StreamWriter(Name))
                FileWriter.Write(Content);
        }

        string StepsFile
        {
            get
            {
                return FeatureFileItem.FileNames[0].Replace(".feature", ".steps.cs");
            } 
        }

        public bool ContainsStepDefinitions(string FeatureFile) 
        { 
            this.FeatureFile = FeatureFile;

            return File.Exists(StepsFile);
        }
    }
}
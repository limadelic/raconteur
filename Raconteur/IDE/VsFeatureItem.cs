using System.IO;
using System.Linq;
using EnvDTE;

namespace Raconteur.IDE
{
    public class VsFeatureItem : FeatureItem
    {
        public Project Project { get { return FeatureItem.ContainingProject; } }

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

        public VsFeatureItem() {}

        public VsFeatureItem(ProjectItem FeatureItem)
        {
            this.FeatureItem = FeatureItem;

            StepsItem = FeatureItem.ProjectItems.Cast<ProjectItem>()
                .FirstOrDefault(Item => Item.Name.EndsWith(".steps.cs"));

            DefaultNamespace = Project.Properties.Item("DefaultNamespace").Value as string;
        }

        ProjectItem FeatureItem { get; set;  }    
        ProjectItem StepsItem { get; set;  }    

        public virtual void AddStepDefinitions(string Content)
        {
            if (HasSteps) RenameSteps();
            
            Write(StepsFile, Content);
                
            FeatureItem.ProjectItems.AddFromFile(StepsFile);
        }

        void RenameSteps() 
        { 
            File.Move(ExistingStepsFile, StepsFile);
            StepsItem.Remove();
        }

        protected string ExistingStepsFile
        {
            get
            {
                return StepsItem == null ? null : StepsItem.FileNames[0];
            } 
        }

        protected bool HasSteps
        {
            get { return StepsItem != null; } 
        }

        void Write(string Name, string Content) 
        {
            using (var FileWriter = new StreamWriter(Name))
                FileWriter.Write(Content);
        }

        string StepsFile
        {
            get
            {
                return FeatureItem.FileNames[0].Replace(".feature", ".steps.cs");
            } 
        }

        public bool ContainsStepDefinitions 
        {
            get { return File.Exists(StepsFile); } 
        }

        public string ExistingStepDefinitions
        {
            get 
            { 
                if (!ContainsStepDefinitions) return null;

                using (var FileReader = new StreamReader(StepsFile))
                    return FileReader.ReadToEnd();
            }
        }
    }
}
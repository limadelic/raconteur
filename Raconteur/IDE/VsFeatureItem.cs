using System.IO;
using System.Linq;
using EnvDTE;

namespace Raconteur.IDE
{
    public class VsFeatureItem : FeatureItem
    {
        public string Assembly
        {
            get
            {
                var FullPath = Project.Properties.Item("FullPath").Value.ToString();
                var OutputPath = Project.ConfigurationManager.ActiveConfiguration.Properties.Item("OutputPath").Value.ToString();
                var OutputDir = Path.Combine(FullPath, OutputPath);
                var OutputFileName = Project.Properties.Item("OutputFileName").Value.ToString();

                return Path.Combine(OutputDir, OutputFileName);
            }
        }

        public EnvDTE.Project Project { get { return FeatureItem.ContainingProject; } }

        public string DefaultNamespace { get; set; }
        
//        public string ProjectName
//        {
//            get { return Path.GetFileNameWithoutExtension(Project.FullName); }
//        }

//        public string ProjectFolder
//        {
//            get { return Path.GetFileNameWithoutExtension(Project.FullName); }
//        }

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
            
            if (Content != ExistingStepDefinitions) Write(StepsFile, Content);
                
            if (!HasSteps || NameChanged) FeatureItem.ProjectItems.AddFromFile(StepsFile);
        }

        bool NameChanged { get { return ExistingStepsFile != StepsFile; } }

        void RenameSteps() 
        { 
            if (!NameChanged) return;

            StepsItem.Remove();

            File.Move(ExistingStepsFile, StepsFile);
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
            File.WriteAllText(Name, Content);
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
            get 
            { 
                return !ExistingStepsFile.IsEmpty() 
                    && File.Exists(ExistingStepsFile); 
            } 
        }

        public string ExistingStepDefinitions
        {
            get
            {
                return !ContainsStepDefinitions ? null 
                    : File.ReadAllText(ExistingStepsFile);
            }
        }
    }
}
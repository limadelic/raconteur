using JetBrains.Application.Progress;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Refactorings.Rename;

namespace Raconteur.Resharper
{
    public class RenameStepWorkflow : RenameWorkflow
    {
        public string FileName { get; set; }

        public RenameStepWorkflow(string FileName, ISolution Solution, string ActionId) 
            : base(Solution, ActionId)
        {
            this.FileName = FileName;
        }

        public override bool PostExecute(IProgressIndicator pi) 
        {
            if (!base.PostExecute(pi)) return false;

            RenameStepsInFeature();

            return true;
        }

        string InitialStepName { get { return InitialName.Replace("_"," "); } }

        string NewStepName { get { return InitialStageExecuter.NewName.Replace("_"," "); } }

        void RenameStepsInFeature()
        {
            var FeatureFile = FileName.Replace("steps.cs", "feature");
            var FeatureContent = System.IO.File.ReadAllText(FeatureFile);
            var NewContent = FeatureContent.Replace(InitialStepName, NewStepName);

            System.IO.File.WriteAllText(FeatureFile, NewContent);
        }

    }
}
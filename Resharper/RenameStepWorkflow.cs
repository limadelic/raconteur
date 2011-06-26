using JetBrains.Application.Progress;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Refactorings.Rename;
using Raconteur.Helpers;

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

            ObjectFactory.NewRenameStep(FileName, InitialName, InitialStageExecuter.NewName)
                .Execute();

            return true;
        }
    }
}
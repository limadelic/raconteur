using System;
using JetBrains.Application.Progress;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Refactorings.Rename;
using JetBrains.Util;
using Raconteur.Helpers;

namespace Raconteur.Resharper
{
    public class RenameStepWorkflow : RenameWorkflow
    {
        public RenameStepWorkflow(ISolution Solution, string ActionId) 
            : base(Solution, ActionId)
        {
        }

        public override bool PostExecute(IProgressIndicator pi) 
        {
            try 
            {
                if (!base.PostExecute(pi)) return false;

                RunnerFileWatcher.Path = Solution.SolutionFilePath.Directory.FullPath;

                RunnerFileWatcher.OnFileChange(f =>
                    ObjectFactory.NewRenameStep
                    (
                        f.FeatureFileFromRunner(), 
                        InitialName, 
                        InitialStageExecuter.NewName
                    )
                    .Execute());

            } 
            catch (Exception e)
            {
                MessageBox.ShowInfo(e.ToString());
            }

            return true;
        }
    }
}
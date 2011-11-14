using System;
using System.Collections.Generic;
using JetBrains.ActionManagement;
using JetBrains.Application;
using JetBrains.Application.DataContext;
using JetBrains.Application.Progress;
using JetBrains.DataFlow;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.Bulbs;
using JetBrains.ReSharper.Feature.Services.CSharp.Bulbs;
using JetBrains.ReSharper.Intentions;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services;
using JetBrains.ReSharper.Refactorings.Workflow;
using JetBrains.TextControl;
using JetBrains.Util;

namespace Raconteur.Resharper
{
    [ContextAction(Group = "C#", Name = "Rename Step", Description = "do it", Priority = 15)]
    public class RenameStep : BulbItemImpl, IContextAction
    {
        readonly ICSharpContextActionDataProvider Provider;
        
        public RenameStep(ICSharpContextActionDataProvider Provider)
        {
            this.Provider = Provider;
        }
        
        public override string Text { get { return "Rename Step"; } }

        IMethodDeclaration Method;

        public bool IsAvailable(IUserDataHolder Cache)
        {
            Method = Provider.GetSelectedElement<IMethodDeclaration>(false, true);
 
            return Method != null;
        }

        ISolution Solution { get; set; }

        protected override Action<ITextControl> ExecutePsiTransaction(ISolution Solution, IProgressIndicator Progress)
        {
            this.Solution = Solution;

            try
            {
                ExecuteRenameStep();
            } 
            catch (Exception e)
            {
                MessageBox.ShowInfo(e.ToString());
            }

            return null;
        }

        IEnumerable<IDataRule> Rules
        {
            get
            {
                return DataRules
                    .AddRule("RenameStep", JetBrains.ReSharper.Psi.Services.DataConstants.DECLARED_ELEMENTS, Method.DeclaredElement.ToDeclaredElementsDataConstant())
                    .AddRule("RenameStep", JetBrains.TextControl.DataContext.DataConstants.TEXT_CONTROL, Provider.TextControl)
                    .AddRule("RenameStep", JetBrains.ProjectModel.DataContext.DataConstants.SOLUTION, Solution);
            }
        }

        void ExecuteRenameStep()
        {
            Lifetimes.Using(Lifetime => RefactoringActionUtil.ExecuteRefactoring
            (
                Shell.Instance.Components.ActionManager().DataContexts.Create(Lifetime, Rules), 
                new RenameStepWorkflow(Solution, null)
            ));
        }
    }
}
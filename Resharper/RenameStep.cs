using System;
using JetBrains.ActionManagement;
using JetBrains.Application;
using JetBrains.Application.DataContext;
using JetBrains.Application.Progress;
using JetBrains.DataFlow;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.Bulbs;
using JetBrains.ReSharper.Feature.Services.CSharp.Bulbs;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services;
using JetBrains.ReSharper.Refactorings.Rename;
using JetBrains.TextControl;
using JetBrains.Util;

namespace Raconteur.Resharper
{
    [ContextAction(Group = "C#", Name = "Rename Step", Description = "do it", Priority = 15)]
    public class RenameStep : SingleItemContextAction
    {
        public RenameStep(ICSharpContextActionDataProvider Provider) : base(Provider) { }

        public override string Text { get { return "Rename Step"; } }

        IMethodDeclaration Method;

        public override bool IsAvailable(IUserDataHolder Cache)
        {
            Method = Provider.GetSelectedElement<IMethodDeclaration>(false, true);
 
            return Method != null;
        }

        protected override Action<ITextControl> ExecutePsiTransaction(ISolution Solution, IProgressIndicator Progress)
        {
            try
            {
                var Rules = DataRules
                    .AddRule("ManualRenameRefactoringItem", JetBrains.ReSharper.Psi.Services.DataConstants.DECLARED_ELEMENTS, Method.DeclaredElement.ToDeclaredElementsDataConstant())
                    .AddRule("ManualRenameRefactoringItem", JetBrains.TextControl.DataContext.DataConstants.TEXT_CONTROL, Provider.TextControl)
                    .AddRule("ManualRenameRefactoringItem", JetBrains.ProjectModel.DataContext.DataConstants.SOLUTION, Solution);

                Lifetimes.Using(Lifetime => 
                    RenameRefactoringService.ExcuteRename(
                        Shell.Instance.Components.ActionManager()
                            .DataContexts.Create(Lifetime, Rules)));
            } 
            catch (Exception e)
            {
                MessageBox.ShowInfo(e.ToString());
            }

            return null;
        }
    }
}
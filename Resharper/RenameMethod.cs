using JetBrains.Annotations;
using JetBrains.ReSharper.Feature.Services.Bulbs;
using JetBrains.ReSharper.Feature.Services.CSharp.Bulbs;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace Raconteur.Resharper
{
    [UsedImplicitly]
    [ContextAction(Description = "Renames a Method.", Name = "Rename Method",
        Priority = -1, Group = "C#")]
    public class RenameMethod : ContextActionBase
    {
        public RenameMethod([NotNull] ICSharpContextActionDataProvider provider) : base(provider) {}

        public override bool IsAvailable(ITreeNode element)
        {
            return Method != null;
        }

        IMethodDeclaration Method
        {
            get { return Provider.GetSelectedElement<IMethodDeclaration>(false, true); }
        }

        protected override void Execute(ITreeNode element)
        {
            MessageBox.ShowInfo("getting there");
/*
            Lifetimes.Using(l => 
                RenameRefactoringService.ExcuteRename(
                    Shell.Instance.Components.ActionManager().DataContexts.Create(l, DataRules
                        .AddRule("ManualChangeNameFix", DataConstants.DECLARED_ELEMENTS, element.ToDeclaredElementsDataConstant())
                        .AddRule("ManualChangeNameFix", TextControl.DataContext.DataConstants.TEXT_CONTROL, textControl)
                        .AddRule("ManualChangeNameFix", ProjectModel.DataContext.DataConstants.SOLUTION, solution)
                        .AddRule("ManualChangeNameFix", RenameWorkflow.RenameDataProvider, new RenameDataProvider(mySuggestedName, element)))));          
*/
        }

        protected override string GetText() { return "rename"; }
    }
}
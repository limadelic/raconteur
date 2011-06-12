using JetBrains.Annotations;
using JetBrains.ReSharper.Feature.Services.Bulbs;
using JetBrains.ReSharper.Intentions.CSharp.DataProviders;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;
using JetBrains.ReSharper.Refactorings.Rename;

namespace Raconteur.Resharper
{
    [UsedImplicitly]
    [ContextAction(Description = "Renames a Method.", Name = "Rename Method",
        Priority = -1, Group = "C#")]
    public class RenameMethod : ContextActionBase
    {
        public RenameMethod([NotNull] ICSharpContextActionDataProvider provider) : base(provider) {}

        public override bool IsAvailable(IElement element)
        {
            return Method != null;
        }

        IMethodDeclaration Method
        {
            get { return Provider.GetSelectedElement<IMethodDeclaration>(false, true); }
        }

        protected override void Execute(IElement element)
        {
/*
            MessageBox.ShowInfo("getting there");
            new RenameAction().Execute(this, );
*/
        }

        protected override string GetText() { return "rename"; }
    }
}
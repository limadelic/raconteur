using JetBrains.Annotations;
using JetBrains.ReSharper.Feature.Services.Bulbs;
using JetBrains.ReSharper.Feature.Services.CSharp.Bulbs;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace Raconteur.Resharper
{
    [UsedImplicitly]
    [ContextAction(Description = "Makes a method virtual.", Name = "Make virtual",
        Priority = -1, Group = "C#")]
    public class MakeVirtual : ContextActionBase
    {
        public MakeVirtual([NotNull] ICSharpContextActionDataProvider provider) : base(provider) {}

        public override bool IsAvailable(ITreeNode element)
        {
            var item = Provider.GetSelectedElement<IMethodDeclaration>(false, true);
 
            if (item != null)
            {
                var accessRights = item.GetAccessRights();
 
                if (accessRights == AccessRights.PUBLIC && !item.IsStatic && !item.IsVirtual && !item.IsOverride)
                {
                    return true;
                }
            }
            return false;
        }

        protected override void Execute(ITreeNode element)
        {
            var method = Provider.GetSelectedElement<IMethodDeclaration>(false, true);
 
            if (method != null)
            {
                method.SetVirtual(true);
            }
        }

        protected override string GetText() { return "make virtual"; }
    }
}
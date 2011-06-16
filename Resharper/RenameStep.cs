using System;
using JetBrains.Application.Progress;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.Bulbs;
using JetBrains.ReSharper.Feature.Services.CSharp.Bulbs;
using JetBrains.ReSharper.Intentions;
using JetBrains.TextControl;
using JetBrains.Util;

namespace Raconteur.Resharper
{
    public abstract class CSharpOneItemContextAction : BulbItemImpl, IContextAction
    {
        protected readonly ICSharpContextActionDataProvider Provider;

        protected CSharpOneItemContextAction(ICSharpContextActionDataProvider Provider) { this.Provider = Provider; }

        public abstract bool IsAvailable(IUserDataHolder cache);

        public new IBulbItem[] Items { get { return new[] {this}; } }
    }

    [ContextAction(Group = "C#", Name = "Rename Step", Description = "do it", Priority = 15)]
    public class RenameStep : CSharpOneItemContextAction
    {
        public RenameStep(ICSharpContextActionDataProvider provider) : base(provider) { }

        public override string Text { get { return "Rename Step"; } }

        public override bool IsAvailable(IUserDataHolder cache) { return true; }

        protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
        {
            MessageBox.ShowInfo("getting there");
            return null;
        }
    }
}
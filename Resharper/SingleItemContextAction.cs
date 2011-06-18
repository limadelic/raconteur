using JetBrains.ReSharper.Feature.Services.Bulbs;
using JetBrains.ReSharper.Feature.Services.CSharp.Bulbs;
using JetBrains.ReSharper.Intentions;
using JetBrains.Util;

namespace Raconteur.Resharper
{
    public abstract class SingleItemContextAction : BulbItemImpl, IContextAction
    {
        protected readonly ICSharpContextActionDataProvider Provider;

        protected SingleItemContextAction(ICSharpContextActionDataProvider Provider)
        {
            this.Provider = Provider;
        }

        public abstract bool IsAvailable(IUserDataHolder Cache);

        public new IBulbItem[] Items
        {
            get { return new[] {this}; }
        }
    }
}
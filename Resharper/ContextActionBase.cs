  using JetBrains.Annotations;
  using JetBrains.Application;
  using JetBrains.DocumentModel;
  using JetBrains.ProjectModel;
  using JetBrains.ReSharper.Feature.Services.Bulbs;
  using JetBrains.ReSharper.Intentions.CSharp.DataProviders;
  using JetBrains.ReSharper.Psi;
  using JetBrains.ReSharper.Psi.Tree;
  using JetBrains.TextControl;
  using JetBrains.Util;

namespace Raconteur.Resharper
{
    public abstract class ContextActionBase : IBulbItem, IContextAction
    {
        readonly IContextActionDataProvider provider;
        bool startTransaction = true;

        protected ContextActionBase([NotNull] ICSharpContextActionDataProvider provider) { this.provider = provider; }

        string IBulbItem.Text { get { return GetText(); } }

        [NotNull]
        public IContextActionDataProvider Provider { get { return provider; } }

        protected ISolution Solution { get { return provider.Solution; } }

        protected bool StartTransaction { get { return startTransaction; } set { startTransaction = value; } }

        protected ITextControl TextControl { get { return provider.TextControl; } }

        public abstract bool IsAvailable(IElement element);

        public bool IsAvailable(IUserDataHolder cache) { return IsAvailableInternal(); }

        void IBulbItem.Execute(ISolution solution, ITextControl textControl)
        {
            if (Solution != solution || TextControl != textControl) throw new System.InvalidOperationException();

            var element = provider.SelectedElement;
            if (element == null) return;

            if (StartTransaction) Modify(element);
            else Execute(element);

            PostExecute();
        }

        [NotNull]
        protected ModificationCookie EnsureWritable()
        {
            if (Solution != null) return DocumentManager.GetInstance(Solution).EnsureWritable(TextControl.Document);

            return new ModificationCookie(EnsureWritableResult.FAILURE);
        }

        protected abstract void Execute(IElement element);

        protected void ExecuteInternal(params object[] param)
        {
            var element = param[0] as IElement;

            if (startTransaction) Modify(element);
            else Execute(element);

            PostExecute();
        }

        protected abstract string GetText();

        protected bool IsAvailableInternal()
        {
            IElement element = provider.SelectedElement;
            if (element == null) return false;

            return IsAvailable(element);
        }

        protected virtual void PostExecute() { }

        public IBulbItem[] Items { get { return new[] {this}; } }

        void Modify(IElement element)
        {
            PsiManager psiManager = PsiManager.GetInstance(Solution);
            if (psiManager == null) return;

            using (var cookie = EnsureWritable())
            {
                if (cookie.EnsureWritableResult != EnsureWritableResult.SUCCESS) return;

                using (CommandCookie.Create(string.Format("Context Action {0}", GetText()))) 
                    psiManager.DoTransaction(() => Execute(element));
            }
        }

    }
}
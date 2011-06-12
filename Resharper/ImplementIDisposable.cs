using JetBrains.Annotations;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.Bulbs;
using JetBrains.ReSharper.Intentions.CSharp.DataProviders;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Caches;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace Raconteur.Resharper
{
    [UsedImplicitly]
    [ContextAction(Description = "Implement IDisposable interface.", Name = "Implement IDisposable",
        Priority = -1, Group = "C#")]
    public class ImplementIDisposable : ContextActionBase
    {
        public ImplementIDisposable([NotNull] ICSharpContextActionDataProvider provider) : base(provider) { }

        public override bool IsAvailable(IElement element)
        {
            var classDeclaration = element.ToTreeNode().Parent as IClassDeclaration;
            if (classDeclaration == null) return false;

            foreach (var type in classDeclaration.DeclaredElement.GetSuperTypes())
                if (type.GetLongPresentableName(element.Language) == "System.IDisposable") 
                    return false;

            return true;
        }

        protected override void Execute(IElement element)
        {
            var classDeclaration = element.ToTreeNode().Parent as IClassDeclaration;
            if (classDeclaration == null) return;

            using (ModificationCookie cookie = EnsureWritable())
            {
                if (cookie.EnsureWritableResult != EnsureWritableResult.SUCCESS) return;

                Execute(Solution, classDeclaration);
            }
        }

        protected override string GetText() { return "Implement IDisposable"; }

        static void AddDestructor(IClassDeclaration classDeclaration, CSharpElementFactory factory)
        {
            const string code = @"~Disposable() {
          DisposeObject(false);
        }";

            var memberDeclaration = factory.CreateTypeMemberDeclaration(code) as IClassMemberDeclaration;

            classDeclaration.AddClassMemberDeclaration(memberDeclaration);
        }

        static void AddDisposeMethod(IClassDeclaration classDeclaration, CSharpElementFactory factory)
        {
            const string code =
                @"
        public void Dispose() {
          DisposeObject(true);
          GC.SuppressFinalize(this);
        }";

            var memberDeclaration = factory.CreateTypeMemberDeclaration(code) as IClassMemberDeclaration;

            classDeclaration.AddClassMemberDeclaration(memberDeclaration);
        }

        static void AddDisposeObjectMethod(IClassDeclaration classDeclaration, CSharpElementFactory factory)
        {
            const string code =
                @"
        void DisposeObject(bool disposing) {
          if(_disposed) {
            return;
          }
          if (disposing) {
            // Dispose managed resources.
          }
          // Dispose unmanaged resources.
          _disposed = true;         
        }";

            var memberDeclaration = factory.CreateTypeMemberDeclaration(code) as IClassMemberDeclaration;

            classDeclaration.AddClassMemberDeclaration(memberDeclaration);
        }

        static void AddField(IClassDeclaration classDeclaration, CSharpElementFactory factory)
        {
            const string code = @"
        bool _disposed;
        ";

            var memberDeclaration = factory.CreateTypeMemberDeclaration(code) as IClassMemberDeclaration;

            classDeclaration.AddClassMemberDeclaration(memberDeclaration);
        }

        static void AddInterface(ISolution solution, IClassDeclaration classDeclaration)
        {
            IDeclarationsScope scope = DeclarationsScopeFactory.SolutionScope(solution, true);
            IDeclarationsCache cache = PsiManager.GetInstance(solution).GetDeclarationsCache(scope, true);

            ITypeElement typeElement = cache.GetTypeElementByCLRName("System.IDisposable");
            if (typeElement == null) return;

            IDeclaredType declaredType = TypeFactory.CreateType(typeElement);

            classDeclaration.AddSuperInterface(declaredType, false);
        }

        static void Execute(ISolution solution, IClassDeclaration classDeclaration)
        {
            CSharpElementFactory factory = CSharpElementFactory.GetInstance(classDeclaration.GetPsiModule());
            if (factory == null) return;

            AddInterface(solution, classDeclaration);
            AddField(classDeclaration, factory);
            AddDestructor(classDeclaration, factory);
            AddDisposeMethod(classDeclaration, factory);
            AddDisposeObjectMethod(classDeclaration, factory);
        }
    }
}
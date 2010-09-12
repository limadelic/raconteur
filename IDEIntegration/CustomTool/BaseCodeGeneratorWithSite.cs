using System;
using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Designer.Interfaces;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using IServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;

namespace Raconteur.IDEIntegration
{
    [ComVisible(true)]
    [Guid("B10546A6-B06B-4305-8D7C-A84BBF82AB52")]
    public abstract class BaseCodeGeneratorWithSite : BaseCodeGenerator, IObjectWithSite
    {
        object Site;
        CodeDomProvider CodeDomProvider;

        void IObjectWithSite.SetSite(object PUnkSite)
        {
            Site = PUnkSite;
            CodeDomProvider = null;
            ServiceProvider = null;
        }

        void IObjectWithSite.GetSite(ref Guid Riid, out IntPtr PpvSite)
        {
            if (Site == null) throw new COMException("object is not sited", VSConstants.E_FAIL);

            var PUnknownPointer = Marshal.GetIUnknownForObject(Site);

            IntPtr IntPointer;

            Marshal.QueryInterface(PUnknownPointer, ref Riid, out IntPointer);

            if (IntPointer == IntPtr.Zero) throw new COMException("site does not support requested interface", VSConstants.E_NOINTERFACE);

            PpvSite = IntPointer;
        }

        ServiceProvider ServiceProvider;
        ServiceProvider SiteServiceProvider
        {
            get { return ServiceProvider ?? (ServiceProvider = new ServiceProvider(Site as IServiceProvider)); }
        }

        protected object GetService(Type serviceType) { return SiteServiceProvider.GetService(serviceType); }

        protected virtual CodeDomProvider GetCodeProvider()
        {
            if (CodeDomProvider == null)
            {
                var Provider = GetService(typeof(SVSMDCodeDomProvider)) as IVSMDCodeDomProvider;

                CodeDomProvider = (Provider != null) ? 
                    Provider.CodeDomProvider as CodeDomProvider :
                    CodeDomProvider.CreateProvider("C#");
            }

            return CodeDomProvider;
        }

        public static bool Failed(int HResult) { return (HResult < 0); }

        protected IVsHierarchy VsHierarchy { get { return GetService(typeof (IVsHierarchy)) as IVsHierarchy; } }

        protected DTE DTE
        {
            get
            {
                return Get<DTE>(
                    typeof(DTE).GUID, 
                    typeof(DTE).GUID);
            }
        }

        protected IVsErrorList ErrorList
        {
            get
            {
                return Get<IVsErrorList>(
                    typeof(SVsErrorList).GUID, 
                    typeof(IVsErrorList).GUID);
            }
        }

        public Project CurrentProject
        {
            get { return GetProjectForSourceFile(DTE); }
        }

        readonly Exception NoDTE = new InvalidOperationException("Unable to detect current project.");

        Project GetProjectForSourceFile(DTE Dte)
        {
            if (Dte == null) throw NoDTE;

            var ProjectItem = Dte.Solution.FindProjectItem(CodeFilePath);

            if (ProjectItem == null) throw NoDTE;
            
            return ProjectItem.ContainingProject;
        }

        T Get<T>(Guid ServiceId, Guid Id)
        {
            var Service = GetService(typeof (IVsHierarchy)) as IVsHierarchy;
            if (Service == null) return default(T);

            IServiceProvider CurrentServiceProvider;
            if (Failed(Service.GetSite(out CurrentServiceProvider)) 
                && CurrentServiceProvider != null) return default(T);

            IntPtr Result;

            ErrorHandler.ThrowOnFailure(
                CurrentServiceProvider.QueryService(ref ServiceId, ref Id, out Result));

            if (Result == IntPtr.Zero) return default(T);
                
            return (T) Marshal.GetObjectForIUnknown(Result);
        }

    }
}
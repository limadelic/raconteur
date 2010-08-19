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
        object site;
        ServiceProvider serviceProvider;
        CodeDomProvider codeDomProvider;

        void IObjectWithSite.SetSite(object pUnkSite)
        {
            site = pUnkSite;
            codeDomProvider = null;
            serviceProvider = null;
        }

        void IObjectWithSite.GetSite(ref Guid riid, out IntPtr ppvSite)
        {
            if (site == null) throw new COMException("object is not sited", VSConstants.E_FAIL);
            var pUnknownPointer = Marshal.GetIUnknownForObject(site);
            IntPtr intPointer;
            Marshal.QueryInterface(pUnknownPointer, ref riid, out intPointer);
            if (intPointer == IntPtr.Zero) throw new COMException("site does not support requested interface", VSConstants.E_NOINTERFACE);
            ppvSite = intPointer;
        }

        ServiceProvider SiteServiceProvider
        {
            get { return serviceProvider ?? (serviceProvider = new ServiceProvider(site as IServiceProvider)); }
        }

        protected object GetService(Type serviceType) { return SiteServiceProvider.GetService(serviceType); }

        protected virtual CodeDomProvider GetCodeProvider()
        {
            if (codeDomProvider == null)
            {
                var provider = GetService(typeof (SVSMDCodeDomProvider)) as IVSMDCodeDomProvider;
                if (provider != null) codeDomProvider = provider.CodeDomProvider as CodeDomProvider;
                else codeDomProvider = CodeDomProvider.CreateProvider("C#");
            }
            return codeDomProvider;
        }

        public static bool Failed(int hr) { return (hr < 0); }

        protected DTE Dte
        {
            get
            {
                DTE objectForIUnknown = null;
                var service = GetService(typeof (IVsHierarchy)) as IVsHierarchy;
                if (service != null)
                {
                    IServiceProvider ppSP;
                    if (!Failed(service.GetSite(out ppSP)) && (ppSP != null))
                    {
                        var gUID = typeof (DTE).GUID;
                        IntPtr zero;
                        ErrorHandler.ThrowOnFailure(ppSP.QueryService(ref gUID, ref gUID, out zero));
                        if (zero != IntPtr.Zero) objectForIUnknown = Marshal.GetObjectForIUnknown(zero) as DTE;
                    }
                }
                return objectForIUnknown;
            }
        }

        protected IVsHierarchy VsHierarchy { get { return GetService(typeof (IVsHierarchy)) as IVsHierarchy; } }

        protected IVsErrorList ErrorList
        {
            get
            {
                IVsErrorList objectForIUnknown = null;
                var service = GetService(typeof (IVsHierarchy)) as IVsHierarchy;
                if (service != null)
                {
                    IServiceProvider ppSP;
                    if (!Failed(service.GetSite(out ppSP)) && (ppSP != null))
                    {
                        var gUID = typeof (SVsErrorList).GUID;
                        var riid = typeof (IVsErrorList).GUID;
                        IntPtr zero;
                        ErrorHandler.ThrowOnFailure(ppSP.QueryService(ref gUID, ref riid, out zero));
                        if (zero != IntPtr.Zero) objectForIUnknown = Marshal.GetObjectForIUnknown(zero) as IVsErrorList;
                    }
                }
                return objectForIUnknown;
            }
        }

        public EnvDTE.Project CurrentProject
        {
            get
            {
                return GetProjectForSourceFile(Dte);
            }
        }

        EnvDTE.Project GetProjectForSourceFile(DTE dte)
        {
            if (dte != null)
            {
                var prjItem = dte.Solution.FindProjectItem(CodeFilePath);
                if (prjItem != null) return prjItem.ContainingProject;
            }
            throw new InvalidOperationException("Unable to detect current project.");
        }

        protected override void AfterCodeGenerated(bool error)
        {
            base.AfterCodeGenerated(error);

            if (!error) return;
            var errorList = ErrorList;
            if (errorList == null) return;

            errorList.BringToFront();
            errorList.ForceShowErrors();
        }
    }
}
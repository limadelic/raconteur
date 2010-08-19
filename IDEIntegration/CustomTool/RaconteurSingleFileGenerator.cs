using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace Raconteur.IDEIntegration
{
    [ComVisible(true)]
    [Guid("747D47AC-4681-4B88-8218-623AC7C70145")]
    [ProvideObject(typeof(RaconteurSingleFileGenerator))]
    public class RaconteurSingleFileGenerator : RaconteurSingleFileGeneratorBase
    {
        protected override void RefreshMsTestWindow()
        {
            //the automatic refresh of the test window causes problems in VS2010 -> skip
        }
    }
}

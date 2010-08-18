using System;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace Raconteur.IDEIntegration
{
    [ComVisible(true)]
    [Guid("AA602DCE-8B40-4C5A-BD46-B037C63C3813")]
    public abstract class BaseCodeGenerator : IVsSingleFileGenerator
    {
        IVsGeneratorProgress codeGeneratorProgress;
        string codeFileNameSpace = String.Empty;
        string codeFilePath = String.Empty;

        public string CodeFileNameSpace { get { return codeFileNameSpace; } }

        public string CodeFilePath { get { return codeFilePath; } }

        public IVsGeneratorProgress CodeGeneratorProgress { get { return codeGeneratorProgress; } }

        int IVsSingleFileGenerator.DefaultExtension(out string pbstrDefaultExtension)
        {
            pbstrDefaultExtension = GetDefaultExtension();
            return VSConstants.S_OK;
        }

        protected virtual string GetMessage(Exception ex)
        {
            if (ex.InnerException == null) return ex.Message;

            return ex.Message + " -> " + GetMessage(ex.InnerException);
        }

        int IVsSingleFileGenerator.Generate(string wszInputFilePath, string bstrInputFileContents,
            string wszDefaultNamespace, IntPtr[] rgbOutputFileContents, out uint pcbOutput,
            IVsGeneratorProgress pGenerateProgress)
        {
            codeFilePath = wszInputFilePath;
            codeFileNameSpace = wszDefaultNamespace;
            codeGeneratorProgress = pGenerateProgress;
            byte[] bytes;
            try
            {
                BeforeCodeGenerated();
                bytes = Encoding.UTF8.GetBytes(GenerateCode(bstrInputFileContents));
                AfterCodeGenerated(false);
            } catch (Exception ex)
            {
                var message = GenerateError(pGenerateProgress, ex);
                AfterCodeGenerated(true);
                bytes = Encoding.UTF8.GetBytes(message);
            }

            var outputLength = bytes.Length;
            rgbOutputFileContents[0] = Marshal.AllocCoTaskMem(outputLength);
            Marshal.Copy(bytes, 0, rgbOutputFileContents[0], outputLength);
            pcbOutput = (uint) outputLength;

            RefreshMsTestWindow();

            return VSConstants.S_OK;
        }

        protected virtual void RefreshMsTestWindow()
        {
            //refreshCmdGuid,cmdID is the command id of refresh command.
            var refreshCmdGuid = new Guid("{B85579AA-8BE0-4C4F-A850-90902B317571}");
            var cmdTarget = Package.GetGlobalService(typeof (SUIHostCommandDispatcher)) as IOleCommandTarget;
            const uint cmdID = 13109;
            if (cmdTarget != null)
                cmdTarget.Exec(ref refreshCmdGuid, cmdID, (uint) OLECMDEXECOPT.OLECMDEXECOPT_DONTPROMPTUSER, IntPtr.Zero,
                    IntPtr.Zero);
        }

        protected virtual string GenerateError(IVsGeneratorProgress pGenerateProgress, Exception ex)
        {
            var message = GetMessage(ex);
            pGenerateProgress.GeneratorError(0, 4, message, 0xFFFFFFFF, 0xFFFFFFFF);
            return message;
        }

        protected virtual void BeforeCodeGenerated()
        {
            //nop
        }

        protected virtual void AfterCodeGenerated(bool error)
        {
            //nop
        }

        protected abstract string GetDefaultExtension();
        protected abstract string GenerateCode(string inputFileContent);
    }
}
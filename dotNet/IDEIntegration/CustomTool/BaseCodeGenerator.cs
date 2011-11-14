using System;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Raconteur.Helpers;

namespace Raconteur.IDEIntegration
{
    [ComVisible(true)]
    [Guid("AA602DCE-8B40-4C5A-BD46-B037C63C3813")]
    public abstract class BaseCodeGenerator : IVsSingleFileGenerator
    {
        protected abstract string GetDefaultExtension();

        public abstract string GenerateCode(string inputFileContent);

        IVsGeneratorProgress codeGeneratorProgress;
        string codeFileNameSpace = String.Empty;
        string codeFilePath = String.Empty;

        public string CodeFileNameSpace 
        { 
            get
            {
                return codeFileNameSpace.IsEmpty() ? null : codeFileNameSpace;
            } 
        }

        public string CodeFilePath
        {
            get { return codeFilePath; } 
            set { codeFilePath = value; }
        }

        public IVsGeneratorProgress CodeGeneratorProgress { get { return codeGeneratorProgress; } }

        int IVsSingleFileGenerator.DefaultExtension(out string pbstrDefaultExtension)
        {
            pbstrDefaultExtension = GetDefaultExtension();
            return VSConstants.S_OK;
        }

        int IVsSingleFileGenerator.Generate(string wszInputFilePath, string bstrInputFileContents,
            string wszDefaultNamespace, IntPtr[] rgbOutputFileContents, out uint pcbOutput,
            IVsGeneratorProgress pGenerateProgress)
        {
            codeFilePath = wszInputFilePath;
            codeFileNameSpace = wszDefaultNamespace;
            codeGeneratorProgress = pGenerateProgress;

            string Output;
            try
            {
                Output = GenerateCode(bstrInputFileContents);
            } 
            catch (Exception e)
            {
                Output = GenerateError(pGenerateProgress, e);
            }

            var OutputBytes = Encoding.UTF8.GetBytes(Output);
            var OutputLength = OutputBytes.Length;

            rgbOutputFileContents[0] = Marshal.AllocCoTaskMem(OutputLength);
            Marshal.Copy(OutputBytes, 0, rgbOutputFileContents[0], OutputLength);
            pcbOutput = (uint) OutputLength;

            return VSConstants.S_OK;
        }

        protected virtual string GenerateError(IVsGeneratorProgress pGenerateProgress, Exception ex)
        {
            var Message = ex.ToString();

            pGenerateProgress.GeneratorError(0, 4, Message, 0xFFFFFFFF, 0xFFFFFFFF);

            return Message;
        }
    }
}
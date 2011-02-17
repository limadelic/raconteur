using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Utilities;

namespace Raconteur.IDEIntegration.Intellisense
{
    [Export(typeof(IVsTextViewCreationListener))]
    [Name("Completion Handler")]
    [ContentType("Raconteur")]
    [TextViewRole(PredefinedTextViewRoles.Editable)]
    internal class CompletionHandlerProvider : IVsTextViewCreationListener
    {
        [Import] internal IVsEditorAdaptersFactoryService AdapterService = null;

        [Import]
        internal ICompletionBroker CompletionBroker { get; set; }

        [Import]
        internal SVsServiceProvider ServiceProvider { get; set; }
        
        public void VsTextViewCreated(IVsTextView textViewAdapter)
        {
            ITextView textView = AdapterService.GetWpfTextView(textViewAdapter);
            if (textView == null) return;

            Func<CompletionHandler> createHandler = () => 
                new CompletionHandler(textViewAdapter, textView, this);

            textView.Properties.GetOrCreateSingletonProperty(createHandler);
        }
    }

    internal class CompletionHandler : IOleCommandTarget
    {
        private IOleCommandTarget nextHandler;
        private ITextView textView;
        private CompletionHandlerProvider provider;
        private ICompletionSession session;

        public CompletionHandler(IVsTextView textViewAdapter, ITextView textView, CompletionHandlerProvider provider)
        {
            this.textView = textView;
            this.provider = provider;

            //add the command to the command chain
            textViewAdapter.AddCommandFilter(this, out nextHandler);
        }

        public int QueryStatus(ref Guid pguidCmdGroup, uint cCmds, OLECMD[] prgCmds, IntPtr pCmdText)
        {
            return nextHandler.QueryStatus(ref pguidCmdGroup, cCmds, prgCmds, pCmdText);
        }

        public int Exec(ref Guid pguidCmdGroup, uint nCmdID, uint nCmdexecopt, IntPtr pvaIn, IntPtr pvaOut)
        {
            //TODO
            return 0;
        }
    }
}
using System;
using System.ComponentModel.Composition;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
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
        [Import] internal IVsEditorAdaptersFactoryService AdapterService;

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
        private readonly IOleCommandTarget nextHandler;
        private readonly ITextView textView;
        private readonly CompletionHandlerProvider provider;
        private ICompletionSession session;
        private CommandInfo commandInfo;

        public CompletionHandler(IVsTextView textViewAdapter, ITextView textView, CompletionHandlerProvider provider)
        {
            this.textView = textView;
            this.provider = provider;

            //add the command to the command chain
            textViewAdapter.AddCommandFilter(this, out nextHandler);
        }

        private char TypedCharacter
        {
            get
            {
                return commandInfo.CommandGroup == VSConstants.VSStd2K &&
                       commandInfo.CommandId == (uint)VSConstants.VSStd2KCmdID.TYPECHAR
                           ? (char)(ushort)Marshal.GetObjectForNativeVariant(commandInfo.PvaIn)
                           : char.MinValue;
            }
        }

        private bool IsCommitCharacter
        {
            get
            {
                return commandInfo.CommandId == (uint) VSConstants.VSStd2KCmdID.RETURN
                       || commandInfo.CommandId == (uint) VSConstants.VSStd2KCmdID.TAB;
            }
        }

        private bool IsDeletionCharacter
        {
            get
            {
                return commandInfo.CommandId == (uint) VSConstants.VSStd2KCmdID.BACKSPACE
                       || commandInfo.CommandId == (uint)VSConstants.VSStd2KCmdID.DELETE;
            }
        }

        private bool IsSelection { get { return session != null && !session.IsDismissed; } }
        private bool IsFullySelected { get { return session.SelectedCompletionSet.SelectionStatus.IsSelected; } }
        private bool NoActiveSession { get { return session == null || session.IsDismissed; } }

        public int QueryStatus(ref Guid pguidCmdGroup, uint cCmds, OLECMD[] prgCmds, IntPtr pCmdText)
        {
            return nextHandler.QueryStatus(ref pguidCmdGroup, cCmds, prgCmds, pCmdText);
        }

        public int Exec(ref Guid pguidCmdGroup, uint nCmdID, uint nCmdexecopt, IntPtr pvaIn, IntPtr pvaOut)
        {
            commandInfo = new CommandInfo
            {
                CommandGroup = pguidCmdGroup,
                CommandId = nCmdID,
                ExecOpt = nCmdexecopt,
                PvaIn = pvaIn,
                PvaOut = pvaOut,
            };

            if (VsShellUtilities.IsInAutomationFunction(provider.ServiceProvider))
                return PassCommandAlong();

            if (IsCommitCharacter && IsSelection)
            {
                if (IsFullySelected)
                {
                    session.Commit();
                    return VSConstants.S_OK;
                }
                session.Dismiss();
            }

            var retVal = PassCommandAlong();
            var handled = false;
            if (!TypedCharacter.Equals(char.MinValue) && char.IsLetterOrDigit(TypedCharacter))
            {
                if (NoActiveSession)
                    TriggerCompletion();
                    
                session.Filter();
                handled = true;
            }
            else if (IsDeletionCharacter)
            {
                if (IsSelection)
                    session.Filter();
                handled = true;
            }
            return handled ? VSConstants.S_OK : retVal;
        }

        private int PassCommandAlong()
        {
            var commandGroup = commandInfo.CommandGroup;
            return nextHandler.Exec(ref commandGroup, commandInfo.CommandId, commandInfo.ExecOpt, commandInfo.PvaIn, commandInfo.PvaOut);
        }

        private void TriggerCompletion()
        {
            //the caret must be in a non-projection location 
            SnapshotPoint? caretPoint =
            textView.Caret.Position.Point.GetPoint(
            textBuffer => (!textBuffer.ContentType.IsOfType("projection")), PositionAffinity.Predecessor);
            
            if (!caretPoint.HasValue) return;

            session = provider.CompletionBroker.CreateCompletionSession
         (textView,
                caretPoint.Value.Snapshot.CreateTrackingPoint(caretPoint.Value.Position, PointTrackingMode.Positive),
                true);

            //subscribe to the Dismissed event on the session 
            session.Dismissed += OnSessionDismissed;
            session.Start();

            return;
        }

        private void OnSessionDismissed(object sender, EventArgs e)
        {
            session.Dismissed -= OnSessionDismissed;
            session = null;
        }

        class CommandInfo
        {
            public Guid CommandGroup { get; set; }
            public uint CommandId { get; set; }
            public uint ExecOpt { get; set; }
            public IntPtr PvaIn { get; set; }
            public IntPtr PvaOut { get; set; }
        }
    }
}
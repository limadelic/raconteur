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

// ReSharper disable RedundantDefaultFieldInitializer
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
        
        public void VsTextViewCreated(IVsTextView TextViewAdapter)
        {
            ITextView TextView = AdapterService.GetWpfTextView(TextViewAdapter);
            if (TextView == null) return;

            TextView.Properties.GetOrCreateSingletonProperty(() => 
                new CompletionHandler(TextViewAdapter, TextView, this));
        }
    }

    internal class CompletionHandler : IOleCommandTarget
    {
        private readonly IOleCommandTarget NextHandler;
        private readonly ITextView TextView;
        private readonly CompletionHandlerProvider Provider;
        private ICompletionSession Session;
        private CommandInfo commandInfo;

        public CompletionHandler(IVsTextView textViewAdapter, ITextView textView, CompletionHandlerProvider provider)
        {
            this.TextView = textView;
            this.Provider = provider;

            //add the command to the command chain
            textViewAdapter.AddCommandFilter(this, out NextHandler);
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

        private bool IsSelection { get { return Session != null && !Session.IsDismissed; } }
        private bool IsFullySelected { get { return Session.SelectedCompletionSet.SelectionStatus.IsSelected; } }
        private bool NoActiveSession { get { return Session == null || Session.IsDismissed; } }

        public int QueryStatus(ref Guid pguidCmdGroup, uint cCmds, OLECMD[] prgCmds, IntPtr pCmdText)
        {
            return NextHandler.QueryStatus(ref pguidCmdGroup, cCmds, prgCmds, pCmdText);
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

            if (VsShellUtilities.IsInAutomationFunction(Provider.ServiceProvider))
                return PassCommandAlong();

            if (IsCommitCharacter && IsSelection)
            {
                if (IsFullySelected)
                {
                    Session.Commit();
                    return VSConstants.S_OK;
                }
                Session.Dismiss();
            }

            var retVal = PassCommandAlong();
            var handled = false;
            if (!TypedCharacter.Equals(char.MinValue) && char.IsLetterOrDigit(TypedCharacter))
            {
                if (NoActiveSession)
                    TriggerCompletion();
                    
                Session.Filter();
                handled = true;
            }
            else if (IsDeletionCharacter)
            {
                if (IsSelection)
                    Session.Filter();
                handled = true;
            }
            return handled ? VSConstants.S_OK : retVal;
        }

        private int PassCommandAlong()
        {
            var commandGroup = commandInfo.CommandGroup;
            return NextHandler.Exec(ref commandGroup, commandInfo.CommandId, commandInfo.ExecOpt, commandInfo.PvaIn, commandInfo.PvaOut);
        }

        private void TriggerCompletion()
        {
            //the caret must be in a non-projection location 
            SnapshotPoint? caretPoint =
            TextView.Caret.Position.Point.GetPoint(
            textBuffer => (!textBuffer.ContentType.IsOfType("projection")), PositionAffinity.Predecessor);
            
            if (!caretPoint.HasValue) return;

            Session = Provider.CompletionBroker.CreateCompletionSession
         (TextView,
                caretPoint.Value.Snapshot.CreateTrackingPoint(caretPoint.Value.Position, PointTrackingMode.Positive),
                true);

            //subscribe to the Dismissed event on the session 
            Session.Dismissed += OnSessionDismissed;
            Session.Start();

            return;
        }

        private void OnSessionDismissed(object sender, EventArgs e)
        {
            Session.Dismissed -= OnSessionDismissed;
            Session = null;
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
// ReSharper restore RedundantDefaultFieldInitializer
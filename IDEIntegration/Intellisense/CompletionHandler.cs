using System;
using System.ComponentModel.Composition;
using System.Runtime.InteropServices;
using System.Windows;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Utilities;
using Raconteur.Helpers;

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

        public CompletionHandler(IVsTextView TextViewAdapter, ITextView TextView, CompletionHandlerProvider Provider)
        {
            this.TextView = TextView;
            this.Provider = Provider;

            //add the command to the command chain
            TextViewAdapter.AddCommandFilter(this, out NextHandler);
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
            return QueryStatusGotoDefinition(pguidCmdGroup, prgCmds) ? VSConstants.S_OK :
                NextHandler.QueryStatus(ref pguidCmdGroup, cCmds, prgCmds, pCmdText);
        }

        bool QueryStatusGotoDefinition(Guid pguidCmdGroup, OLECMD[] prgCmds)
        {
            if (pguidCmdGroup != VSConstants.GUID_VSStandardCommandSet97) return false;

            switch ((VSConstants.VSStd97CmdID) prgCmds[0].cmdID)
            {
                case VSConstants.VSStd97CmdID.GotoDefn:
                    prgCmds[0].cmdf = (uint) OLECMDF.OLECMDF_ENABLED | (uint) OLECMDF.OLECMDF_SUPPORTED;
                    return true;
            }

            return false;
        }

        public int Exec(ref Guid pguidCmdGroup, uint nCmdID, uint nCmdexecopt, IntPtr pvaIn, IntPtr pvaOut)
        {
            SetUpCommandInfo(pguidCmdGroup, nCmdID, nCmdexecopt, pvaIn, pvaOut);

            if (pguidCmdGroup == VSConstants.GUID_VSStandardCommandSet97)
                switch ((VSConstants.VSStd97CmdID)nCmdID)
                {
                    case VSConstants.VSStd97CmdID.GotoDefn:
                        MessageBox.Show("going to impl of ... " + 
                            TextView.Caret.Position.BufferPosition.GetContainingLine().GetText());
                        return VSConstants.S_OK;
                }
            
            if (VsShellUtilities.IsInAutomationFunction(Provider.ServiceProvider))
                return PassCommandAlong;

            if (IsCommitCharacter && IsSelection)
            {
                if (IsFullySelected)
                {
                    Session.Commit();
                    return VSConstants.S_OK;
                }
                Session.Dismiss();
            }

            var Result = PassCommandAlong;

            return LetterOrDigit || DeletionCharacter ? 
                VSConstants.S_OK : 
                Result;
        }

        bool LetterOrDigit
        {
            get
            {
                if (!char.IsLetterOrDigit(TypedCharacter)) return false;

                if (NoActiveSession) TriggerCompletion();

                Session.Filter();

                return true;
            } 
        }

        bool DeletionCharacter
        {
            get
            {
                if (!IsDeletionCharacter) return false;

                if (IsSelection) Session.Filter();

                return true;
            } 
        }

        void SetUpCommandInfo(Guid pguidCmdGroup, uint nCmdID, uint nCmdexecopt, IntPtr pvaIn, IntPtr pvaOut)
        {
            commandInfo = new CommandInfo
            {CommandGroup = pguidCmdGroup, CommandId = nCmdID, ExecOpt = nCmdexecopt, PvaIn = pvaIn, PvaOut = pvaOut,};
        }

        int PassCommandAlong
        {
            get
            {
                var commandGroup = commandInfo.CommandGroup;
                return NextHandler.Exec(ref commandGroup, commandInfo.CommandId, commandInfo.ExecOpt, commandInfo.PvaIn,
                    commandInfo.PvaOut);
            }
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
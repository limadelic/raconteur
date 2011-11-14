using System;
using System.ComponentModel.Composition;
using System.Runtime.InteropServices;
using System.Windows;
using EnvDTE;
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
using Project = Raconteur.IDE.Project;

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
        readonly IOleCommandTarget NextHandler;
        readonly ITextView TextView;
        readonly CompletionHandlerProvider Provider;
        ICompletionSession Session;
        CommandInfo CommandInfo;

        public CompletionHandler(IVsTextView TextViewAdapter, ITextView TextView, CompletionHandlerProvider Provider)
        {
            this.TextView = TextView;
            this.Provider = Provider;

            //add the command to the command chain
            TextViewAdapter.AddCommandFilter(this, out NextHandler);
        }

        char TypedCharacter
        {
            get
            {
                return CommandInfo.CommandGroup == VSConstants.VSStd2K 
                    && CommandInfo.CommandId == (uint)VSConstants.VSStd2KCmdID.TYPECHAR ? 
                        (char)(ushort)Marshal.GetObjectForNativeVariant(CommandInfo.PvaIn) : 
                        char.MinValue;
            }
        }

        bool IsCommitCharacter
        {
            get
            {
                return CommandInfo.CommandId == (uint) VSConstants.VSStd2KCmdID.RETURN
                    || CommandInfo.CommandId == (uint) VSConstants.VSStd2KCmdID.TAB;
            }
        }

        bool IsDeletionCharacter
        {
            get
            {
                return CommandInfo.CommandId == (uint) VSConstants.VSStd2KCmdID.BACKSPACE
                    || CommandInfo.CommandId == (uint) VSConstants.VSStd2KCmdID.DELETE;
            }
        }

        bool IsAutocompleteCharacter
        {
            get
            {
                return CommandInfo.CommandId == (uint) VSConstants.VSStd2KCmdID.AUTOCOMPLETE
                    || CommandInfo.CommandId == (uint) VSConstants.VSStd2KCmdID.COMPLETEWORD;
            }
        }

        bool IsSelection { get { return Session != null && !Session.IsDismissed; } }
        bool IsFullySelected { get { return Session.SelectedCompletionSet.SelectionStatus.IsSelected; } }
        bool NoActiveSession { get { return Session == null || Session.IsDismissed; } }

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

            var Result = VSConstants.S_OK;

            try
            {
                if (pguidCmdGroup == VSConstants.GUID_VSStandardCommandSet97)
                    switch ((VSConstants.VSStd97CmdID)nCmdID)
                    {
                        case VSConstants.VSStd97CmdID.GotoDefn:
                            GotoDefinition();
                            return Result;
                    }
            
                if (VsShellUtilities.IsInAutomationFunction(Provider.ServiceProvider))
                    return PassCommandAlong;

                if (IsCommitCharacter && IsSelection)
                {
                    if (IsFullySelected)
                    {
                        Session.Commit();
                        return Result;
                    }
                    Session.Dismiss();
                }

                Result = PassCommandAlong;

                return LetterOrDigit 
                    || DeletionCharacter 
                    || AutocompleteCommand ? 
                        VSConstants.S_OK : 
                        Result;
            
            } catch { return Result; }
        }

        void GotoDefinition()
        {
            try
            {
                var Sentence = TextView.Caret.Position.BufferPosition
                    .GetContainingLine().GetText().Trim();

                var ProjectItem = (Marshal.GetActiveObject("VisualStudio.DTE") as DTE).ActiveDocument.ProjectItem;
            
                var FeatureItem = ObjectFactory.FeatureItemFrom(ProjectItem);
            
                var FeatureContent = TextView.TextSnapshot.GetText();

                var project = Project.LoadFrom(FeatureItem);

                var Feature = ObjectFactory.NewFeatureParser.FeatureFrom(FeatureContent, FeatureItem);

                ObjectFactory.NewFeatureCompiler.Compile(Feature, FeatureItem);

                var Step = Feature.Steps.Find(x => x.Location.Content == Sentence);

                if (Step == null) return;

                var CodeFunction = project.CodeFunction(Step);

                if (CodeFunction == null)
                {
                    MessageBox.Show("Step implementation not found ...");
                    return;
                }

                if (!CodeFunction.ProjectItem.IsOpen) CodeFunction.ProjectItem.Open();

                var NavigatePoint = CodeFunction.GetStartPoint(vsCMPart.vsCMPartHeader);
                NavigatePoint.TryToShow();
                NavigatePoint.Parent.Selection.MoveToPoint(NavigatePoint);
            } 
            catch (Exception e)
            {
                MessageBox.Show("Error:" + e);
            }
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

        bool AutocompleteCommand
        {
            get
            {
                if (!IsAutocompleteCharacter) return false;

                if (NoActiveSession) TriggerCompletion();

                Session.Filter();

                return true;
            }
        }

        void SetUpCommandInfo(Guid pguidCmdGroup, uint nCmdID, uint nCmdexecopt, IntPtr pvaIn, IntPtr pvaOut)
        {
            CommandInfo = new CommandInfo
            {
                CommandGroup = pguidCmdGroup, 
                CommandId = nCmdID, 
                ExecOpt = nCmdexecopt, 
                PvaIn = pvaIn, 
                PvaOut = pvaOut,
            };
        }

        int PassCommandAlong
        {
            get
            {
                var commandGroup = CommandInfo.CommandGroup;
                return NextHandler.Exec(ref commandGroup, CommandInfo.CommandId, CommandInfo.ExecOpt, CommandInfo.PvaIn,
                    CommandInfo.PvaOut);
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
// ReSharper restore RedundantDefaultFieldInitializer
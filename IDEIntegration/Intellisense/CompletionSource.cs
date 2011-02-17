using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;

namespace Raconteur.IDEIntegration.Intellisense
{
    internal class CompletionSource : ICompletionSource
    {
        private ICompletionSourceProvider provider;
        private ITextBuffer buffer;
        private List<Completion> completions;
        private bool IsDisposed;

        public CompletionSource(ICompletionSourceProvider provider, ITextBuffer buffer)
        {
            this.provider = provider;
            this.buffer = buffer;
        }

        public void Dispose()
        {
            if (IsDisposed) return;
            
            GC.SuppressFinalize(this);
            IsDisposed = true;
        }

        public void AugmentCompletionSession(ICompletionSession session, IList<CompletionSet> completionSets)
        {
            //TODO
        }
    }
}
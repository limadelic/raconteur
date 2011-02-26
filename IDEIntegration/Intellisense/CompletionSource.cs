using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Formatting;

namespace Raconteur.IDEIntegration.Intellisense
{
    internal class CompletionSource : ICompletionSource
    {
        private bool IsDisposed;
        private readonly ITextBuffer buffer;
        private readonly CompletionSourceProvider provider;

        public CompletionSource(CompletionSourceProvider completionSourceProvider, ITextBuffer textBuffer)
        {
            provider = completionSourceProvider;
            buffer = textBuffer;
        }

        public void Dispose()
        {
            if (IsDisposed) return;
            
            GC.SuppressFinalize(this);
            IsDisposed = true;
        }

        public void AugmentCompletionSession(ICompletionSession session, IList<CompletionSet> completionSets)
        {
            var currentLine = GetCurrentLineFrom(session);
            var feature = session.TextView.TextSnapshot.GetText();
            var completions = new CompletionCalculator { Feature = feature };

            completionSets.Add(new CompletionSet("Steps", "Steps", FindSpanAtCurrentPositionFrom(session),
                completions.For(currentLine.Extent.GetText().Trim()), null));
        }

        private ITextViewLine GetCurrentLineFrom(ICompletionSession session)
        {
            return session.TextView.Caret.ContainingTextViewLine;
        }

        private ITrackingSpan FindSpanAtCurrentPositionFrom(ICompletionSession session)
        {
            var currentPoint = (session.TextView.Caret.Position.BufferPosition) - 1;
            var navigator = provider.NavigatorService.GetTextStructureNavigator(buffer);
            var extent = navigator.GetExtentOfWord(currentPoint);
            return currentPoint.Snapshot.CreateTrackingSpan(extent.Span, SpanTrackingMode.EdgeInclusive);
        }
    }
}
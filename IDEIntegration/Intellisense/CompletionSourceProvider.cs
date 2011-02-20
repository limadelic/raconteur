using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Utilities;

namespace Raconteur.IDEIntegration.Intellisense
{
    [Export(typeof(ICompletionSourceProvider))]
    [ContentType("Raconteur")]
    [Name("Completion")]
    internal class CompletionSourceProvider : ICompletionSourceProvider
    {
        public ICompletionSource TryCreateCompletionSource(ITextBuffer textBuffer)
        {
            return new CompletionSource();
        }
    }
}
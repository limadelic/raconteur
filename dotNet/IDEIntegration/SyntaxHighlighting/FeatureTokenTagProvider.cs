using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using Raconteur.IDEIntegration.SyntaxHighlighting.Token;

namespace Raconteur.IDEIntegration.SyntaxHighlighting
{
    [Export(typeof(ITaggerProvider))]
    [ContentType("Raconteur")]
    [TagType(typeof(FeatureTokenTag))]
    public class FeatureTokenTagProvider : ITaggerProvider
    {
        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {
            return new FeatureTokenTagger() as ITagger<T>;
        }
    }
}
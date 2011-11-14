using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using Raconteur.IDEIntegration.SyntaxHighlighting.Outlining;
using Raconteur.IDEIntegration.SyntaxHighlighting.Token;

namespace Raconteur.IDEIntegration.SyntaxHighlighting
{
// ReSharper disable RedundantDefaultFieldInitializer
    [Export(typeof(ITaggerProvider))]
    [ContentType("Raconteur")]
    [TagType(typeof(OutliningRegionTag))]
    internal sealed class FeatureOutlinerProvider : ITaggerProvider
    {
        [Import] internal IBufferTagAggregatorFactoryService Factory = null;
                
        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {
            var tagAggregator = Factory.CreateTagAggregator<FeatureTokenTag>(buffer);
            return new FeatureOutliner(tagAggregator) as ITagger<T>;
        }
    }
// ReSharper restore RedundantDefaultFieldInitializer
}

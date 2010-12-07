using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using Raconteur.IDEIntegration.SyntaxHighlighting.Token;

namespace Raconteur.IDEIntegration.SyntaxHighlighting.Outlining
{
    internal class FeatureOutliner : ITagger<IOutliningRegionTag>
    {
        private readonly ITagAggregator<FeatureTokenTag> aggregator;

        public FeatureOutliner(ITextBuffer buffer, ITagAggregator<FeatureTokenTag> tagAggregator)
        {
            aggregator = tagAggregator;
        }

        public IEnumerable<ITagSpan<IOutliningRegionTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            return from tag in aggregator.GetTags(spans)
                   let tagSpans = tag.Span.GetSpans(spans[0].Snapshot)
                   where tag.Tag.Type == FeatureTokenTypes.ScenarioBody
                   select new TagSpan<IOutliningRegionTag>(tagSpans[0], 
                       new OutliningRegionTag(false, false, 
                           Settings.Language.Scenario + "...", tagSpans[0].GetText()));
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged
        {
            add {}
            remove {}
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;
using Raconteur.IDEIntegration.SyntaxHighlighting.Token;

namespace Raconteur.IDEIntegration.SyntaxHighlighting.Classification
{
    public class FeatureClassifier : ITagger<ClassificationTag>
    { 
        private readonly ITagAggregator<FeatureTokenTag> aggregator;
        private readonly Dictionary<FeatureTokenTypes, IClassificationType> featureTypes
            = new Dictionary<FeatureTokenTypes, IClassificationType>();

        public static readonly Dictionary<FeatureTokenTypes, string> Styles = new Dictionary<FeatureTokenTypes, string>
        {
            {FeatureTokenTypes.Keyword, "Keyword"},
            {FeatureTokenTypes.Arg, "String"},
            {FeatureTokenTypes.TableValue, "String"},
            {FeatureTokenTypes.Comment, "Comment"},
        };

        public FeatureClassifier(ITextBuffer buffer, ITagAggregator<FeatureTokenTag> tagAggregator, IClassificationTypeRegistryService registry)
        {
            aggregator = tagAggregator;

            foreach (var Type in Styles.Keys)
                featureTypes.Add
                (
                    Type, 
                    registry.GetClassificationType(Styles[Type])
                );
        }

        public IEnumerable<ITagSpan<ClassificationTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            return from tag in aggregator.GetTags(spans) 
                   let tagSpans = tag.Span.GetSpans(spans[0].Snapshot)
                   where featureTypes.ContainsKey(tag.Tag.Type)
                   select new TagSpan<ClassificationTag>(tagSpans[0], 
                        new ClassificationTag(featureTypes[tag.Tag.Type]));
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged
        {
            add { }
            remove { }
        }
    }
}
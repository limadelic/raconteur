using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using Raconteur.IDEIntegration.SyntaxHighlighting.Token;

namespace Raconteur.IDEIntegration.SyntaxHighlighting
{
    internal sealed class FeatureTokenTagger : ITagger<FeatureTokenTag>
    {
        private readonly IDictionary<string, FeatureTokenTypes> tokenTypes;
        private readonly ITextBuffer buffer;

        IEnumerable<ITagSpan<FeatureTokenTag>> AllTags
        {
            get { return CreateArgSpans().Union(CreateKeywordSpans()); }
        }

        public FeatureTokenTagger(ITextBuffer buffer)
        {
            this.buffer = buffer;

            tokenTypes = new Dictionary<string, FeatureTokenTypes>
            {
                {Settings.Language.Feature + ":", FeatureTokenTypes.FeatureDefinition},
                {Settings.Language.Scenario + ":", FeatureTokenTypes.ScenarioDefinition},
            };
        }

        private IEnumerable<TagSpan<FeatureTokenTag>> CreateArgSpans()
        {
            var boundaries = buffer.CurrentSnapshot.GetText().ArgBoundaries();

            foreach (var boundary in boundaries)
            {
                if (boundary.IsOpen)
                    yield return CreateTag(boundary.Start, 
                        buffer.CurrentSnapshot.Length - boundary.Start + 1, 
                        FeatureTokenTypes.Arg);
                else
                    yield return CreateTag(boundary.Start,
                        boundary.End - boundary.Start + 1,
                        FeatureTokenTypes.Arg);
            }
        }

        private IEnumerable<TagSpan<FeatureTokenTag>> CreateKeywordSpans()
        {
            foreach (var line in buffer.CurrentSnapshot.Lines)
            {
                var location = line.Start.Position;
                var tokens = line.GetText().Split(' ');

                foreach (var token in tokens)
                {
                    if (tokenTypes.ContainsKey(token.Trim()))
                        yield return CreateTag(location, token.Length, tokenTypes[token.Trim()]); 

                    location += token.Length + 1;
                }
            }
        }

        TagSpan<FeatureTokenTag> CreateTag(int startLocation, int length, FeatureTokenTypes type)
        {
            var tokenSpan = new SnapshotSpan(buffer.CurrentSnapshot, new Span(startLocation, length));
            return new TagSpan<FeatureTokenTag>(tokenSpan, new FeatureTokenTag(type));
        }

        public IEnumerable<ITagSpan<FeatureTokenTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            return spans.SelectMany(span => AllTags
                .Where(tagSpan => tagSpan.Span.IntersectsWith(span)));
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged
        {
            add { }
            remove { }
        }
    }
}
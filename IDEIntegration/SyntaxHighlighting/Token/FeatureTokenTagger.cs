using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using Raconteur.IDEIntegration.SyntaxHighlighting.Token;

namespace Raconteur.IDEIntegration.SyntaxHighlighting
{
    internal sealed class FeatureTokenTagger : ITagger<FeatureTokenTag>
    {
        private readonly IDictionary<string, FeatureTokenTypes> tokenTypes;

        public FeatureTokenTagger(ITextBuffer buffer)
        {
            tokenTypes = new Dictionary<string, FeatureTokenTypes>
            {
                {"Feature:", FeatureTokenTypes.FeatureDefinition},
                {"Scenario:", FeatureTokenTypes.ScenarioDefinition},
            };
        }

        public IEnumerable<ITagSpan<FeatureTokenTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            foreach (var span in spans)
            {
                var line = span.Start.GetContainingLine();
                var location = line.Start.Position;
                var tokens = line.GetText().Split(' ');

                foreach (var token in tokens)
                {
                    if (tokenTypes.ContainsKey(token))
                    {
                        var tokenSpan = new SnapshotSpan(span.Snapshot, new Span(location, token.Length));

                        if (tokenSpan.IntersectsWith(span))
                            yield return new TagSpan<FeatureTokenTag>(tokenSpan, new FeatureTokenTag(tokenTypes[token]));
                    }

                    if (token.StartsWith("\"") && token.EndsWith("\""))
                    {
                        var tokenSpan = new SnapshotSpan(span.Snapshot, new Span(location, token.Length));

                        if (tokenSpan.IntersectsWith(span))
                            yield return new TagSpan<FeatureTokenTag>(tokenSpan, new FeatureTokenTag(FeatureTokenTypes.Arg));
                    }

                    location += token.Length + 1;
                }
            }
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged
        {
            add { }
            remove { }
        }
    }
}
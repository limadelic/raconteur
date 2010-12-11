using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;

namespace Raconteur.IDEIntegration.SyntaxHighlighting.Token
{
    internal sealed class FeatureTokenTagger : ITagger<FeatureTokenTag>
    {
        readonly IDictionary<string, FeatureTokenTypes> tokenTypes;
        readonly ITextBuffer buffer;

        ITextSnapshot Snapshot { get { return buffer.CurrentSnapshot;  } }

        IEnumerable<ITagSpan<FeatureTokenTag>> AllTags
        {
            get
            {
                return ArgSpans
                    .Union(KeywordSpans)
                    .Union(TableSpans)
                    .Union(CommentSpans)
                    .Union(ScenarioSpans); 
            }
        }

        public FeatureTokenTagger(ITextBuffer buffer)
        {
            this.buffer = buffer;

            tokenTypes = new Dictionary<string, FeatureTokenTypes>
            {
                {Settings.Language.Feature + ":", FeatureTokenTypes.FeatureDefinition},
                {Settings.Language.Scenario + ":", FeatureTokenTypes.ScenarioDefinition},
                {Settings.Language.Examples + ":", FeatureTokenTypes.ExampleDefinition},
            };
        }

        IEnumerable<TagSpan<FeatureTokenTag>> ArgSpans
        {
            get
            {
                return Snapshot.GetText().ArgBoundaries().Select(
                        boundary => CreateTag(boundary.Start, boundary.Length, FeatureTokenTypes.Arg));
            }
        }

        IEnumerable<TagSpan<FeatureTokenTag>> KeywordSpans
        {
            get
            {
                foreach (var line in Snapshot.Lines)
                {
                    var location = line.Start.Position;
                    var tokens = line.GetText().Split(' ');

                    foreach (var token in tokens)
                    {
                        if (tokenTypes.ContainsKey(token.Trim())) yield return CreateTag(location, token.Length, tokenTypes[token.Trim()]);

                        location += token.Length + 1;
                    }
                }
            }
        }

        IEnumerable<TagSpan<FeatureTokenTag>> TableSpans
        {
            get
            {
                foreach (var line in Snapshot.Lines.Where(line => line.IsTableRow()))
                {
                    var location = line.Start.Position;
                    var tokens = line.GetText().Split('|');

                    foreach (var token in tokens)
                    {
                        if (!string.IsNullOrWhiteSpace(token) && !line.IsTableHeader()) yield return CreateTag(location, token.Length, FeatureTokenTypes.TableValue);

                        location += token.Length + 1;
                    }
                }
            }
        }

        IEnumerable<TagSpan<FeatureTokenTag>> ScenarioSpans
        {
            get
            {
                ITextSnapshotLine lastDeclaration = null;

                foreach (
                    var line in Snapshot.Lines.Where(line => line.GetText().Trim().StartsWith(Settings.Language.Scenario + ":"))
                    )
                {
                    if (lastDeclaration != null) yield return CreateTag(lastDeclaration.Start, line.PreviousLine().End, FeatureTokenTypes.ScenarioBody);

                    lastDeclaration = line;
                }

                yield return
                    CreateTag(lastDeclaration.Start, Snapshot.GetLineFromLineNumber(Snapshot.LineCount - 1).End,
                        FeatureTokenTypes.ScenarioBody);
            }
        }

        IEnumerable<TagSpan<FeatureTokenTag>> CommentSpans
        {
            get
            {
                return from line in Snapshot.Lines
                       let startOfComment = line.Start.Position + line.GetText().IndexOf("//")
                       let commentLength = line.End.Position - startOfComment
                       where line.GetText().Contains("//") && !InArgOrTable(startOfComment)
                       select CreateTag(startOfComment, commentLength, FeatureTokenTypes.Comment);
            }
        }

        private bool InArgOrTable(int startOfComment)
        {
            return ArgSpans.Any(arg =>
                arg.Span.Start.Position <= startOfComment &&
                arg.Span.End.Position >= startOfComment)
                || TableSpans.Any(tableValue =>
                    tableValue.Span.Start.Position <= startOfComment &&
                    tableValue.Span.End.Position >= startOfComment);

        }

        TagSpan<FeatureTokenTag> CreateTag(int startLocation, int length, FeatureTokenTypes type)
        {
            var tokenSpan = new SnapshotSpan(Snapshot, new Span(startLocation, length));
            return new TagSpan<FeatureTokenTag>(tokenSpan, new FeatureTokenTag(type));
        }

        TagSpan<FeatureTokenTag> CreateTag(SnapshotPoint startPoint, SnapshotPoint endPoint, FeatureTokenTypes type)
        {
            var tokenSpan = new SnapshotSpan(Snapshot, new Span(startPoint.Position, startPoint.Difference(endPoint)));
            return new TagSpan<FeatureTokenTag>(tokenSpan, new FeatureTokenTag(type));
        }

        public IEnumerable<ITagSpan<FeatureTokenTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            return spans.SelectMany(span => AllTags
                .Where(tagSpan => tagSpan.Span.IntersectsWith(span)));
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged { add {} remove {} }
    }
}
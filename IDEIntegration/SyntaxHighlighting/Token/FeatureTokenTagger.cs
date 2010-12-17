using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using ITags=System.Collections.Generic.IEnumerable<Microsoft.VisualStudio.Text.Tagging.ITagSpan<Raconteur.IDEIntegration.SyntaxHighlighting.Token.FeatureTokenTag>>;
using ITagsWrap=System.Collections.Generic.IEnumerable<Raconteur.IDEIntegration.SyntaxHighlighting.Token.ITagSpanWrap<Raconteur.IDEIntegration.SyntaxHighlighting.Token.FeatureTokenTag>>;
using Tags=System.Collections.Generic.List<Microsoft.VisualStudio.Text.Tagging.ITagSpan<Raconteur.IDEIntegration.SyntaxHighlighting.Token.FeatureTokenTag>>;
using TagsWrap=System.Collections.Generic.List<Raconteur.IDEIntegration.SyntaxHighlighting.Token.ITagSpanWrap<Raconteur.IDEIntegration.SyntaxHighlighting.Token.FeatureTokenTag>>;

namespace Raconteur.IDEIntegration.SyntaxHighlighting.Token
{
    public interface ITagSpanWrap<T> where T : ITag
    {
        TagSpan<T> Core { get; }

        string Text { get; }

        FeatureTokenTypes Type { get; }
    }

    public class TagSpanWrap<T> : ITagSpanWrap<T> where T : ITag
    {
        public TagSpan<T> Core { get; set; }

        public string Text { get { return Core.Span.GetText(); } }

        public FeatureTokenTypes Type { get { return (Core.Tag as FeatureTokenTag).Type; } }
    }

    public class FeatureTokenTagger : ITagger<FeatureTokenTag>
    {
        readonly ITextBuffer buffer;
        protected readonly string Feature;

        ITextSnapshot Snapshot { get { return buffer.CurrentSnapshot;  } }

        public FeatureTokenTagger(ITextBuffer buffer)
        {
            this.buffer = buffer;
            Feature = Snapshot.GetText();
        }

        public FeatureTokenTagger(string Feature)
        {
            this.Feature = Feature;
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged { add {} remove {} }

        public ITags GetTags(NormalizedSnapshotSpanCollection spans)
        {
            return spans.SelectMany(span => AllTags
                .Where(tagSpan => tagSpan.Span.IntersectsWith(span)));
        }


        public ITagsWrap Tags
        {
            get
            {
                var Position = 0;
                Action<int> NextPosition = LineLength => Position += LineLength + 2;

                return Feature
                    .Lines()
                    .ApplyLengthTo(NextPosition)
                    .SelectMany(Line => (TagsIn(Line, Position)));
            }
        }

        ITagsWrap TagsIn(string Line, int Position)
        {
            var Tags = new TagsWrap();

            var OriginalLine = Line;
            Line = Line.Trim();

            if (Line.StartsWith("//"))
            {
                Tags.Add(CreateTagWrap
                (
                    Position + OriginalLine.IndexOf("//"),
                    Line.Length, 
                    FeatureTokenTypes.Comment
                ));

                return Tags;
            }

            var FirstWord = Line.FirstWord();

            if (Keywords.Contains(FirstWord)) 
                Tags.Add(CreateTagWrap
                (
                    Position + OriginalLine.IndexOf(FirstWord),
                    FirstWord.Length, 
                    FeatureTokenTypes.Keyword
                ));
            
            return Tags;
        }


        ITags AllTags
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

        ITags ArgSpans
        {
            get
            {
                return Feature.ArgBoundaries().Select(
                    boundary => CreateTag(boundary.Start, boundary.Length, FeatureTokenTypes.Arg));
            }
        }

        readonly List<string> Keywords = new List<string>
        {
            Settings.Language.Feature + ":",
            Settings.Language.Scenario + ":",
            Settings.Language.Examples + ":",
        };


        ITags KeywordSpans
        {
            get
            {
                var Position = 0;
                Action<int> NextPosition = LineLength => Position += LineLength + 2;

                return 
                    from Line in Feature.Lines().ApplyLengthTo(NextPosition) 
                    let FirstWord = Line.FirstWord() 
                    where Keywords.Contains(FirstWord) 
                    select CreateTag
                    (
                        Position + Line.IndexOf(FirstWord),
                        FirstWord.Length, 
                        FeatureTokenTypes.Keyword
                    );
            }
        }

        ITags TableSpans
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

        ITags ScenarioSpans
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

        ITags CommentSpans
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

        protected virtual ITagSpanWrap<FeatureTokenTag> CreateTagWrap(int startLocation, int length, FeatureTokenTypes type)
        {
            var tokenSpan = new SnapshotSpan(Snapshot, new Span(startLocation, length));

            return new TagSpanWrap<FeatureTokenTag>
            {
                Core = new TagSpan<FeatureTokenTag>(tokenSpan, new FeatureTokenTag(type))
            };
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
    }
}
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

                return 
                    Feature
                    .Lines()
                    .ApplyLengthTo(NextPosition)
                    .SelectMany(Line => (TagsIn(Line, Position)));
            }
        }

        int MultilineTagStart = -1;
        string MultilineSymbol;
        
        bool IsInsideMultilineTag { get { return MultilineTagStart >= 0; } }

        string FullLine;
        string Line;
        int Position;

        ITagsWrap TagsIn(string Line, int Position)
        {
            FullLine = Line;
            this.Line = Line.Trim();
            this.Position = Position;

            return 
                MultiLineTags ?? 
                CommentTags ?? 
                KeywordTags ??
                TableTags ??
                ArgsTags ??
                new TagsWrap();
        }

        ITagsWrap TableTags
        {
            get
            {
                if (!Line.StartsWith("|")) return null;
                
                var Index = FullLine.IndexOf('|') + 1;
                var StartPoint = Position;

                return 
                    from Arg in Line.Split('|').Chop(1)
                    select CreateTagWrap
                    (
                        (StartPoint += Arg.Length) - Arg.Length + Index++, 
                        Arg.Length, 
                        FeatureTokenTypes.Arg
                    );
            }
        }

        ITagsWrap ArgsTags
        {
            get
            {
                if (!Line.Contains('"')) return null;

                return 
                    from Arg in Line.Split('"').Odds().Distinct()
                    from Index in FullLine.IndexesOf(Arg)
                    select CreateTagWrap
                    (
                        Position + Index - 1, 
                        Arg.Length + 2, 
                        FeatureTokenTypes.Arg
                    );
            }
        }

        TagsWrap KeywordTags
        {
            get
            {
                var FirstWord = Line.FirstWord();

                return !Keywords.Contains(FirstWord) ? null : new TagsWrap
                {
                    CreateTagWrap
                    (
                        Position + FullLine.IndexOf(FirstWord), 
                        FirstWord.Length, 
                        FeatureTokenTypes.Keyword
                    )
                };
            }
        }

        TagsWrap CommentTags
        {
            get
            {
                return !Line.StartsWith("//") ? null : new TagsWrap
                {
                    CreateTagWrap
                    (
                        Position + FullLine.IndexOf("//"), 
                        Line.Length, 
                        FeatureTokenTypes.Comment
                    )
                };
            }
        }

        TagsWrap MultiLineTags
        {
            get
            {
                return 
                    CloseMultilineTag ??
                    InsideMultilineTag ??
                    StartMultilineTag;
            }
        }

        bool IsMultilineTagStart
        {
            get
            {
                return 
                    !IsInsideMultilineTag && 
                    (Line == "/*" || Line == "\"");
            } 
        }

        TagsWrap StartMultilineTag
        {
            get
            {
                if (!IsMultilineTagStart) return null;

                MultilineTagStart = Position + FullLine.IndexOf(Line);
                MultilineSymbol = Line;

                return new TagsWrap();
            }
        }

        TagsWrap InsideMultilineTag
        {
            get { return IsInsideMultilineTag ? new TagsWrap() : null; } 
        }

        bool IsClosingMultilineTag
        {
            get
            {
                return 
                    IsInsideMultilineTag && 
                    (IsClosingMultilineComment || IsClosingMultilineArg);
            }
        }

        bool IsInsideMultilineComment { get { return MultilineSymbol == "/*"; } }

        bool IsInsideMultilineArg { get { return MultilineSymbol == "\""; } }

        bool IsClosingMultilineComment
        {
            get { return IsInsideMultilineComment && Line == "*/"; }
        }

        bool IsClosingMultilineArg
        {
            get { return IsInsideMultilineArg && Line == "\""; }
        }

        TagsWrap CloseMultilineTag
        {
            get
            {
                if (!IsClosingMultilineTag) return null;

                var Tags = new TagsWrap
                {
                    CreateTagWrap
                    (
                        MultilineTagStart, 
                        Position + FullLine.Length - MultilineTagStart, 
                        Line == "*/" ? 
                            FeatureTokenTypes.Comment : 
                            FeatureTokenTypes.Arg
                    )
                };

                MultilineTagStart = -1;

                return Tags;
            }
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
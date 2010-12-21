using System;
using System.Collections.Generic;
using System.Linq;
using ITagsWrap=System.Collections.Generic.IEnumerable<Raconteur.IDEIntegration.SyntaxHighlighting.Token.ITagSpanWrap<Raconteur.IDEIntegration.SyntaxHighlighting.Token.FeatureTokenTag>>;
using TagsWrap=System.Collections.Generic.List<Raconteur.IDEIntegration.SyntaxHighlighting.Token.ITagSpanWrap<Raconteur.IDEIntegration.SyntaxHighlighting.Token.FeatureTokenTag>>;

namespace Raconteur.IDEIntegration.SyntaxHighlighting.Token
{
    public class ParsingState
    {
        public TagFactory TagFactory;
        public string Feature;

        public int Position;
        public string FullLine;
        public string Line;
    }

    interface TagsParser { ITagsWrap Tags { get; } }

    public class TagsParserBase
    {
        protected ParsingState ParsingState;

        protected TagFactory TagFactory
        {
            get { return ParsingState.TagFactory;  }
            set { ParsingState.TagFactory = value; }
        }
        protected string Feature
        {
            get { return ParsingState.Feature;  }
            set { ParsingState.Feature = value; }
        }
        protected int Position
        {
            get { return ParsingState.Position;  }
            set { ParsingState.Position = value; }
        }
        protected string FullLine
        {
            get { return ParsingState.FullLine;  }
            set { ParsingState.FullLine = value; }
        }
        protected string Line
        {
            get { return ParsingState.Line;  }
            set { ParsingState.Line = value; }
        }

        protected ITagSpanWrap<FeatureTokenTag> CreateTag(int StartLocation, int Length, FeatureTokenTypes Type)
        {
            return TagFactory.CreateTag(StartLocation, Length, Type);
        }
    }

    public class KeywordParser : TagsParserBase, TagsParser
    {
        public KeywordParser(ParsingState ParsingState)
        {
            this.ParsingState = ParsingState;
        }

        readonly List<string> Keywords = new List<string>
        {
            Settings.Language.Feature,
            Settings.Language.Scenario,
            Settings.Language.ScenarioOutline,
            Settings.Language.Examples,
        };

        public ITagsWrap Tags
        {
            get
            {
                var Keyword = Line.Split(':')[0];

                return !Keywords.Contains(Keyword) ? null : new TagsWrap
                {
                    CreateTag
                    (
                        Position + FullLine.IndexOf(Keyword), 
                        Keyword.Length + 1, 
                        FeatureTokenTypes.Keyword
                    )
                };
            }
        }
    }

    public class FeatureTagParser : TagsParserBase
    {
        readonly TagsParser Keywords;

        public FeatureTagParser(TagFactory TagFactory, string Feature)
        {
            ParsingState = new ParsingState();

            this.TagFactory = TagFactory;
            this.Feature = Feature;

            Keywords = new KeywordParser(ParsingState);
        }

        public ITagsWrap Tags
        {
            get
            {
                Position = 0;
                Action<int> NextPosition = LineLength => Position += LineLength + 2;

                return 
                    Feature
                    .Lines()
                    .ApplyLengthTo(NextPosition)
                    .SelectMany(TagsFromLine);
            }
        }

        ITagsWrap TagsFromLine(string Line)
        {
            FullLine = Line;
            this.Line = Line.Trim();

            return 
                MultiLineTags ?? 
                CommentTags ??
                (Keywords.Tags ??
                 TableTags ??
                 ArgsTags ??
                 new TagsWrap())
                 .Union(ScenarioTag);
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
                    select CreateTag
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
                    select CreateTag
                    (
                        Position + Index - 1, 
                        Arg.Length + 2, 
                        FeatureTokenTypes.Arg
                    );
            }
        }

        int ScenarioStart;

        bool IsLastLine
        {
            get
            {
                return Position + FullLine.Length == Feature.Length;
            }
        }

        bool IsEndOfScenario
        {
            get
            {
                if (IsLastLine) return true;

                if (!Line.StartsWith(Settings.Language.Scenario)) return false;

                if (ScenarioStart > 0) return true;
                
                ScenarioStart = Position;

                return false;
            }
        }

        TagsWrap ScenarioTag
        {
            get
            {
                var Tags = new TagsWrap();

                if (IsEndOfScenario)
                {
                    Tags.Add(CreateTag
                    (
                        ScenarioStart,
                        Position - ScenarioStart + (IsLastLine ? FullLine.Length : -2),
                        FeatureTokenTypes.ScenarioBody
                    ));

                    ScenarioStart = IsLastLine ? 0 : Position;
                }

                return Tags;
            }
        }

        TagsWrap CommentTags
        {
            get
            {
                return !Line.StartsWith("//") ? null : new TagsWrap
                {
                    CreateTag
                    (
                        Position + FullLine.IndexOf("//"), 
                        Line.Length, 
                        FeatureTokenTypes.Comment
                    )
                };
            }
        }

        int MultilineTagStart = -1;
        string MultilineSymbol;
        bool IsInsideMultilineTag { get { return MultilineTagStart >= 0; } }

        ITagsWrap MultiLineTags
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
                    (
                        IsLastLine ||
                        IsClosingMultilineComment || 
                        IsClosingMultilineArg
                    );
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
                    CreateTag
                    (
                        MultilineTagStart, 
                        Position + FullLine.Length - MultilineTagStart, 
                        MultilineSymbol == "/*" ? 
                            FeatureTokenTypes.Comment : 
                            FeatureTokenTypes.Arg
                    )
                };

                MultilineTagStart = -1;

                return Tags;
            }
        }
    }
}
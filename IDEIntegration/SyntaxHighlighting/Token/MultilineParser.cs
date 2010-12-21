using System.Collections.Generic;

namespace Raconteur.IDEIntegration.SyntaxHighlighting.Token
{
    public class MultilineParser : TagsParserBase 
    {
        public MultilineParser(ParsingState ParsingState) : base(ParsingState) {}

        int MultilineTagStart = -1;
        string MultilineSymbol;
        bool IsInsideMultilineTag { get { return MultilineTagStart >= 0; } }

        public override IEnumerable<ITagSpanWrap<FeatureTokenTag>> Tags
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

        List<ITagSpanWrap<FeatureTokenTag>> StartMultilineTag
        {
            get
            {
                if (!IsMultilineTagStart) return null;

                MultilineTagStart = Position + FullLine.IndexOf(Line);
                MultilineSymbol = Line;

                return new List<ITagSpanWrap<FeatureTokenTag>>();
            }
        }

        List<ITagSpanWrap<FeatureTokenTag>> InsideMultilineTag
        {
            get { return IsInsideMultilineTag ? new List<ITagSpanWrap<FeatureTokenTag>>() : null; } 
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

        List<ITagSpanWrap<FeatureTokenTag>> CloseMultilineTag
        {
            get
            {
                if (!IsClosingMultilineTag) return null;

                var Tags = new List<ITagSpanWrap<FeatureTokenTag>> 
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
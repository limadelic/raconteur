using System.Collections.Generic;

namespace Raconteur.IDEIntegration.SyntaxHighlighting.Token
{
    public class CommentsParser : TagsParserBase
    {
        public CommentsParser(ParsingState ParsingState) : base(ParsingState) {}

        public override IEnumerable<ITagSpanWrap<FeatureTokenTag>> Tags
        {
            get
            {
                return !Line.StartsWith("//") ? null : new List<ITagSpanWrap<FeatureTokenTag>> 
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
    }
}
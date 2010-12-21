using System.Collections.Generic;
using Raconteur.IDEIntegration.SyntaxHighlighting.Token;

namespace Raconteur.IDEIntegration.SyntaxHighlighting.Parsing
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
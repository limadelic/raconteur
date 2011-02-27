using System.Collections.Generic;
using Raconteur.IDEIntegration.SyntaxHighlighting.Token;

namespace Raconteur.IDEIntegration.SyntaxHighlighting.Parsing
{
    public class UsingParser : TagsParserBase
    {
        public UsingParser(ParsingState ParsingState) : base(ParsingState) {}

        public override IEnumerable<ITagSpanWrap<FeatureTokenTag>> Tags
        {
            get
            {
                return !Line.StartsWith(Settings.Language.Using) ? null : new List<ITagSpanWrap<FeatureTokenTag>> 
                {
                    CreateTag
                    (
                        Position + FullLine.IndexOf(Settings.Language.Using), 
                        Line.Length, 
                        FeatureTokenTypes.Comment
                    )
                };
            }
        }
    }
}
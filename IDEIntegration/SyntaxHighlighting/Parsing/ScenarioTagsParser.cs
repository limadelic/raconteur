using System.Collections.Generic;
using System.Linq;
using Raconteur.IDEIntegration.SyntaxHighlighting.Token;

namespace Raconteur.IDEIntegration.SyntaxHighlighting.Parsing
{
    public class ScenarioTagsParser : TagsParserBase
    {
        public ScenarioTagsParser(ParsingState ParsingState) : base(ParsingState) {}

        public override IEnumerable<ITagSpanWrap<FeatureTokenTag>> Tags
        {
            get
            {
                if (!Line.StartsWith("@")) return null;

                return 
                    from Index in FullLine.IndexesOf("@")
                    select CreateTag
                    (
                        Position + Index,
                        FullLine.Length,
                        FeatureTokenTypes.Tag
                    );
            }
        }
    }
}
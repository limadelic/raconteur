using System.Collections.Generic;
using System.Linq;

namespace Raconteur.IDEIntegration.SyntaxHighlighting.Token
{
    public class ArgsParser : TagsParserBase
    {
        public ArgsParser(ParsingState ParsingState) : base(ParsingState) {}

        public override IEnumerable<ITagSpanWrap<FeatureTokenTag>> Tags
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
    }
}
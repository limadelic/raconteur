using System.Collections.Generic;
using System.Linq;
using Raconteur.IDEIntegration.SyntaxHighlighting.Token;

namespace Raconteur.IDEIntegration.SyntaxHighlighting.Parsing
{
    public class TableParser : TagsParserBase 
    {
        public TableParser(ParsingState ParsingState) : base(ParsingState) {}

        public override IEnumerable<ITagSpanWrap<FeatureTokenTag>> Tags
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
    }
}
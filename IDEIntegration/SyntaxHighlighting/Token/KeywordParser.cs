using System.Collections.Generic;
using ITagsWrap=System.Collections.Generic.IEnumerable<Raconteur.IDEIntegration.SyntaxHighlighting.Token.ITagSpanWrap<Raconteur.IDEIntegration.SyntaxHighlighting.Token.FeatureTokenTag>>;

namespace Raconteur.IDEIntegration.SyntaxHighlighting.Token
{
    public class KeywordParser : TagsParserBase
    {
        public KeywordParser(ParsingState ParsingState) : base(ParsingState) {}

        readonly List<string> Keywords = new List<string>
        {
            Settings.Language.Feature,
            Settings.Language.Scenario,
            Settings.Language.ScenarioOutline,
            Settings.Language.Examples,
        };

        public override ITagsWrap Tags
        {
            get
            {
                var Keyword = Line.Split(':')[0];

                return !Keywords.Contains(Keyword) ? null : new List<ITagSpanWrap<FeatureTokenTag>> 
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
}
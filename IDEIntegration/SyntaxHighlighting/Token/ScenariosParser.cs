using System.Collections.Generic;

namespace Raconteur.IDEIntegration.SyntaxHighlighting.Token
{
    public class ScenariosParser : TagsParserBase 
    {
        public ScenariosParser(ParsingState ParsingState) : base(ParsingState) {}
        
        int ScenarioStart;

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

        public override IEnumerable<ITagSpanWrap<FeatureTokenTag>> Tags
        {
            get
            {
                var Results = new List<ITagSpanWrap<FeatureTokenTag>>();

                if (IsEndOfScenario)
                {
                    Results.Add(CreateTag
                    (
                        ScenarioStart,
                        Position - ScenarioStart + (IsLastLine ? FullLine.Length : -2),
                        FeatureTokenTypes.ScenarioBody
                    ));

                    ScenarioStart = IsLastLine ? 0 : Position;
                }

                return Results;
            }
        }
    }
}
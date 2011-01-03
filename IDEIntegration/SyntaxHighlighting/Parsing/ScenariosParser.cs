using System.Collections.Generic;
using Raconteur.IDEIntegration.SyntaxHighlighting.Token;

namespace Raconteur.IDEIntegration.SyntaxHighlighting.Parsing
{
    public class ScenariosParser : TagsParserBase 
    {
        public ScenariosParser(ParsingState ParsingState) : base(ParsingState) {}
        
        int ScenarioStart;

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

                    ScenarioStart = 0;
                }

                if (IsStartOfScenario) ScenarioStart = Position;

                return Results;
            }
        }

        bool IsFirstScenarioTag { get { return Line.StartsWith("@"); } }

        bool IsStartOfScenario { get { return Line.StartsWith(Settings.Language.Scenario); } }

        bool IsEndOfScenario
        {
            get
            {
                if (ScenarioStart == 0) return IsLastLine && IsStartOfScenario;

                return IsLastLine 
                    || IsStartOfScenario 
                    || IsFirstScenarioTag;
            }
        }
    }
}
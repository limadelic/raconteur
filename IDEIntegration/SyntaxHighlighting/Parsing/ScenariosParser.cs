using System.Collections.Generic;
using Raconteur.IDEIntegration.SyntaxHighlighting.Token;

namespace Raconteur.IDEIntegration.SyntaxHighlighting.Parsing
{
    public class ScenariosParser : TagsParserBase 
    {
        public ScenariosParser(ParsingState ParsingState) : base(ParsingState) {}
        
        public override IEnumerable<ITagSpanWrap<FeatureTokenTag>> Tags
        {
            get
            {
                if (IsEndOfScenario)
                {
                    yield return NewCollapsibleScenario;
                    ScenarioStart = -1;
                }
                
                if (IsStartOfScenario)
                {
                    ScenarioStart = Position;
                    if (IsLastLine) yield return NewCollapsibleScenario;
                }

                yield break;
            }
        }

        int ScenarioStart = -1;

        int ScenarioLength 
        {
            get
            {
                var Length = Position - ScenarioStart;

                return Length == 0 ? FullLine.Length : Length - 2;
            } 
        }

        ITagSpanWrap<FeatureTokenTag> NewCollapsibleScenario
        { 
            get
            {
                return CreateTag(ScenarioStart, ScenarioLength, FeatureTokenTypes.ScenarioBody);
            }
        }

        bool ThereIsNoScenario { get { return ScenarioStart == -1; } }
        
        bool IsFirstScenarioTag { get { return Line.StartsWith("@"); } }

        bool IsStartOfScenario { get { return Line.StartsWith(Settings.Language.Scenario); } }

        bool IsEndOfScenario
        {
            get
            {
                if (ThereIsNoScenario) return false;

                return IsLastLine 
                    || IsStartOfScenario 
                    || IsFirstScenarioTag;
            }
        }
    }
}
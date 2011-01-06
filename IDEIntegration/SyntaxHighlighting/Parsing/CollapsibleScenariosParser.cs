using System.Collections.Generic;
using Raconteur.IDEIntegration.SyntaxHighlighting.Token;

namespace Raconteur.IDEIntegration.SyntaxHighlighting.Parsing
{
    public class CollapsibleScenariosParser : TagsParserBase 
    {
        public CollapsibleScenariosParser(ParsingState ParsingState) : base(ParsingState) {}
        
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

                if (!IsLastLine) return Length - 2;

                if (Length > 0) return Length + (IsStartOfScenario ? -2 : FullLine.Length);
                 
                return FullLine.Length;
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
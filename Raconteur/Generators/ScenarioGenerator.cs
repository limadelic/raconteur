using System.Linq;

namespace Raconteur.Generators
{
    public class ScenarioGenerator : CodeGenerator
    {
        private const string ScenarioDeclaration = 
@"        
        [TestMethod]
        public void {0}()
        {{ {1}
        }}
";

        readonly Scenario Scenario;

        public ScenarioGenerator(Scenario Scenario) 
        {
            this.Scenario = Scenario;
        }

        public string Code
        {
            get
            {
                return Scenario.IsOutline ?
                    OutlineScenarioCode :
                    ScenarioCode;
            }
        }

        string ScenarioCode
        {
            get
            {
                var StepCode = Scenario.Steps.Aggregate(string.Empty, 
                    (Steps, Step) => Steps + CodeFor(Step));

                return string.Format(ScenarioDeclaration, Scenario.Name, StepCode);
            }
        }

        string CodeFor(Step Step) { return new StepGenerator(Step).Code; }

        string OutlineScenarioCode
        {
            get
            {
                var Outline = ScenarioCode;
                var Result = string.Empty;

                for (var Row = 0; Row < Scenario.Examples.Count; Row++)
                {
                    var CurrentCode = ReplaceNameIn(Outline, Scenario.Name, Row + 1);

                    for (var Col = 0; Col < Scenario.Examples.Header.Count; Col++) 
                        CurrentCode = ReplaceExampleIn(CurrentCode, Row, Col);

                    Result += CurrentCode;
                }

                return Result;
            }
        }

        string ReplaceExampleIn(string CurrentScenarioCode, int Row, int Col)
        {
            return CurrentScenarioCode.Replace
            (
                Scenario.Examples.Header[Col].Quote(), 
                ArgFormatter.ValueOf(Scenario.Examples[Row, Col])
            );
        }

        string ReplaceNameIn(string Outline, string Name, int Index)
        {
            return Outline.Replace(Name+"()", Name+Index+"()");
        }
    }
}
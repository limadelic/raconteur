using System;
using System.Linq;

namespace Raconteur.Generators
{
    public class ScenarioGenerator : CodeGenerator
    {
        private const string ScenarioDeclaration = 
@"        
        {0}{1}{2}
        public void {3}()
        {{ {4}
        }}
";

        private const string TagDeclaration = 
@"        
        {0}";

        readonly Scenario Scenario;
        readonly Type StepLibrary;

        public ScenarioGenerator(Scenario Scenario, Type StepLibrary = null)
        {
            this.Scenario = Scenario;
            this.StepLibrary = StepLibrary;
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

                return string.Format
                (
                    ScenarioDeclaration, 
                    Settings.XUnit.MethodAttr,
                    Tags, 
                    Ignore,
                    Scenario.Name, 
                    StepCode
                );
            }
        }

        string Tags
        {
            get
            {
                return Scenario.Tags.Where(IsNotIgnored).Aggregate(string.Empty, (Tags, Tag) =>
                    Tags + string.Format(TagDeclaration, 
                        string.Format(Settings.XUnit.Category, Tag)));
            }
        }

        string Ignore
        {
            get
            {
                var IgnoreTag = Scenario.Tags.FirstOrDefault(IsIgnored);

                if (IgnoreTag == null) return string.Empty;

                var Reason = IgnoreTag.Remove(0, 6);

                return string.Format
                (
                    TagDeclaration, 
                    Reason.IsEmpty() ? 
                        Settings.XUnit.Ignore :
                        string.Format(Settings.XUnit.IgnoreWithReason, Reason.Trim())
                );
            }
        }

        bool IsIgnored(string Tag) { return Tag.ToLower().StartsWith("ignore"); }
        
        bool IsNotIgnored(string Tag) { return !IsIgnored(Tag); }

        string CodeFor(Step Step) { return new StepGenerator(Step, StepLibrary).Code; }

        string OutlineScenarioCode
        {
            get
            {
                var Result = string.Empty;
                var Outline = ScenarioCode;

                for (var Row = 0; Row < Scenario.Examples.Count; Row++)
                {
                    var CurrentCode = ReplaceNameIn(Outline, Row);

                    for (var Col = 0; Col < Scenario.Examples.Header.Count; Col++) 
                        CurrentCode = ReplaceExampleIn(CurrentCode, Row, Col);

                    Result += CurrentCode;
                }

                return Result;
            }
        }

        string ReplaceNameIn(string Outline, int Row)
        {
            return Outline.Replace
            (
                Scenario.Name + "()", 
                Scenario.Name + (Row + 1) + "()"
            );
        }

        string ReplaceExampleIn(string Outline, int Row, int Col)
        {
            return Outline.Replace
            (
                Scenario.Examples.Header[Col].Quote().Quote(), 
                ArgFormatter.ValueOf(Scenario.Examples[Row, Col])
            )
            .Replace
            (
                Scenario.Examples.Header[Col].Quote(), 
                ArgFormatter.ValueOf(Scenario.Examples[Row, Col])
            );
        }
    }
}
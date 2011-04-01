using System.Linq;
using Raconteur.Compilers;
using Raconteur.Helpers;

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

        string CodeFor(Step Step) { return new StepGenerator(Step).Code; }

        string OutlineScenarioCode
        {
            get
            {
                var Result = string.Empty;
                var Outline = ScenarioCode;

                foreach (var Example in Scenario.Examples)
                for (var Row = 0; Row < Example.Count; Row++)
                {
                    var CurrentCode = ReplaceNameIn(Outline, Row, Example);

                    for (var Col = 0; Col < Example.Header.Count; Col++) 
                        CurrentCode = ReplaceExampleIn(CurrentCode, Row, Col, Example);

                    Result += CurrentCode;
                }

                return Result;
            }
        }

        string ReplaceNameIn(string Outline, int Row, Table Example)
        {
            return Outline.Replace
            (
                Scenario.Name + "()", 
                Scenario.Name + "_" + Example.Name + (Row + 1) + "()"
            );
        }

        string ReplaceExampleIn(string Outline, int Row, int Col, Table Example)
        {
            var Value = ArgFormatter.ValueOf(Example[Row, Col]);
            
            return Outline
                .Replace(Example.Header[Col].Quoted().Quoted(), Value)
                .Replace(Example.Header[Col].Quoted(), Value);
        }
    }
}
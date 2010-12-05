using System.Linq;

namespace Raconteur.Generators
{
    public class RunnerGenerator : CodeGenerator
    {
        const string RunnerClass =

@"using {0};

namespace {1} 
{{
    {2}
    public partial class {3} 
    {{
{4}
    }}
}}
";
        readonly Feature Feature;

        public RunnerGenerator(Feature Feature)
        {
            this.Feature = Feature;
        }

        public string Code
        {
            get
            {
                return string.Format
                (
                    RunnerClass,
                    XUnitSettings.Namespace, 
                    Feature.Namespace,
                    XUnitSettings.ClassAttr, 
                    Feature.Name, 
                    ScenariosCode
                );
            }
        }

        string ScenariosCode
        {
            get 
            {
                return Feature.Scenarios.Aggregate(string.Empty,
                    (Scenarios, Scenario) => Scenarios + CodeFrom(Scenario)); 
            } 
        }

        string CodeFrom(Scenario Scenario)
        {
            return new ScenarioGenerator(Scenario).Code;
        }
    }
}
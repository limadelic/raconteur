using System.Linq;

namespace Raconteur.Generators
{
    public class RunnerGenerator : CodeGenerator
    {
        const string RunnerClass =

@"using {0};
{1}

namespace {2} 
{{
    {3}
    public partial class {4} 
    {{
{5}
{6}
    }}
}}
";
        const string Using =
@"using {0};
";

        const string StepLibraryVar =
@"        public {0} {0} = new {0}();
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
                    Settings.XUnit.Namespace, 
                    StepLibraryNamespace,
                    Feature.Namespace,
                    Settings.XUnit.ClassAttr, 
                    Feature.Name,
                    StepLibraryDeclaration, 
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
            return new ScenarioGenerator(Scenario, Feature.StepLibrary).Code;
        }

        string StepLibraryNamespace
        {
            get
            {
                return Feature.StepLibrary == null ? string.Empty :
                    string.Format(Using, Feature.StepLibrary.Namespace);
            }
        }

        string StepLibraryDeclaration
        {
            get
            {
                return Feature.StepLibrary == null ? string.Empty :
                    string.Format(StepLibraryVar, Feature.StepLibrary.Name);
            }
        }
    }
}
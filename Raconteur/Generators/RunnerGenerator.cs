using System;
using System.Linq;

namespace Raconteur.Generators
{
    public class RunnerGenerator : CodeGenerator
    {
        readonly Feature Feature;

        public RunnerGenerator(Feature Feature)
        {
            this.Feature = Feature;
        }

        const string RunnerClass =

@"using {0};{1}

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
                    StepLibraryDeclarations, 
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
            return new ScenarioGenerator(Scenario, Feature.StepLibraries).Code;
        }

        const string Using = "\r\n" + "using {0};";

        string StepLibraryNamespace
        {
            get { return AggregateLibraries(Using, Lib => Lib.Namespace); }
        }

        const string StepLibraryDeclaration =
@"        public {0} {0} = new {0}();
";
        string StepLibraryDeclarations
        {
            get { return AggregateLibraries(StepLibraryDeclaration, Lib => Lib.Name); }
        }

        string AggregateLibraries(string Template, Func<Type, string> FieldFrom)
        {
            return !Feature.HasStepLibraries ? null :
                Feature.StepLibraries.Aggregate(string.Empty, (Result, Lib) => Result + 
                    string.Format(Template, FieldFrom(Lib)));
        }
    }
}
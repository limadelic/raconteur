using System;
using System.Linq;
using Raconteur.Helpers;

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
                    StepDefinitionsNamespaces,
                    Feature.Namespace,
                    Settings.XUnit.ClassAttr, 
                    Feature.Name,
                    StepDefinitionsDeclarations, 
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
            return new ScenarioGenerator(Scenario, Feature.StepDefinitions).Code;
        }

        const string Using = "\r\n" + "using {0};";

        string StepDefinitionsNamespaces
        {
            get { return AggregateStepDefinitions(Using, Steps => Steps.Namespace); }
        }

        const string StepDefinitionsDeclaration =
@"        public {0} {0} = new {0}();
";
        string StepDefinitionsDeclarations
        {
            get { return AggregateStepDefinitions(StepDefinitionsDeclaration, Steps => Steps.Name); }
        }

        string AggregateStepDefinitions(string Template, Func<Type, string> FieldFrom)
        {
            return !Feature.HasStepDefinitions ? null :
                Feature.StepDefinitions.Aggregate(string.Empty, (Result, Steps) => 
                {
                    var CurrentLine = string.Format(Template, FieldFrom(Steps));

                    return Result.Contains(CurrentLine) ? Result : Result + CurrentLine;
                });
        }
    }
}
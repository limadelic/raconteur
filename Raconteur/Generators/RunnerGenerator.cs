using System;
using System.Collections.Generic;
using System.Linq;

namespace Raconteur.Generators
{
    public class RunnerGenerator
    {
        const string RunnerClass = 

@"using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace {0} 
{{
    [TestClass]
    public partial class {1} 
    {{
{2}
    }}
}}
";

        private const string ScenarioDeclaration = 
@"        
        [TestMethod]
        public void {0}()
        {{ {1}
        }}
";

        private const string StepExecution = 
@"        
            {0}({1});";

        Feature Feature;
        public string RunnerFor(Feature Feature)
        {
            this.Feature = Feature;
            
            return string.Format
            (
                RunnerClass, 
                Feature.Namespace, 
                Feature.Name, 
                ScenariosCode
            );
        }

        string ScenariosCode
        {
            get 
            {
                return Feature.Scenarios.Aggregate(string.Empty,
                    (Scenarios, Scenario) => 
                        Scenarios + ScenarioCodeFrom(Scenario)); 
            } 
        }

        string ScenarioCodeFrom(Scenario Scenario) 
        {
            return (Scenario.IsOutline) ?
                ScenarioCodeFromOutline(Scenario):
                ScenarioCodeFromSimple(Scenario);
        }

        string ScenarioCodeFromOutline(Scenario Scenario)
        {
            var OutlineCode = ScenarioCodeFromSimple(Scenario);

            var i = 1;
            return Scenario.Examples.Rows.Skip(1)
                .Aggregate(string.Empty, (Scenarios, Example) => Scenarios + 
                    ScenarioCodeFromOutlineExample(Scenario, i++, Example, OutlineCode));
        }

        string ScenarioCodeFromOutlineExample(Scenario Scenario, int Index, List<string> Example, string OutlineCode)
        {
            OutlineCode = OutlineCode.Replace(Scenario.Name + "()", Scenario.Name + Index + "()");

            for (var i = 0; i < Scenario.Examples.Header.Count; i++)
                OutlineCode = OutlineCode.Replace
                (
                    "\"" + Scenario.Examples.Header[i] + "\"", 
                    ArgFormatter.ValueOf(Example[i])
                );

            return OutlineCode;
        }

        string ScenarioCodeFromSimple(Scenario Scenario) 
        {
            var StepCode = Scenario.Steps.Aggregate(string.Empty,
                (Steps, Step) => Steps + ExecuteStep(Step));

            return string.Format(ScenarioDeclaration, Scenario.Name, StepCode);
        }

        string ExecuteStep(Step Step) 
        {
            return Step.HasTable ?
                ExecuteStepWithTable(Step) :
                ExecuteSimpleStep(Step);
        }

        string ExecuteStepWithTable(Step Step)
        {
            return Step.Table.Rows.Skip(1)
                .Aggregate(string.Empty, (Steps, Row) => 
                    Steps + ExecuteStepRow(Step, Row));
        }

        string ExecuteStepRow(Step Step, List<string> Row) 
        { 
            return ExecuteSimpleStep
            (
                new Step
                {
                    Name = Step.Name,
                    Args = Step.Args.Union(Row).ToList()
                }
            );
        }

        string ExecuteSimpleStep(Step Step) 
        {
            var ArgsValues = Step.Args.Select(ArgFormatter.ValueOf);

            var Args = string.Join(", ", ArgsValues);
            return string.Format(StepExecution, Step.Name, Args);
        }
    }
}
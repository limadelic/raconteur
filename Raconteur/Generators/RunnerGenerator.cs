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
            return string.Format(RunnerClass, 
                Feature.Namespace, 
                Feature.Name, 
                ScenariosImpl);
        }

        string ScenariosImpl
        {
            get 
            {
                return Feature.Scenarios.Aggregate("",
                    (Scenarios, Scenario) => 
                        Scenarios + ScenarioCodeFrom(Scenario)); 
            } 
        }

        string ScenarioCodeFrom(Scenario Scenario)
        {
            var StepCode = Scenario.Steps.Aggregate("",
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
            var FixedArgs = Step.Args.ToList();

            return Step.Table.Rows.Skip(1)
                .Aggregate(string.Empty, (Steps, Row) => 
                    Steps + ExecuteStepRow(Step, Row, FixedArgs));
        }

        string ExecuteStepRow(Step Step, List<string> Row, List<string> FixedArgs) 
        { 
            Step.Args = FixedArgs.Union(Row).ToList();
            return ExecuteSimpleStep(Step);
        }

        string ExecuteSimpleStep(Step Step) {
            var ArgsValues = Step.Args.Select(ArgFormatter.ValueOf);

            var Args = string.Join(", ", ArgsValues);
            return string.Format(StepExecution, Step.Name, Args);
        }
    }
}
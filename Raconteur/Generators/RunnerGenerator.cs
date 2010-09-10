using System.Collections.Generic;
using System.Linq;

namespace Raconteur.Generators
{
    public class RunnerGenerator
    {
        const string FeatureDeclaration = 

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
            {0}();";

        private const string StepDeclaration = 
@"        
        void {0}()
        {{ 
            throw new System.NotImplementedException(""Pending Step {0}"");
        }}
";

        readonly Feature Feature;

        public RunnerGenerator(Feature Feature)
        {
            this.Feature = Feature;
        }

        public string Runner
        {
            get
            {
                return string.Format(FeatureDeclaration, 
                    Feature.Namespace, 
                    Feature.FileName, 
                    ScenariosImpl);
            }
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

        string ExecuteStep(string Step)
        {
            return string.Format(StepExecution, Step);
        }

        public string DeclareStep(string Step) 
        { 
            return string.Format(StepDeclaration, Step);
        }

        string StepsDeclaration 
        {
            get
            {
                return Feature.Scenarios.Aggregate(new List<string>() as IEnumerable<string>,
                    (Steps, Scenario) => Steps.Union(Scenario.Steps))
                    .Aggregate("", (Steps, Step) => Steps + DeclareStep(Step));
            } 
        }
    }
}
using System.Linq;

namespace Raconteur.Generators
{
    public class RunnerGenerator : CodeGenerator
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
                    Feature.Namespace, 
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
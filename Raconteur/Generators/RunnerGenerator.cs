using System.Text;

namespace Raconteur.Generators
{
    public class RunnerGenerator
    {
        const string FeatureDeclaration = 

@"using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace {0} 
{{
    [TestClass]
    public class {1}Runner 
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
        Feature Feature;

        public string RunnerFor(Feature Feature)
        {
            this.Feature = Feature;
            return BuildRunnerCode();
        }

        string BuildRunnerCode()
        {
            var ScenarioCode = new StringBuilder();

            Feature.Scenarios.ForEach(Scenario => 
                ScenarioCode.Append(ScenarioCodeFrom(Scenario)));
            
            return FeatureCodeFrom(ScenarioCode.ToString());
        }

        string FeatureCodeFrom(string ScenarioCode)
        {
            return string.Format(FeatureDeclaration, Feature.Namespace, Feature.FileName, ScenarioCode);
        }

        static string ScenarioCodeFrom(Scenario Scenario)
        {
            var StepCode = string.Empty;
            Scenario.Steps.ForEach(Step => StepCode += @"
            " + Step + "();");

            return string.Format(ScenarioDeclaration, Scenario.Name, StepCode);
        }

        protected string StepDefinitionFullClassName
        {
            get { return "StepDefinitions." + Feature.Name; } 
        }
    }
}
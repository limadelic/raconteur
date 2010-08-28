using System.Text;
using Raconteur.Parsers;

namespace Raconteur.Generators
{
    public class RunnerGenerator
    {
        private Feature Feature;
        private string Namespace;
        private string FeatureFileName;

        public string RunnerFor(FeatureFile FeatureFile)
        {
            Feature = Parser.FeatureFrom(FeatureFile.Content);
            Namespace = FeatureFile.Namespace;
            FeatureFileName = FeatureFile.Name;

            return BuildRunnerCode();
        }

        private string BuildRunnerCode()
        {
            var featureCode = new StringBuilder();
            var scenarioCode = new StringBuilder();

            Feature.Scenarios.ForEach(Scenario => scenarioCode.Append(ScenarioCodeFrom(Scenario)));
            
            featureCode.Append(FeatureCodeFrom(scenarioCode.ToString()));

            return featureCode.ToString();
        }

        private string FeatureCodeFrom(string ScenarioCode)
        {
            return string.Format(@"using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace {0} 
{{
    [TestClass]
    public class {1}Runner 
    {{
        {2} Steps = new {2}();
{3}
    }}
}}
", Namespace, FeatureFileName, StepDefinitionFullClassName, ScenarioCode);
        }

        private static string ScenarioCodeFrom(Scenario Scenario)
        {
            return @"
        [TestMethod]
        public void " + Scenario.Name + @"()
        {

        }
";
        }

        protected string StepDefinitionFullClassName
        {
            get { return "StepDefinitions." + Feature.Name; } 
        }

        public FeatureParser Parser { get; set; }
    }
}
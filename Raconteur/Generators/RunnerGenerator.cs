using System.Text;
using EnvDTE80;
using Raconteur.Parsers;

namespace Raconteur.Generators
{
    public class RunnerGenerator
    {
        private const string FeatureDeclaration = 

@"using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace {0} 
{{
    [TestClass]
    public class {1}Runner 
    {{
        readonly {2} Steps = new {2}();
{3}
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
            var ScenarioCode = new StringBuilder();

            Feature.Scenarios.ForEach(Scenario => 
                ScenarioCode.Append(ScenarioCodeFrom(Scenario)));
            
            return FeatureCodeFrom(ScenarioCode.ToString());
        }

        private string FeatureCodeFrom(string ScenarioCode)
        {
            return string.Format(FeatureDeclaration, Namespace, FeatureFileName, StepDefinitionFullClassName, ScenarioCode);
        }

        private static string ScenarioCodeFrom(Scenario Scenario)
        {
            var StepCode = string.Empty;
            Scenario.Steps.ForEach(Step => StepCode += @"
            Steps." + Step + "();");

            return string.Format(ScenarioDeclaration, Scenario.Name, StepCode);
        }

        protected string StepDefinitionFullClassName
        {
            get { return "StepDefinitions." + Feature.Name; } 
        }

        public FeatureParser Parser { get; set; }
    }
}
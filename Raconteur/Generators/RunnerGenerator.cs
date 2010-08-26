using System.Text;
using Raconteur.Parsers;

namespace Raconteur.Generators
{
    public class RunnerGenerator
    {
        private Feature Feature;

        public string RunnerFor(FeatureFile FeatureFile)
        {
            Feature = Parser.FeatureFrom(FeatureFile.Content);

            return BuildRunnerCode(FeatureFile);
        }

        private string BuildRunnerCode(FeatureFile FeatureFile)
        {
            var code = new StringBuilder();
            code.Append(
@"
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace " + FeatureFile.Namespace + @" 
{
    [TestClass]
    public class " + FeatureFile.Name + @"Runner 
    {
        " + StepDefinitionFullClassName + " Steps = new " + StepDefinitionFullClassName  + @"();
");
            Feature.Scenarios.ForEach( Scenario => code.Append(
@"
        [TestMethod]
        public void " + Scenario.Name + @"()
        {

        }"));

            code.Append(@"
    }
}
            ");

            return code.ToString();
        }

        protected string StepDefinitionFullClassName
        {
            get { return "StepDefinitions." + Feature.Name; } 
        }

        public FeatureParser Parser { get; set; }
    }
}
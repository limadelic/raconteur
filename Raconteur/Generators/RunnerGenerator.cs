using System;
using System.Collections.Generic;
using System.Linq;
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
        private const string StepDeclaration = 
@"        
        void {0}()
        {{ 
            throw new System.NotImplementedException(""Pending Step {0}"");
        }}
";
        Feature Feature;

        public string RunnerFor(Feature Feature)
        {
            this.Feature = Feature;

            return string.Format(FeatureDeclaration, 
                Feature.Namespace, 
                Feature.FileName, 
                ScenariosImpl,
                StepsDeclaration);
        }

        string ScenariosImpl
        {
            get 
            {
                return Feature.Scenarios.Aggregate(string.Empty,
                    (Scenarios, Scenario) => 
                        Scenarios + ScenarioCodeFrom(Scenario)); 
            } 
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

        public string DeclareStep(string Step) 
        { 
            return string.Format(StepDeclaration, Step);
        }

        protected string StepsDeclaration 
        {
            get
            {
                return Feature.Scenarios.Aggregate(new List<string>(),
                    (Steps, Scenario) => Steps.Union(Scenario.Steps).ToList())
                    .Aggregate("", (Steps, Step) => Steps + DeclareStep(Step));
            } 
        }
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features 
{
    [TestClass]
    public class GenerateScenarioRunner 
    {
        readonly StepDefinitions.GenerateScenario Steps = new StepDefinitions.GenerateScenario();
        
        [TestMethod]
        public void GenerateTestMethods()
        { 
            Steps.When_the_Scenario_for_a_feature_is_generated();
            Steps.Then_it_should_be_a_Test_Method();
            Steps.And_it_should_be_named_After_the_Scenario_name();
        }

    }
}

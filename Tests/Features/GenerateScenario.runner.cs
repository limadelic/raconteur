using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features 
{
    [TestClass]
    public partial class GenerateScenario 
    {
        
        [TestMethod]
        public void Test()
        {         
            Given_the_Feature(
@"Feature: Feature Name
In order to do something
Another thing should happen
");        
            The_Runner_should_contain(
@"[TestMethod]
public void ScenarioName() {}
");
        }
        
        [TestMethod]
        public void GenerateTestMethods()
        {         
            When_the_Scenario_for_a_feature_is_generated();        
            Then_it_should_be_a_Test_Method();        
            And_it_should_be_named_After_the_Scenario_name();
        }
        
        [TestMethod]
        public void GenerateStepCalls()
        {         
            When_a_Scenario_with_steps_is_generated();        
            it_should_call_each_step_in_order();
        }

    }
}

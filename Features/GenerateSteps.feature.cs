using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features 
{
    [TestClass]
    public class GenerateStepsRunner 
    {
        readonly StepDefinitions.GenerateSteps Steps = new StepDefinitions.GenerateSteps();
        
        [TestMethod]
        public void GenerateStepCalls()
        { 
            Steps.When_a_Scenario_with_steps_is_generated();
            Steps.it_should_call_each_step_in_order();
            Steps.and_declare_the_StepDefinition();
        }

    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features 
{
    [TestClass]
    public partial class GenerateSteps 
    {
        
        [TestMethod]
        public void GenerateStepCalls()
        {         
            When_a_Scenario_with_steps_is_generated();        
            it_should_call_each_step_in_order();
        }

    }
}

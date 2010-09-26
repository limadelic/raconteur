using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features 
{
    [TestClass]
    public partial class StepArguments 
    {
        
        [TestMethod]
        public void GenerateArguments()
        {         
            When_a_step_contains_arguments();        
            The_runner_should_pass_them_in_the_call();
        }

    }
}

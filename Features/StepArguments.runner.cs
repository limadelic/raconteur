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
        
        [TestMethod]
        public void TypeInference()
        {         
            When_an_argument_is_a_string();        
            It_should_be_passed_as_a_string();        
            When_an_argument_is_an_integer();        
            It_should_be_passed_as_a_number();        
            When_an_argument_is_a_date();        
            It_should_be_passed_as_a_date();
        }

    }
}

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
            When_an_argument_is_an_integer();        
            It_should_be_passed_as_a_number();
        }
        
        [TestMethod]
        public void MultilineArg()
        {         
            When_an_Arg_is_not_finish_in_a_Sentence();        
            It_should_expand_until_it_s_closed_in_another_line();
        }

    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features 
{
    [TestClass]
    public partial class StepArguments 
    {
        
        [TestMethod]
        public void GenerateArguments()
        {         
            Given_the_Feature_contains(
@"Scenario: Scenario Name
If ""X"" happens
");        
            The_Runner_should_contain(
@"If__happens(""X"");
");
        }
        
        [TestMethod]
        public void TypeInference()
        {         
            Given_the_Feature_contains(
@"Scenario: Scenario Name
If the balance is ""42""
");        
            The_Runner_should_contain(
@"If_the_balance_is(42);
");
        }
        
        [TestMethod]
        public void MultilineArg()
        {         
            When_an_Arg_is_not_finish_in_a_Sentence();        
            It_should_expand_until_it_s_closed_in_another_line();
        }

    }
}

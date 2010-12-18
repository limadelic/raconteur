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
@"
Scenario: Scenario Name
If ""X"" happens
");        
            The_Runner_should_contain(
@"
If__happens(""X"");
");
        }
        
        [TestMethod]
        public void TypeInference()
        {         
            Given_the_Feature_contains(
@"
Scenario: Scenario Name
If the balance is ""42""
");        
            The_Runner_should_contain(
@"
If_the_balance_is(42);
");
        }
        
        [TestMethod]
        public void MultilineArg()
        {         
            Given_the_Feature_contains(
@"
Scenario: Multiline Arg
Step Arg with multiple lines
""
could start on one line
and finish on another
""
");        
            The_Runner_should_contain(
@"
Step_Arg_with_multiple_lines(
@""
could start on one line
and finish on another
"");
");
        }

    }
}

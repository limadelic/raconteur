using Microsoft.VisualStudio.TestTools.UnitTesting;
using Features.StepDefinitions;

namespace Features 
{
    [TestClass]
    public partial class StepArguments 
    {
        public FeatureRunner FeatureRunner = new FeatureRunner();
        public HighlightFeature HighlightFeature = new HighlightFeature();

        
        [TestMethod]
        public void GenerateArguments()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
Scenario: Scenario Name
If ""X"" happens
");        
            FeatureRunner.The_Runner_should_contain(
@"
If__happens(""X"");
");
        }
        
        [TestMethod]
        public void TypeInference()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
Scenario: Scenario Name
If the balance is ""42""
");        
            FeatureRunner.The_Runner_should_contain(
@"
If_the_balance_is(42);
");
        }
        
        [TestMethod]
        public void MultilineArg()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
Scenario: Multiline Arg
Step Arg with multiple lines
""
could start on one line
and finish on another
""
");        
            FeatureRunner.The_Runner_should_contain(
@"
Step_Arg_with_multiple_lines(
@""
could start on one line
and finish on another
"");
");
        }
        
        [TestMethod]
        public void StepStartingWithArg()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
Scenario: Name
""
Multiline Arg
""
before a Step
");        
            FeatureRunner.The_Runner_should_contain(
@"
before_a_Step(
@""
Multiline Arg
"");
");
        }

    }
}

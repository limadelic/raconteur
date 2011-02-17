using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features 
{
    [TestClass]
    public partial class ReusingSteps 
    {
        
        [TestMethod]
        public void UsingStepsFromAStepLibrary()
        {         
            Given_the_Feature_contains(
@"
using Step Library
Scenario: Reuse a Step from the lib
Step from Lib
");        
            The_Runner_should_contain(
@"
public StepLibrary StepLibrary = new StepLibrary();
[TestMethod]
public void ReuseAStepFromTheLib()
{
StepLibrary.Step_from_Lib();
}
");
        }
        
        [TestMethod]        
        [Ignore]
        public void UsingStepsFromAnotherFeature()
        { 
        }

    }
}

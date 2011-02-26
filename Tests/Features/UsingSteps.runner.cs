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
        public void UsingStepsFromMultipleStepLibraries()
        {         
            Given_the_Feature_contains(
@"
using Step Library
using another Step Library
Scenario: Reuse Steps from libs
Step from Lib
Step from another Lib
");        
            The_Runner_should_contain(
@"
public StepLibrary StepLibrary = new StepLibrary();
public AnotherStepLibrary AnotherStepLibrary = new AnotherStepLibrary();
[TestMethod]
public void ReuseStepsFromLibs()
{
StepLibrary.Step_from_Lib();
AnotherStepLibrary.Step_from_another_Lib();
}
");
        }

    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Features;

namespace Features 
{
    [TestClass]
    public partial class ReusingSteps 
    {
        public StepDefinitions StepDefinitions = new StepDefinitions();
        public AnotherStepDefinitions AnotherStepDefinitions = new AnotherStepDefinitions();

        
        [TestMethod]
        public void ReusingStepDefinitions()
        {         
            Given_the_Feature_contains(
@"
using Step Definitions
Scenario: Reuse a Step
Step from Step Definitions
");        
            The_Runner_should_contain(
@"
public StepDefinitions StepDefinitions = new StepDefinitions();
[TestMethod]
public void ReuseAStep()
{
StepDefinitions.Step_from_Step_Definitions();
}
");
        }
        
        [TestMethod]
        public void UsingStepsFromMultipleStepDefinitions()
        {         
            Given_the_Feature_contains(
@"
using Step Definitions
using another Step Definitions
Scenario: Reuse Steps from multiple Definitions
Step from Step Definitions
Step from another Step Definitions
");        
            The_Runner_should_contain(
@"
public StepDefinitions StepDefinitions = new StepDefinitions();
public AnotherStepDefinitions AnotherStepDefinitions = new AnotherStepDefinitions();
[TestMethod]
public void ReuseStepsFromMultipleDefinitions()
{
StepDefinitions.Step_from_Step_Definitions();
AnotherStepDefinitions.Step_from_another_Step_Definitions();
}
");
        }

    }
}

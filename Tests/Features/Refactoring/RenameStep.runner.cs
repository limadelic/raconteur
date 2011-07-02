using MbUnit.Framework;
using Features.StepDefinitions;

namespace Features.Refactoring 
{
    [TestFixture]
    public partial class RenameStep 
    {
        public FeatureRunner FeatureRunner = new FeatureRunner();
        public HighlightFeature HighlightFeature = new HighlightFeature();

        
        [Test]
        public void RenameWithinFeature()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
Scenario: Name
Step
Step
");        
            When__is_renamed_to("Step", "new Step");        
            The_Feature_should_contain(
@"
Scenario: Name
new Step
new Step
");
        }
        
        [Test]
        public void StepWithArgs()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
Scenario: Name
simple Step
Step ""with Arg""
");        
            When__is_renamed_to("Step", "new Step");        
            The_Feature_should_contain(
@"
Scenario: Name
simple Step
new Step ""with Arg""
");
        }
        
        [Test]
        public void StepWithMultiline()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
Scenario: With Multiline Arg in Step
Step
""
Multiline Arg
""
");        
            When__is_renamed_to("Step", "new Step");        
            The_Feature_should_contain(
@"
Scenario: With Multiline Arg in Step
new Step
""
Multiline Arg
""
");
        }

    }
}

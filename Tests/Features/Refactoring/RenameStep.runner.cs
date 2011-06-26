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

    }
}

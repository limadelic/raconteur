using MbUnit.Framework;
using Features.StepDefinitions;

namespace Features 
{
    [TestFixture]
    public partial class IgnoringScenarios 
    {
        public FeatureRunner FeatureRunner = new FeatureRunner();
        public HighlightFeature HighlightFeature = new HighlightFeature();

        
        [Test]
        public void IgnoreAScenario()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
@ignore
Scenario: Ignored
");        
            FeatureRunner.The_Runner_should_contain(
@"
[TestMethod]
[Ignore]
public void Ignored()
{
}
");
        }
        
        [Test]
        public void IgnoreWithAReason()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
@ignore just because
Scenario: Ignored
");        
            FeatureRunner.The_Runner_should_contain(
@"
[TestMethod]
[Ignore] // just because
public void Ignored()
{
}
");
        }

    }
}

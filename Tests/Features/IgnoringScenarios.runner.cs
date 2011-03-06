using Microsoft.VisualStudio.TestTools.UnitTesting;
using Features.StepDefinitions;

namespace Features 
{
    [TestClass]
    public partial class IgnoringScenarios 
    {
        public FeatureRunner FeatureRunner = new FeatureRunner();
        public HighlightFeature HighlightFeature = new HighlightFeature();

        
        [TestMethod]
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
        
        [TestMethod]
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

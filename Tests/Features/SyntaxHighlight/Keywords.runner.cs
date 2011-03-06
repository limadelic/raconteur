using Microsoft.VisualStudio.TestTools.UnitTesting;
using Features.StepDefinitions;

namespace Features.SyntaxHighlight 
{
    [TestClass]
    public partial class HighlightKeywords 
    {
        public FeatureRunner FeatureRunner = new FeatureRunner();
        public HighlightFeature HighlightFeature = new HighlightFeature();

        
        [TestMethod]
        public void FeatureAndScenarios()
        {         
            FeatureRunner.Given_the_Feature_is(
@"
Feature: Name
Scenario: First
Scenario: Second
");        
            HighlightFeature.Raconteur_should_highlight(1, "Feature:", "Keyword");        
            HighlightFeature.Raconteur_should_highlight(2, "Scenario:", "Keyword");
        }

    }
}

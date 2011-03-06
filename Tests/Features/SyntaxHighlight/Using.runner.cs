using Microsoft.VisualStudio.TestTools.UnitTesting;
using Features.StepDefinitions;

namespace Features.SyntaxHighlight 
{
    [TestClass]
    public partial class HighlightUsing 
    {
        public FeatureRunner FeatureRunner = new FeatureRunner();
        public HighlightFeature HighlightFeature = new HighlightFeature();

        
        [TestMethod]
        public void FeatureAndScenarios()
        {         
            FeatureRunner.Given_the_Feature_is(
@"
Feature: Name
using Step Definitions
Scenario: First
using after Scenario
");        
            HighlightFeature.Raconteur_should_highlight_like_a("Comment", "using Step Definitions");        
            HighlightFeature.Raconteur_should_not_highlight("using after Scenario");
        }

    }
}

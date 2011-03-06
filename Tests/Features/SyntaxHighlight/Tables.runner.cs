using Microsoft.VisualStudio.TestTools.UnitTesting;
using Features.StepDefinitions;

namespace Features.SyntaxHighlight 
{
    [TestClass]
    public partial class TableValues 
    {
        public FeatureRunner FeatureRunner = new FeatureRunner();
        public HighlightFeature HighlightFeature = new HighlightFeature();

        
        [TestMethod]
        public void TableValuesAreDisplayedLikeStrings()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
Scenario: Name
Step with Table
[ X | Y ]
| a | b |
| c | d |
");        
            HighlightFeature.Raconteur_should_highlight_like_a("String", "a");        
            HighlightFeature.Raconteur_should_highlight_like_a("String", "b");        
            HighlightFeature.Raconteur_should_highlight_like_a("String", "c");        
            HighlightFeature.Raconteur_should_highlight_like_a("String", "d");
        }

    }
}

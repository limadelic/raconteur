using Microsoft.VisualStudio.TestTools.UnitTesting;
using Features.StepDefinitions;

namespace Features.SyntaxHighlight 
{
    [TestClass]
    public partial class HighlightTags 
    {
        public FeatureRunner FeatureRunner = new FeatureRunner();
        public HighlightFeature HighlightFeature = new HighlightFeature();

        
        [TestMethod]
        public void HighlightAllTags()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
@tag @tag
@tag
Scenario: Name
");        
            HighlightFeature.Raconteur_should_highlight___like_a(3, "@tag", "Comment");
        }

    }
}

using MbUnit.Framework;
using Features.StepDefinitions;

namespace Features.SyntaxHighlight 
{
    [TestFixture]
    public partial class HighlightTags 
    {
        public FeatureRunner FeatureRunner = new FeatureRunner();
        public HighlightFeature HighlightFeature = new HighlightFeature();

        
        [Test]
        public void HighlightAllTags()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
@tag @tag
@multiword tag
Scenario: Name
");        
            HighlightFeature.Raconteur_should_highlight_like_a("Comment", 1, "@tag @tag");        
            HighlightFeature.Raconteur_should_highlight_like_a("Comment", 1, "@multiword tag");
        }

    }
}

using MbUnit.Framework;
using Features.StepDefinitions;

namespace Features.SyntaxHighlight 
{
    [TestFixture]
    public partial class HighlightComments 
    {
        public FeatureRunner FeatureRunner = new FeatureRunner();
        public HighlightFeature HighlightFeature = new HighlightFeature();

        
        [Test]
        public void SingleLineComments()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
Scenario: Name
// Comment
Step
// Scenario:
");
        }
        
        [Test]
        public void Multi_lineComments()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
Scenario: Name
/*
Scenario: Commented
*/
");        
            HighlightFeature.Raconteur_should_highlight_like_a("Comment", 
@"
/*
Scenario: Commented
*/
");
        }
        
        [Test]
        public void UnclosedMultilineComments()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
Scenario: Name
/*
Scenario: Commented
/*
""
");        
            HighlightFeature.Raconteur_should_highlight_like_a("Comment", 
@"
/*
Scenario: Commented
/*
""
");
        }

    }
}

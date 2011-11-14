using MbUnit.Framework;
using Features.StepDefinitions;

namespace Features.SyntaxHighlight 
{
    [TestFixture]
    public partial class TableValues 
    {
        public FeatureRunner FeatureRunner = new FeatureRunner();
        public HighlightFeature HighlightFeature = new HighlightFeature();

        
        [Test]
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
            HighlightFeature.Raconteur_should_highlight_like_a("String", "d");        
            HighlightFeature.Raconteur_should_highlight_like_a("String", "b");        
            HighlightFeature.Raconteur_should_highlight_like_a("String", "c");
        }
        
        [Test]
        public void TableHeadersAreNotHighlighted()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
Scenario: Name

Step with Table
[ X | Y ]
| a | b |

Scenario Outline: Name

Step with X

Examples:
[ X ]
| a |
");        
            HighlightFeature.Raconteur_should_not_highlight("X");        
            HighlightFeature.Raconteur_should_not_highlight("Y");
        }

    }
}

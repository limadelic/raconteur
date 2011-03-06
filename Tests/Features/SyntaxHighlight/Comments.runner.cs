using Microsoft.VisualStudio.TestTools.UnitTesting;
using Features.StepDefinitions;

namespace Features.SyntaxHighlight 
{
    [TestClass]
    public partial class HighlightComments 
    {
        public FeatureRunner FeatureRunner = new FeatureRunner();
        public HighlightFeature HighlightFeature = new HighlightFeature();

        
        [TestMethod]
        public void SingleLineComments()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
Scenario: Name
// Comment
Step
// Scenario:
");        
            HighlightFeature.Raconteur_should_highlight(1, "// Comment", "Comment");        
            HighlightFeature.Raconteur_should_highlight(1, "// Scenario:", "Comment");        
            HighlightFeature.Raconteur_should_highlight(1, "Scenario:", "Keyword");
        }
        
        [TestMethod]
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
            HighlightFeature.Raconteur_should_highlight(1, "Scenario:", "Keyword");
        }
        
        [TestMethod]
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

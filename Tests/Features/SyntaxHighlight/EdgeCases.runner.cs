using Microsoft.VisualStudio.TestTools.UnitTesting;
using Features.StepDefinitions;

namespace Features.SyntaxHighlight 
{
    [TestClass]
    public partial class TestHiglightingEdgeCases 
    {
        public FeatureRunner FeatureRunner = new FeatureRunner();
        public HighlightFeature HighlightFeature = new HighlightFeature();

        
        [TestMethod]
        public void Keywords_Comments_TablesInMultilineArgDisplayLikeArg()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
""
Scenario: Name
// Single Line Comment
/*
MultiLine Comment
*/
[ Table			  ]
|  value in table |
""
");        
            HighlightFeature.Raconteur_should_highlight_like_a("String", 
@"
""
Scenario: Name
// Single Line Comment
/*
MultiLine Comment
*/
[ Table			  ]
|  value in table |
""
");        
            HighlightFeature.Raconteur_should_not_highlight("Scenario:");        
            HighlightFeature.Raconteur_should_not_highlight("// Single Line Comment");        
            HighlightFeature.Raconteur_should_not_highlight("value in table");        
            HighlightFeature.Raconteur_should_not_highlight(
@"
/*
MultiLine Comment
*/
");
        }
        
        [TestMethod]
        public void Keywords_Args_TablesInsideMultilineCommentsDisplayLikeAComment()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
/*
Scenario: Name
Step ""Arg""
""
MultiLine Arg
""
[ Table			  ]
|  value in table |
*/
");        
            HighlightFeature.Raconteur_should_highlight_like_a("Comment", 
@"
/*
Scenario: Name
Step ""Arg""
""
MultiLine Arg
""
[ Table			  ]
|  value in table |
*/
");        
            HighlightFeature.Raconteur_should_not_highlight("Scenario:");        
            HighlightFeature.Raconteur_should_not_highlight("\"Args\"");        
            HighlightFeature.Raconteur_should_not_highlight("value in table");        
            HighlightFeature.Raconteur_should_not_highlight(
@"
""
MultiLine Arg
""
");
        }

    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features.SyntaxHighlight 
{
    [TestClass]
    public partial class TestHiglightingEdgeCases 
    {
        
        [TestMethod]
        public void KeywordsAndCommentsInMultilineArgDisplayLikeArg()
        {         
            Given_the_Feature_contains(
@"
""
Scenario: Name
// Single Line Comment
/*
MultiLine Comment
*/
""
");        
            Raconteur_should_highlight_like_a("String", 
@"
""
Scenario: Name
// Single Line Comment
/*
MultiLine Comment
*/
""
");        
            Raconteur_should_not_highlight("Scenario:");        
            Raconteur_should_not_highlight("// Single Line Comment");        
            Raconteur_should_not_highlight(
@"
/*
MultiLine Comment
*/
");
        }
        
        [TestMethod]
        public void KeywordsAndArgsInsideMultilineCommentsDisplayLikeAComment()
        {         
            Given_the_Feature_contains(
@"
/*
Scenario: Name
Step ""Arg""
""
MultiLine Arg
""
*/
");        
            Raconteur_should_highlight_like_a("Comment", 
@"
/*
Scenario: Name
Step ""Arg""
""
MultiLine Arg
""
*/
");        
            Raconteur_should_not_highlight("Scenario:");        
            Raconteur_should_not_highlight("\"Args\"");        
            Raconteur_should_not_highlight(
@"
""
MultiLine Arg
""
");
        }

    }
}

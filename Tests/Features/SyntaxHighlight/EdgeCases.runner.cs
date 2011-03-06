using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features.SyntaxHighlight 
{
    [TestClass]
    public partial class TestHiglightingEdgeCases 
    {

        
        [TestMethod]
        public void Keywords_Comments_TablesInMultilineArgDisplayLikeArg()
        {         
            Given_the_Feature_contains(
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
            Raconteur_should_highlight_like_a("String", 
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
            Raconteur_should_not_highlight("Scenario:");        
            Raconteur_should_not_highlight("// Single Line Comment");        
            Raconteur_should_not_highlight("value in table");        
            Raconteur_should_not_highlight(
@"
/*
MultiLine Comment
*/
");
        }
        
        [TestMethod]
        public void Keywords_Args_TablesInsideMultilineCommentsDisplayLikeAComment()
        {         
            Given_the_Feature_contains(
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
            Raconteur_should_highlight_like_a("Comment", 
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
            Raconteur_should_not_highlight("Scenario:");        
            Raconteur_should_not_highlight("\"Args\"");        
            Raconteur_should_not_highlight("value in table");        
            Raconteur_should_not_highlight(
@"
""
MultiLine Arg
""
");
        }

    }
}

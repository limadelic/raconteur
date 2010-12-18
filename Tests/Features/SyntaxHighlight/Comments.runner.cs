using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features.SyntaxHighlight 
{
    [TestClass]
    public partial class HighlightComments 
    {
        
        [TestMethod]
        public void SingleLineComments()
        {         
            Given_the_Feature_is(
@"
Feature: Name
Scenario: First
// Comment
Step
// Scenario:
");        
            Raconteur_should_highlight(1, "// Comment", "Comment");        
            Raconteur_should_highlight(1, "// Scenario:", "Comment");        
            Raconteur_should_highlight(1, "Scenario:", "Keyword");
        }
        
        [TestMethod]
        public void Multi_lineComments()
        {         
            Given_the_Feature_is(
@"
Feature: Name
Scenario: First
/*
Scenario: Commented
*/
");        
            Raconteur_should_highlight_like_a("Comment", 
@"
/*
Scenario: Commented
*/
");        
            Raconteur_should_highlight(1, "Scenario:", "Keyword");
        }

    }
}

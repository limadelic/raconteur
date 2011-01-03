using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features.SyntaxHighlight 
{
    [TestClass]
    public partial class HighlightTags 
    {
        
        [TestMethod]
        public void HighlightAllTags()
        {         
            Given_the_Feature_contains(
@"
@tag @tag
@tag
Scenario: Name
");        
            Raconteur_should_highlight___like_a(3, "@tag", "Comment");
        }

    }
}

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
@"Feature: Name
Scenario: First
// One Comment
One Step
// Another Comment
");        
            Raconteur_should_highlight(1, "// One Comment", "Comment");        
            Raconteur_should_highlight(1, "// Another Comment", "Comment");
        }

    }
}

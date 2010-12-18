using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features.SyntaxHighlight 
{
    [TestClass]
    public partial class TableValues 
    {
        
        [TestMethod]
        public void TableValuesAreDisplayedLikeStrings()
        {         
            Given_the_Feature_contains(
@"
Scenario: Name
Step with Table
[ X | Y ]
| a | b |
| c | d |
");        
            Raconteur_should_highlight_like_a("String", "a");        
            Raconteur_should_highlight_like_a("String", "b");        
            Raconteur_should_highlight_like_a("String", "c");        
            Raconteur_should_highlight_like_a("String", "d");
        }

    }
}

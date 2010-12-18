using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features.SyntaxHighlight 
{
    [TestClass]
    public partial class HighlightArgs 
    {
        
        [TestMethod]
        public void SingleLineArg()
        {         
            Given_the_Feature_contains(
@"
Scenario: Name
Step with ""Arg1"" and ""Arg2""
// ""Commented Arg""
");        
            Raconteur_should_highlight_like_a("String", 1, "\"Arg1\"");        
            Raconteur_should_highlight_like_a("String", 1, "\"Arg2\"");        
            Raconteur_should_highlight_like_a("String", 0, "\"Commented Arg\"");
        }
        
        [TestMethod]
        public void MultilineArg()
        {         
            Given_the_Feature_contains(
@"
Scenario: Name
Step with
""
Multiline Arg
""
");        
            Raconteur_should_highlight_like_a("String", 
@"
""
Multiline Arg
""
");
        }

    }
}

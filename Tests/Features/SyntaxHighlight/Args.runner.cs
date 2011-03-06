using Microsoft.VisualStudio.TestTools.UnitTesting;
using Features.StepDefinitions;

namespace Features.SyntaxHighlight 
{
    [TestClass]
    public partial class HighlightArgs 
    {
        public HighlightFeature HighlightFeature = new HighlightFeature();
        public FeatureRunner FeatureRunner = new FeatureRunner();

        
        [TestMethod]
        public void SingleLineArg()
        {         
            HighlightFeature.Given_the_Feature_is(
@"
Scenario: Name
Step with ""Arg1"" and ""Arg2""
");        
            HighlightFeature.Raconteur_should_highlight_like_a("String", 1, "\"Arg1\"");        
            HighlightFeature.Raconteur_should_highlight_like_a("String", 1, "\"Arg2\"");
        }
        
        [TestMethod]
        public void MultilineArg()
        {         
            HighlightFeature.Given_the_Feature_is(
@"
Scenario: Name
Step with
""
Multiline Arg
""
");        
            HighlightFeature.Raconteur_should_highlight_like_a("String", 
@"
""
Multiline Arg
""
");
        }

    }
}

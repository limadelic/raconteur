using Microsoft.VisualStudio.TestTools.UnitTesting;
using Features.StepDefinitions;

namespace Features.SyntaxHighlight 
{
    [TestClass]
    public partial class HighlightArgs 
    {
        public FeatureRunner FeatureRunner = new FeatureRunner();
        public HighlightFeature HighlightFeature = new HighlightFeature();

        
        [TestMethod]
        public void SingleLineArg()
        {         
            FeatureRunner.Given_the_Feature_is(
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
            FeatureRunner.Given_the_Feature_is(
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

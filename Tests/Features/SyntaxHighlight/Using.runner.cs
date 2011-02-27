using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Features.SyntaxHighlight 
{
    [TestClass]
    public partial class HighlightUsing 
    {

        
        [TestMethod]
        public void FeatureAndScenarios()
        {         
            Given_the_Feature_is(
@"
Feature: Name
using Step Library
Scenario: First
");        
            Raconteur_should_highlight_like_a("Comment", "using Step Library");
        }

    }
}

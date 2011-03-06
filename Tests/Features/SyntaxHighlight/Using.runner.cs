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
using Step Definitions
Scenario: First
using after Scenario
");        
            Raconteur_should_highlight_like_a("Comment", "using Step Definitions");        
            Raconteur_should_not_highlight("using after Scenario");
        }

    }
}

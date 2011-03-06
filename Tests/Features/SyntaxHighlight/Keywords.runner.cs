using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features.SyntaxHighlight 
{
    [TestClass]
    public partial class HighlightKeywords 
    {

        
        [TestMethod]
        public void FeatureAndScenarios()
        {         
            Given_the_Feature_is(
@"
Feature: Name
Scenario: First
Scenario: Second
");        
            Raconteur_should_highlight(1, "Feature:", "Keyword");        
            Raconteur_should_highlight(2, "Scenario:", "Keyword");
        }

    }
}

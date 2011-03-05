using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features.SyntaxHighlight 
{
    [TestClass]
    public partial class HighlightUsing 
    {
        public StepDefinitions StepDefinitions = new StepDefinitions();

        
        [TestMethod]
        public void FeatureAndScenarios()
        {         
            Given_the_Feature_is(
@"
Feature: Name
using Step Definitions
Scenario: First
");        
            Raconteur_should_highlight_like_a("Comment", "using Step Definitions");
        }

    }
}

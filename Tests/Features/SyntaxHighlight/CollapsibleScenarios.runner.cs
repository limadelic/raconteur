using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features.SyntaxHighlight 
{
    [TestClass]
    public partial class MakeScenariosCollapsibles 
    {
        
        [TestMethod]
        public void ScenariosAreCollapsible()
        {         
            Given_the_Feature_is(
@"
Feature: Name
Scenario: First
Steps
Scenario: Second
Steps
");        
            Raconteur_should_allow_to_collapse(
@"
Scenario: First
Steps
");        
            Raconteur_should_allow_to_collapse(
@"
Scenario: Second
Steps
");
        }
        
        [TestMethod]
        public void SingleLineScenario()
        {         
            Given_the_Feature_contains("Scenario: First");        
            Raconteur_should_allow_to_collapse("Scenario: First");
        }

    }
}

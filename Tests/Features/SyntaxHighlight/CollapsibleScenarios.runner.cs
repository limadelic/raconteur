using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features.SyntaxHighlight 
{
    [TestClass]
    public partial class MakeScenariosCollapsibles 
    {
        
        [TestMethod]
        public void ScenariosAreCollapsible()
        {         
            Given_the_Feature_contains(
@"
Scenario: First
Steps
Scenario: Second
Step1
Step2
");        
            Raconteur_should_allow_to_collapse(
@"
Scenario: First
Steps
");        
            Raconteur_should_allow_to_collapse(
@"
Scenario: Second
Step1
Step2
");
        }
        
        [TestMethod]
        public void SingleLineScenario()
        {         
            Given_the_Feature_contains("Scenario: First");        
            Raconteur_should_allow_to_collapse("Scenario: First");
        }
        
        [TestMethod]
        public void FeatureWithSingleLineScenario()
        {         
            Given_the_Feature_contains(
@"
Feature: Name
Scenario: Single
");        
            Raconteur_should_allow_to_collapse("Scenario: Single");
        }
        
        [TestMethod]
        public void MultipleSingleLineScenarios()
        {         
            Given_the_Feature_contains(
@"
Scenario: First
Scenario: Second
Scenario: Third
");        
            Raconteur_should_allow_to_collapse("Scenario: First");        
            Raconteur_should_allow_to_collapse("Scenario: Second");        
            Raconteur_should_allow_to_collapse("Scenario: Third");
        }
        
        [TestMethod]
        public void ScenariosWithTags()
        {         
            Given_the_Feature_is(
@"
Feature: Name
Scenario: First
Steps
@tag
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
        public void ScenariosWithMultipleTags()
        {         
            Given_the_Feature_contains(
@"
@tag
Scenario: First
@tag
@another_tag
Scenario: Second
");        
            Raconteur_should_allow_to_collapse("Scenario: First");        
            Raconteur_should_not_allow_to_collapse(
@"
Scenario: First
@tag
");
        }
        
        [TestMethod]
        public void LastScenarioEndsInMultilineLine()
        {         
            Given_the_Feature_contains(
@"
Scenario: First
/*
");        
            Raconteur_should_allow_to_collapse(
@"
Scenario: First
/*
");
        }

    }
}

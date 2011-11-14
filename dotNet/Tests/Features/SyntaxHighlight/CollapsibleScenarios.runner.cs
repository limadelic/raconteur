using MbUnit.Framework;
using Features.StepDefinitions;

namespace Features.SyntaxHighlight 
{
    [TestFixture]
    public partial class MakeScenariosCollapsibles 
    {
        public FeatureRunner FeatureRunner = new FeatureRunner();
        public HighlightFeature HighlightFeature = new HighlightFeature();

        
        [Test]
        public void ScenariosAreCollapsible()
        {         
            FeatureRunner.Given_the_Feature_contains(
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
        
        [Test]
        public void SingleLineScenario()
        {         
            FeatureRunner.Given_the_Feature_contains("Scenario: First");        
            Raconteur_should_allow_to_collapse("Scenario: First");
        }
        
        [Test]
        public void FeatureWithSingleLineScenario()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
Feature: Name
Scenario: Single
");        
            Raconteur_should_allow_to_collapse("Scenario: Single");
        }
        
        [Test]
        public void MultipleSingleLineScenarios()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
Scenario: First
Scenario: Second
Scenario: Third
");        
            Raconteur_should_allow_to_collapse("Scenario: First");        
            Raconteur_should_allow_to_collapse("Scenario: Second");        
            Raconteur_should_allow_to_collapse("Scenario: Third");
        }
        
        [Test]
        public void ScenariosWithTags()
        {         
            FeatureRunner.Given_the_Feature_is(
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
        
        [Test]
        public void ScenariosWithMultipleTags()
        {         
            FeatureRunner.Given_the_Feature_contains(
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
        
        [Test]
        public void LastScenarioEndsInMultilineLine()
        {         
            FeatureRunner.Given_the_Feature_contains(
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

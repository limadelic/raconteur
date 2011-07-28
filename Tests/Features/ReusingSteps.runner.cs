using MbUnit.Framework;
using Features.StepDefinitions;

namespace Features 
{
    [TestFixture]
    public partial class ReusingSteps 
    {
        public FeatureRunner FeatureRunner = new FeatureRunner();
        public HighlightFeature HighlightFeature = new HighlightFeature();

        
        [Test]
        public void DefaultStepDefinition()
        {         
            FeatureRunner.Given_the_Feature_is(
@"
Feature: Reusing Steps

Scenario: Reuse a Step
Step
");        
            FeatureRunner.The_Runner_should_contain(
@"
[TestMethod]
public void ReuseAStep()
{
Step();
}
");
        }
        
        [Test]
        public void InheritedStepInDefaultStepDefinition()
        {         
            FeatureRunner.Given_the_Feature_is(
@"
Feature: Reusing Steps

Scenario: Reuse a Step
Inherited Step
");        
            FeatureRunner.The_Runner_should_contain(
@"
[TestMethod]
public void ReuseAStep()
{
Inherited_Step();
}
");
        }
        
        [Test]
        public void ReusingStepDefinitions()
        {         
            FeatureRunner.Given_the_setting__contains("Libraries", "Common");        
            FeatureRunner.Given_the_Feature_contains(
@"
using Step Definitions

Scenario: Reuse a Step
Step from Step Definitions
");        
            FeatureRunner.The_Runner_should_contain(
@"
public StepDefinitions StepDefinitions = new StepDefinitions();

[TestMethod]
public void ReuseAStep()
{
StepDefinitions.Step_from_Step_Definitions();
}
");
        }
        
        [Test]
        public void ReusingInheritedStepInStepDefinitions()
        {         
            FeatureRunner.Given_the_setting__contains("Libraries", "Common");        
            FeatureRunner.Given_the_Feature_contains(
@"
using Step Definitions

Scenario: Reuse a Step
Base Step
");        
            FeatureRunner.The_Runner_should_contain(
@"
public StepDefinitions StepDefinitions = new StepDefinitions();

[TestMethod]
public void ReuseAStep()
{
StepDefinitions.Base_Step();
}
");
        }
        
        [Test]
        public void ReusingStepsFromMultipleStepDefinitions()
        {         
            FeatureRunner.Given_the_setting__contains("Libraries", "Common");        
            FeatureRunner.Given_the_Feature_contains(
@"
using Step Definitions
using another Step Definitions

Scenario: Reuse Steps from multiple Definitions
Step from Step Definitions
Step from another Step Definitions
");        
            FeatureRunner.The_Runner_should_contain(
@"
public StepDefinitions StepDefinitions = new StepDefinitions();
public AnotherStepDefinitions AnotherStepDefinitions = new AnotherStepDefinitions();

[TestMethod]
public void ReuseStepsFromMultipleDefinitions()
{
StepDefinitions.Step_from_Step_Definitions();
AnotherStepDefinitions.Step_from_another_Step_Definitions();
}
");
        }
        
        [Test]
        public void ReusingGlobalStepDefinitions()
        {         
            FeatureRunner.Given_the_setting__contains("Libraries", "Common");        
            FeatureRunner.Given_the_setting__contains("StepDefinitions", "StepDefinitions");        
            FeatureRunner.Given_the_Feature_contains(
@"
Scenario: Reuse a Step in global Step Definition
Step from Step Definitions
");        
            FeatureRunner.The_Runner_should_contain(
@"
public StepDefinitions StepDefinitions = new StepDefinitions();

[TestMethod]
public void ReuseAStepInGlobalStepDefinition()
{
StepDefinitions.Step_from_Step_Definitions();
}
");
        }
        
        [Test]
        public void ReusingStepDefinitionsFromLibraries()
        {         
            FeatureRunner.Given_the_setting__contains("Libraries", "Common");        
            FeatureRunner.Given_the_Feature_contains(
@"
using Step Definitions in Library

Scenario: Reuse Steps from Library
Step from Step Definitions in Library
");        
            FeatureRunner.The_Runner_should_contain(
@"
public StepDefinitionsInLibrary StepDefinitionsInLibrary = new StepDefinitionsInLibrary();

[TestMethod]
public void ReuseStepsFromLibrary()
{
StepDefinitionsInLibrary.Step_from_Step_Definitions_in_Library();
}
");
        }
        
        [Test]
        public void ReusingStepDefinitionsFromRaconteur_Web()
        {         
            FeatureRunner.Given_the_setting__contains("Libraries", "Raconteur.Web");        
            FeatureRunner.Given_the_Feature_contains(
@"
using Browser

Scenario: Reuse Steps from Library
Title should be ""IMDb Top 250""
");        
            FeatureRunner.The_Runner_should_contain(
@"
public Browser Browser = new Browser();

[TestMethod]
public void ReuseStepsFromLibrary()
{
Browser.Title_should_be(""IMDb Top 250"");
}
");
        }

    }
}

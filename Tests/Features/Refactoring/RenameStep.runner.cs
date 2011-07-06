using MbUnit.Framework;
using Features.StepDefinitions;

namespace Features.Refactoring 
{
    [TestFixture]
    public partial class RenameStep 
    {
        public FeatureRunner FeatureRunner = new FeatureRunner();
        public HighlightFeature HighlightFeature = new HighlightFeature();

        
        [Test]
        public void StepWithinFeature()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
Scenario: Name
Step
Step
");        
            When__is_renamed_to("Step", "new Step");        
            The_Feature_should_contain(
@"
Scenario: Name
new Step
new Step
");
        }
        
        [Test]        
        [Category("wip")]
        public void StepSharedAmongFeatures()
        {         
            Given_the_Feature__contains("Alpha", 
@"
Scenario: Name
// Step in Alpha
Step
");        
            And_the_Feature__contains("Beta", 
@"
Scenario: Name
// Step in Beta
Step
");        
            When__used_in_multiple_features_is_renamed_to("Step", "new Step");        
            The_Feature__should_contain("Alpha", 
@"
Scenario: Name
// Step in Alpha
new Step
");        
            and_the_Feature__should_contain("Beta", 
@"
Scenario: Name
// Step in Beta
new Step
");
        }
        
        [Test]
        public void StepWithArgs()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
Scenario: Name
simple Step
Step ""with Arg""
");        
            When__is_renamed_to("Step", "new Step");        
            The_Feature_should_contain(
@"
Scenario: Name
simple Step
new Step ""with Arg""
");
        }
        
        [Test]
        public void StepWithMultilineArg()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
Scenario: With Multiline Arg in Step
Step
""
Multiline Arg
""
");        
            When__is_renamed_to("Step", "new Step");        
            The_Feature_should_contain(
@"
Scenario: With Multiline Arg in Step
new Step
""
Multiline Arg
""
");
        }
        
        [Test]
        public void StepWithTable()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
Scenario: Step with Table
Step
|X|0|X|
|X|X|0|
|0|0|X|
");        
            When__is_renamed_to("Step", "new Step");        
            The_Feature_should_contain(
@"
Scenario: Step with Table
new Step
|X|0|X|
|X|X|0|
|0|0|X|
");
        }
        
        [Test]
        public void StepWithHeaderTable()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
Scenario: Step with Header Table
Step
[X|Y]
|1|0|
|0|0|
");        
            When__is_renamed_to("Step", "new Step");        
            The_Feature_should_contain(
@"
Scenario: Step with Header Table
new Step
[X|Y]
|1|0|
|0|0|
");
        }
        
        [Test]
        public void StepWithMultilineArg_ArgsAndHeaderTable()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
Scenario: Step with Multiline Arg, Args and Header Table
""
Multiline Arg
""
Step ""Arg""
[X|Y]
|1|0|
|0|0|
""Arg"" Step ""Arg""
Step ""Arg"" Step
");        
            When__is_renamed_to("Step", "new Step");        
            The_Feature_should_contain(
@"
Scenario: Step with Multiline Arg, Args and Header Table
""
Multiline Arg
""
new Step ""Arg""
[X|Y]
|1|0|
|0|0|
""Arg"" new Step ""Arg""
Step ""Arg"" Step
");
        }
        
        [Test]
        public void StepInScenarioOutline()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
Scenario Ouline: Step in Scenario Outline
Step ""X""
""Y"" Step
Step ""X"" + ""Y""
Examples:
|X|Y|
|1|0|
|0|0|
");        
            When__is_renamed_to("Step", "new Step");        
            The_Feature_should_contain(
@"
Scenario Ouline: Step in Scenario Outline
new Step ""X""
""Y"" new Step
Step ""X"" + ""Y""
Examples:
|X|Y|
|1|0|
|0|0|
");
        }

    }
}

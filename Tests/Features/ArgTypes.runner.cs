using MbUnit.Framework;
using Features.StepDefinitions;

namespace Features 
{
    [TestFixture]
    public partial class ArgTypes 
    {
        public FeatureRunner FeatureRunner = new FeatureRunner();
        public HighlightFeature HighlightFeature = new HighlightFeature();

        
        [Test]
        public void SimpleArgs()
        {         
            FeatureRunner.Given_the_Feature_is(
@"
Feature: Arg Types
Scenario: Use string instead of int
Given the zipcode is ""33131""
");        
            FeatureRunner.The_Runner_should_contain(
@"
Given_the_zipcode_is(""33131"");
");
        }
        
        [Test]
        public void Tables()
        {         
            FeatureRunner.Given_the_Feature_is(
@"
Feature: Arg Types
Scenario: Use string instead of int in table
Given the Board
|0| | |
| |X| |
| | |X|
");        
            FeatureRunner.The_Runner_should_contain(
@"
Given_the_Board
(
new[] {""0"", """", """"},
new[] {"""", ""X"", """"},
new[] {"""", """", ""X""}
);
");
        }
        
        [Test]        
        [Category("wip")]
        public void TablesWithHeader()
        {         
            FeatureRunner.Given_the_Feature_is(
@"
Feature: Arg Types
Scenario: Tables with Header
Given the Addresses:
[ state | zip  ]
|FL	    |33131 |
|NY	    |10001 |
");        
            FeatureRunner.The_Runner_should_contain(
@"
Given_the_Addresses_(""FL"", ""33131"");
Given_the_Addresses_(""NY"", ""10001"");
");
        }
        
        [Test]        
        [Category("wip")]
        public void ScenarioOutlines()
        { 
        }
        
        [Test]
        public void TablesWithArgs()
        { 
        }
        
        [Test]
        public void TablesWithHeaderAndArgs()
        { 
        }

    }
}

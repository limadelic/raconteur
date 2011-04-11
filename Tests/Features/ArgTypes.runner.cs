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
        public void MultilineArgs()
        {         
            FeatureRunner.Given_the_Feature_is(
@"
Feature: Arg Types
Scenario: multiline arg
Given the zipcode is
""
33131
33133
""
");        
            FeatureRunner.The_Runner_should_contain(
@"
Given_the_zipcode_is(
@""
33131
33133
"");
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
        public void TablesWithArgs()
        {         
            FeatureRunner.Given_the_Feature_is(
@"
Feature: Arg Types
Scenario: Use string instead of int in table
Given ""0"" is next on the Board
|0| | |
| |X| |
| | |X|
");        
            FeatureRunner.The_Runner_should_contain(
@"
Given__is_next_on_the_Board
(
""0"",
new[] {""0"", """", """"},
new[] {"""", ""X"", """"},
new[] {"""", """", ""X""}
);
");
        }
        
        [Test]
        public void TablesWithHeaderAndArgs()
        {         
            FeatureRunner.Given_the_Feature_is(
@"
Feature: Arg Types
Scenario: Tables with Header
Given the ""US"" Addresses:
[ state | zip  ]
|FL	    |33131 |
|NY	    |10001 |
");        
            FeatureRunner.The_Runner_should_contain(
@"
Given_the__Addresses_(""US"", ""FL"", ""33131"");
Given_the__Addresses_(""US"", ""NY"", ""10001"");
");
        }
        
        [Test]
        public void ScenarioOutlines()
        {         
            FeatureRunner.Given_the_Feature_is(
@"
Feature: Arg Types
Scenario: Interest Rate
Given ""account"" has ""amount""
When interest is calculated
It should be ""interest""
Examples:
|account|amount|interest|
|23     |42    |1       |
|56     |23    |3       |
");        
            FeatureRunner.The_Runner_should_contain(
@"
[TestMethod]
public void InterestRate_1()
{
Given__has(""23"", 42);
When_interest_is_calculated();
It_should_be(1);
}
[TestMethod]
public void InterestRate_2()
{
Given__has(""56"", 23);
When_interest_is_calculated();
It_should_be(3);
}
");
        }

    }
}

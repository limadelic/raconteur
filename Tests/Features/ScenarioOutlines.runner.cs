using MbUnit.Framework;
using Features.StepDefinitions;

namespace Features 
{
    [TestFixture]
    public partial class ScenarioOutlines 
    {
        public FeatureRunner FeatureRunner = new FeatureRunner();
        public HighlightFeature HighlightFeature = new HighlightFeature();

        
        [Test]
        public void UsingScenarioOutlines()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
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
Given__has(23, 42);
When_interest_is_calculated();
It_should_be(1);
}
[TestMethod]
public void InterestRate_2()
{
Given__has(56, 23);
When_interest_is_calculated();
It_should_be(3);
}
");
        }
        
        [Test]
        public void OutlinesValuesInsideMultilineArgs()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
Scenario: Outline
Given
""
""""values"""" in """"arg""""
""
Examples:
|values|arg|
|42    |1  |
|23    |3  |
");        
            FeatureRunner.The_Runner_should_contain(
@"
[TestMethod]
public void Outline_1()
{
Given(
@""
42 in 1
"");
}
[TestMethod]
public void Outline_2()
{
Given(
@""
23 in 3
"");
}
");
        }
        
        [Test]
        public void ScenarioOutlinesWithMultipleExamples()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
Scenario: Interest Rate
Given ""account"" has ""amount""
When interest is calculated
It should be ""interest""
Examples: Poor Man
|account|amount|interest|
|23     |42    |0.01    |
Examples: Rich Man
|account|amount  |interest|
|007    |10000000|5       |
|007    |50000000|10      |
");        
            FeatureRunner.The_Runner_should_contain(
@"
[TestMethod]
public void InterestRate_PoorMan1()
{
Given__has(23, 42);
When_interest_is_calculated();
It_should_be(0.01);
}
[TestMethod]
public void InterestRate_RichMan1()
{
Given__has(007, 10000000);
When_interest_is_calculated();
It_should_be(5);
}
[TestMethod]
public void InterestRate_RichMan2()
{
Given__has(007, 50000000);
When_interest_is_calculated();
It_should_be(10);
}
");
        }

    }
}

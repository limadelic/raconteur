using Microsoft.VisualStudio.TestTools.UnitTesting;
using Features.StepDefinitions;

namespace Features 
{
    [TestClass]
    public partial class ScenarioOutlines 
    {
        public FeatureRunner FeatureRunner = new FeatureRunner();
        public HighlightFeature HighlightFeature = new HighlightFeature();

        
        [TestMethod]
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
public void InterestRate1()
{
Given__has(23, 42);
When_interest_is_calculated();
It_should_be(1);
}
[TestMethod]
public void InterestRate2()
{
Given__has(56, 23);
When_interest_is_calculated();
It_should_be(3);
}
");
        }
        
        [TestMethod]
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
public void Outline1()
{
Given(
@""
42 in 1
"");
}
[TestMethod]
public void Outline2()
{
Given(
@""
23 in 3
"");
}
");
        }

    }
}

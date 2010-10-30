using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features 
{
    [TestClass]
    public partial class ScenarioOutlines 
    {
        
        [TestMethod]
        public void UsingScenarioOutlines()
        {         
            Given_the_Feature_contains(
@"Scenario: Interest Rate
Given ""account"" has ""amount""
When interest is calculated
It should be ""interest""

Examples:
|account|amount|interest|
|23     |42    |1       |
|56     |23    |3       |
");        
            The_Runner_should_contain(
@"[TestMethod]
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

    }
}

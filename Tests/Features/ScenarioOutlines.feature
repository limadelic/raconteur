Feature: Scenario Outlines
	In order to use a Scenario as a template
	I want to have Scenario Outlines

Scenario: Using Scenario Outlines
	
	Given the Feature contains
	"
		Scenario: Interest Rate
			Given ""account"" has ""amount""
			When interest is calculated
			It should be ""interest""

			Examples:
			|account|amount|interest|
			|23     |42    |1       |
			|56     |23    |3       |    
	"

	The Runner should contain 
	"
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
	"

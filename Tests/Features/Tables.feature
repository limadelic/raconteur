Feature: Tables
	In order to write tabular style tests
	I want to be able to pass a table into a step

Scenario: Using Tables

	Given the Feature contains
	"
		Scenario: Scenario Name
			Given some values:
			|0|0|
			|0|1|
	"

	The Runner should contain
	"
		[TestMethod]
		public void ScenarioName()
		{
			Given_some_values_
			(
				new[] {0, 0},
				new[] {0, 1}
			);
		}
	"

Scenario: Tables with Header

	Given the Feature contains
	"
		Scenario: Scenario Name
			Verify some values:
			[X|Y]
			|0|0|
			|0|1|
	"

	The Runner should contain
	"
		[TestMethod]
		public void ScenarioName()
		{
			Verify_some_values_(0, 0);
			Verify_some_values_(0, 1);
		}
	"

Scenario: Tables with Args

	Given the Feature contains
	"
		Scenario: Scenario Name
			Given stuff in ""X"" place
			[ stuff ]
			|one    |
			|another|
			
			""Y"" stuff in
			|somewhere|
			|else	  |
	"

	The Runner should contain
	"
		[TestMethod]
		public void ScenarioName()
		{
			Given_stuff_in__place(""X"", ""one"");
			Given_stuff_in__place(""X"", ""another"");	
			stuff_in
			(
				new[] {""Y"", ""somewhere"", ""else""}
			);
		}
	"

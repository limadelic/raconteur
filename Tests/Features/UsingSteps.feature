Feature: Reusing Steps
	In order to speed up the production of test 
		while making them easier to maintain
	I want to be able to reuse steps across features

Scenario: Using steps from a Step library

	Given the Feature contains
	"
		using Step Library

		Scenario: Reuse a Step from the lib
			Step from Lib
	"

	The Runner should contain 
	"
		public StepLibrary StepLibrary = new StepLibrary();
		
		[TestMethod]
		public void ReuseAStepFromTheLib()
		{
			StepLibrary.Step_from_Lib();
		}
	"

@ignore
Scenario: Using steps from another Feature

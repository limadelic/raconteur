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

Scenario: Using steps from multiple Step libraries

	Given the Feature contains
	"
		using Step Library
		using another Step Library

		Scenario: Reuse Steps from libs
			Step from Lib
			Step from another Lib
	"

	The Runner should contain 
	"
		public StepLibrary StepLibrary = new StepLibrary();
		public AnotherStepLibrary AnotherStepLibrary = new AnotherStepLibrary();
		
		[TestMethod]
		public void ReuseStepsFromLibs()
		{
			StepLibrary.Step_from_Lib();
			AnotherStepLibrary.Step_from_another_Lib();
		}
	"

Feature: Reusing Steps
	In order to speed up the production of test 
		while making them easier to maintain
	I want to be able to reuse steps across features

Scenario: Reusing Step Definitions

	Given the Feature contains
	"
		using Step Definitions

		Scenario: Reuse a Step
			Step from Step Definitions
	"

	The Runner should contain 
	"
		public StepDefinitions StepDefinitions = new StepDefinitions();
		
		[TestMethod]
		public void ReuseAStep()
		{
			StepDefinitions.Step_from_Step_Definitions();
		}
	"

Scenario: Reusing steps from multiple Step Definitions

	Given the Feature contains
	"
		using Step Definitions
		using another Step Definitions

		Scenario: Reuse Steps from multiple Definitions
			Step from Step Definitions
			Step from another Step Definitions
	"

	The Runner should contain 
	"
		public StepDefinitions StepDefinitions = new StepDefinitions();
		public AnotherStepDefinitions AnotherStepDefinitions = new AnotherStepDefinitions();
		
		[TestMethod]
		public void ReuseStepsFromMultipleDefinitions()
		{
			StepDefinitions.Step_from_Step_Definitions();
			AnotherStepDefinitions.Step_from_another_Step_Definitions();
		}
	"
Scenario: Reusing Global Step Definitions
Scenario: Reusing Step Definitions from Libraries

	Given the setting "Libraries" contain "Common" 
	Given the Feature contains
	"
		using Step Definitions in Library

		Scenario: Reuse Steps from Library
			Step from Step Definitions in Library
	"

	The Runner should contain 
	"
		public StepDefinitionsInLibrary StepDefinitionsInLibrary = new StepDefinitionsInLibrary();
		
		[TestMethod]
		public void ReuseStepsFromLibrary()
		{
			StepDefinitionsInLibrary.Step_from_Step_Definitions_in_Library();
		}
	"

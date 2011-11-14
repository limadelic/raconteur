Feature: Reusing Steps
	In order to speed up the production of test 
		while making them easier to maintain
	I want to be able to reuse steps across features

Scenario: Default Step Definition
	
	Given the Feature is
	"
		Feature: Reusing Steps

		Scenario: Reuse a Step
			Step
	"

	The Runner should contain 
	"
		[TestMethod]
		public void ReuseAStep()
		{
			Step();
		}
	"

Scenario: private and protected Step in Default Step Definition
	
	Given the Feature is
	"
		Feature: Reusing Steps

		Scenario: Reuse a Step
			Step ""123""
			Inherited Step ""123""
	"

	The Runner should contain 
	"
		[TestMethod]
		public void ReuseAStep()
		{
			Step(""123"");
			Inherited_Step(""123"");
		}
	"

Scenario: Inherited Step in Default Step Definition
	
	Given the Feature is
	"
		Feature: Reusing Steps

		Scenario: Reuse a Step
			Inherited Step
	"

	The Runner should contain 
	"
		[TestMethod]
		public void ReuseAStep()
		{
			Inherited_Step();
		}
	"

Scenario: Reusing Step Definitions

	Given the setting "Libraries" contains "Common" 
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

Scenario: Reusing inherited Step in Step Definitions

	Given the setting "Libraries" contains "Common" 
	Given the Feature contains
	"
		using Step Definitions

		Scenario: Reuse a Step
			Base Step
	"

	The Runner should contain 
	"
		public StepDefinitions StepDefinitions = new StepDefinitions();
		
		[TestMethod]
		public void ReuseAStep()
		{
			StepDefinitions.Base_Step();
		}
	"

Scenario: Reusing steps from multiple Step Definitions

	Given the setting "Libraries" contains "Common" 
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

	Given the setting "Libraries" contains "Common" 
	Given the setting "StepDefinitions" contains "StepDefinitions" 
	Given the Feature contains
	"
		Scenario: Reuse a Step in global Step Definition
			Step from Step Definitions
	"

	The Runner should contain 
	"
		public StepDefinitions StepDefinitions = new StepDefinitions();
		
		[TestMethod]
		public void ReuseAStepInGlobalStepDefinition()
		{
			StepDefinitions.Step_from_Step_Definitions();
		}
	"

Scenario: Reusing Step Definitions from Libraries

	Given the setting "Libraries" contains "Common" 
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

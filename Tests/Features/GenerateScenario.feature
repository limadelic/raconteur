Feature: Generate Scenario
	In order to run a Scenario
	Raconteur should generate a TestMethod per Scenario and call each Step

Scenario: Generate Test Methods 

	Given the Feature contains
	"
		Scenario: Scenario Name
			If something happens
			Then something else should happen
			If something happens
			And another thing too
	"

	The Runner should contain
	"
		[TestMethod]
		public void ScenarioName() 
		{
			If_something_happens();
			Then_something_else_should_happen();
			If_something_happens();
			And_another_thing_too();
		}
	"

Feature: Generate Scenario
	In order to run a Scenario
	Raconteur should generate the Scenario defintion

Scenario: Generate Test Methods
	When the Scenario for a feature is generated
	Then it should be a Test Method
	And it should be named After the Scenario name

Scenario: Generate Step Calls
	When a Scenario with steps is generated
	it should call each step in order

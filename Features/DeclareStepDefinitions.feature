Feature: Declare Step Definitions
	In order simplify the implemententation a Feature
	Raconteur should declare the StepDefinitions

Scenario: Create Step Definitions file  
	When a Feature is declared for the first time
	The StepDefinitions file should be created
	and should have a method per Step

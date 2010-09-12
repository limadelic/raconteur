Feature: Generate Step Definitions file
	In order simplify the implemententation a Feature
	Raconteur should declare the StepDefinitions

Scenario: Create Step Definitions file  
	When a Feature is declared for the first time
	The StepDefinitions file should be created

Scenario: Update Feature file
	When the Feature file is updated
	The StepDefinitions file should not be recreated

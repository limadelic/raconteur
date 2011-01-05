Feature: Make Scenarios Collapsibles
	In order to allow a high level view of the scenarios
	Raconteur should allow to collapse the scenarios

Scenario: Scenarios are collapsible

	Given the Feature is
	"
		Feature: Name

		Scenario: First
			Steps

		Scenario: Second
			Steps
	"

	Raconteur should allow to collapse
	"
		Scenario: First
			Steps
	"

	Raconteur should allow to collapse
	"
		Scenario: Second
			Steps
	"

Scenario: Single line Scenario

	Given the Feature contains "Scenario: First"
	Raconteur should allow to collapse "Scenario: First"

Scenario: Feature with single line Scenario

	Given the Feature contains 	
	"
		Feature: Name 

		Scenario: Single
	"
	Raconteur should allow to collapse "Scenario: Single"

Scenario: Multiple single line Scenarios

	Given the Feature contains
	"
		Scenario: First
		Scenario: Second
		Scenario: Third
	"

	Raconteur should allow to collapse "Scenario: First"
	Raconteur should allow to collapse "Scenario: Second"
	Raconteur should allow to collapse "Scenario: Third"
Scenario: Scenarios with Tags

	Given the Feature is
	"
		Feature: Name

		Scenario: First
			Steps
		
		@tag
		Scenario: Second
			Steps
	"

	Raconteur should allow to collapse
	"
		Scenario: First
			Steps
	"

	Raconteur should allow to collapse
	"
		Scenario: Second
			Steps
	"

Scenario: Scenarios with multiple Tags

	Given the Feature contains
	"
		@tag
		Scenario: First
		@tag
		@another_tag
		Scenario: Second
	"

	Raconteur should allow to collapse "Scenario: First"

	Raconteur should not allow to collapse
	"
		Scenario: First
		@tag
	"

	
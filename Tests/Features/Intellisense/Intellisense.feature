Feature: Intellisense
	In order to write features quicker
	I want Raconteur to complete sentences for me

Scenario Outline: Completion
	Given the Feature contains
	"
		Scenario: Scenario Name
			I do something
			I do another thing
			If something happens
			Then this should happen
	"
	When I begin to type "fragment" on the next line
	Then "suggestion" should be displayed

	Examples:
	| fragment | suggestion            |
	| Sc       | Scenario:             |
	| Sc       | Scenario Outline:     |
	| Ex       | Examples:             |
	| Fe       | Feature:              |
	| I        | I do something        |
	| I        | I do another thing    |
	| I        | If something happens  |

Scenario Outline: Ignore non-steps
	Given the Feature contains
	"
		Feature: Feature Name
			Description
		Scenario: Scenario Name
			I do something
			I do another thing
			If something happens
			Then this should happen
			""
				I am an arg
			""
	"
	When I begin to type "fragment" on the next line
	Then "suggestion" should not be displayed

	Examples:
	| fragment | suggestion              |
	| De       | Description             |
	| Sc       | Scenario: Scenario Name |
	| Fe       | Feature: Feature Name   |
	| I        | I am an arg             |

@wip
Scenario: Suggestions from Base Class
	Given the Feature contains "Feature: Intellisense"
	When I begin to type "In" on the next line
	Then "Inherited Step" should be displayed
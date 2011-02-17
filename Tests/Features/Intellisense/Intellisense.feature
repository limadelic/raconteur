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
	| fragment | suggestion           |
	| Sc       | Scenario             |
	| Sc       | Scenario Outline     |
	| Ex       | Examples             |
	| Fe       | Feature              |
	| I        | I do something       |
	| I        | I do another thing   |
	| I        | If something happens |

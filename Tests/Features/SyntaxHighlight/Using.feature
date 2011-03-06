Feature: Highlight Using
	In order to enhance the UX and reduce typing errors
	Raconteur should highlight the using statements

Scenario: Feature and Scenarios

	Given the Feature is
	"
		Feature: Name

		using Step Definitions

		Scenario: First

		using after Scenario
	"

	Raconteur should highlight like a "Comment" "using Step Definitions"
	Raconteur_should_not_highlight "using after Scenario"
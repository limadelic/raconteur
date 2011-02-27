Feature: Highlight Using
	In order to enhance the UX and reduce typing errors
	Raconteur should highlight the using statements

Scenario: Feature and Scenarios

	Given the Feature is
	"
		Feature: Name

		using Step Library

		Scenario: First
	"

	Raconteur should highlight like a "Comment" "using Step Library"
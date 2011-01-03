Feature: Highlight Tags
	In order to enhance the UX
	Raconteur should highlight the Tags 

Scenario: Highlight all Tags

	Given the Feature contains
	"
		@tag @tag
		@tag
		Scenario: Name
	"

	Raconteur should highlight "3" "@tag" like a "Comment"

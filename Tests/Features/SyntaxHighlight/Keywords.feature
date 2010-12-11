Feature: Highlight Keywords
	In order to enhance the UX and reduce typing errors
	Raconteur should highlight it's keywords

Scenario: Feature and Scenarios

	Given the Feature is
	"
		Feature: Name

		Scenario: First

		Scenario: Second
	"

	Raconteur should highlight
	[ Count | Text      | Color   ]
	|     1 | Feature:  | Keyword |
	|     2 | Scenario: | Keyword |



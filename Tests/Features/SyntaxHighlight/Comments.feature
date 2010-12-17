Feature: Highlight Comments
	In order to enhance the UX
	Raconteur should highlight the comments

Scenario: Single line Comments

	Given the Feature is
	"
		Feature: Name

		Scenario: First
		// One Comment
		One Step
		// Another Comment
	"

	Raconteur should highlight
	[ Count | Text					| Color   ]
	|     1 | // One Comment		| Comment |
	|     1 | // Another Comment	| Comment |



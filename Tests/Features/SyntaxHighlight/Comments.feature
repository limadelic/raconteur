Feature: Highlight Comments
	In order to enhance the UX
	Raconteur should highlight the comments

Scenario: Single line Comments

	Given the Feature is
	"
		Feature: Name

		Scenario: First
		// Comment
		Step
		// Scenario:
	"

	Raconteur should highlight
	[ Count | Text			| Style   ]
	|     1 | // Comment	| Comment |
	|     1 | // Scenario:	| Comment |
	|     1 | Scenario:		| Keyword |

Scenario: Multi-line Comments

	Given the Feature is
	"
		Feature: Name

		Scenario: First
		/* 
		Scenario: Commented
		*/
	"

	Raconteur should highlight
	"
		/* 
		Scenario: Commented
		*/
	"
	with "Comment" style

	Raconteur should highlight
	[ Count | Text			| Color   ]
	|     1 | Scenario:		| Keyword |


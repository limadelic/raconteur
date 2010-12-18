Feature: Highlight Comments
	In order to enhance the UX
	Raconteur should highlight the comments

Scenario: Single line Comments

	Given the Feature contains
	"
		Scenario: Name
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

	Given the Feature contains
	"
		Scenario: Name
		/* 
		Scenario: Commented
		*/
	"

	Raconteur should highlight like a "Comment"
	"
		/* 
		Scenario: Commented
		*/
	"

	Raconteur should highlight
	[ Count | Text			| Style   ]
	|     1 | Scenario:		| Keyword |


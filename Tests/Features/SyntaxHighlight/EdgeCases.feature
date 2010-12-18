Feature: Test Higlighting Edge Cases
	In order to enhance the UX
	Raconteur should highlight the feature

Scenario: Keywords and Comments in Multiline Arg display like Arg

	Given the Feature contains
	"
		""
			Scenario: Name
			// Single Line Comment
			/*
				MultiLine Comment
			*/
		""
	"

	Raconteur should highlight like a "String"
	"
		""
			Scenario: Name
			// Single Line Comment
			/*
				MultiLine Comment
			*/
		""
	"

	Raconteur should not highlight
	[ Text					 ]
	| Scenario:				 |
	| // Single Line Comment |

	Raconteur should not highlight
	"
		/*
			MultiLine Comment
		*/
	"



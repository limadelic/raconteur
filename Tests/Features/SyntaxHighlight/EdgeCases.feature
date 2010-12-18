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
Scenario: Keywords and Args inside Multiline Comments display like a Comment

	Given the Feature contains
	"
		/*
			Scenario: Name

				Step ""Arg""
				""
					MultiLine Arg
				""
		*/
	"

	Raconteur should highlight like a "Comment"
	"
		/*
			Scenario: Name

				Step ""Arg""
				""
					MultiLine Arg
				""
		*/
	"

	Raconteur should not highlight
	[ Text		]
	| Scenario:	|
	| \"Args\"	|

	Raconteur should not highlight
	"
		""
			MultiLine Arg
		""
	"

Feature: Test Higlighting Edge Cases
	In order to enhance the UX
	Raconteur should highlight the feature

Scenario: Keywords, Comments & Tables in Multiline Arg display like Arg

	Given the Feature contains
	"
		""
			Scenario: Name
			// Single Line Comment
			/*
				MultiLine Comment
			*/
			[ Table			  ]
			|  value in table |
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
			[ Table			  ]
			|  value in table |
		""
	"

	Raconteur should not highlight
	[ Text					 ]
	| Scenario:				 |
	| // Single Line Comment |
	| value in table		 |

	Raconteur should not highlight
	"
		/*
			MultiLine Comment
		*/
	"
Scenario: Keywords, Args & Tables inside Multiline Comments display like a Comment

	Given the Feature contains
	"
		/*
			Scenario: Name

				Step ""Arg""
				""
					MultiLine Arg
				""

			[ Table			  ]
			|  value in table |
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

			[ Table			  ]
			|  value in table |
		*/
	"

	Raconteur should not highlight
	[ Text			 ]
	| Scenario:		 |
	| \"Args\"		 |
	| value in table |

	Raconteur should not highlight
	"
		""
			MultiLine Arg
		""
	"

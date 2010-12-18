Feature: Highlight Args
	In order to enhance the UX
	Raconteur should highlight the Args 

Scenario: Single line Arg

	Given the Feature contains
	"
		Scenario: Name
			
			Step with ""Arg1"" and ""Arg2""
			// ""Commented Arg""
	"

	Raconteur should highlight like a "String"
	[ Count | Text				]
	|     1 | \"Arg1\"			|
	|     1 | \"Arg2\"			|
	|     0 | \"Commented Arg\" |

Scenario: Multiline Arg

	Given the Feature contains
	"
		Scenario: Name
			
			Step with
			""
				Multiline Arg
			""
	"

	Raconteur should highlight like a "String"
	"
		""
			Multiline Arg
		""
	"

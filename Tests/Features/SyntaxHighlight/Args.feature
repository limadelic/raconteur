Feature: Highlight Args
	In order to enhance the UX
	Raconteur should highlight the Args 

Scenario: Single line Arg

	Given the Feature is
	"
		Feature: Name

		Scenario: First
			
			Step with ""Arg1"" and ""Arg2""
			// Commented ""Arg""
	"

	Raconteur should highlight like a "String"
	[ Count | Text	   ]
	|     1 | \"Arg1\" |
	|     1 | \"Arg2\" |
	|     0 | \"Arg\"  |

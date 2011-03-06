Feature: Highlight Args
	In order to enhance the UX
	Raconteur should highlight the Args 

using Highlight Feature

Scenario: Single line Arg

	Given the Feature is
	"
		Scenario: Name
			
			Step with ""Arg1"" and ""Arg2""
	"

	Raconteur should highlight like a "String"
	[ Count | Text		]
	|     1 | \"Arg1\"	|
	|     1 | \"Arg2\"	|

Scenario: Multiline Arg

	Given the Feature is
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
	
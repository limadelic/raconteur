Feature: Step Arguments
	In order to reuse steps
	I want to paramerize them

Scenario: Generate Arguments

	Given the Feature contains
	"
		Scenario: Scenario Name
			If ""X"" happens
	"
	The Runner should contain
	"
		If__happens(""X"");
	"

Scenario: Type Inference

	Given the Feature contains
	"
		Scenario: Scenario Name
			If the balance is ""42""
	"
	The Runner should contain
	"
		If_the_balance_is(42);
	"

Scenario: Multiline Arg

	Given the Feature contains
	"
		Scenario: Multiline Arg
			Step Arg with multiple lines 
			""
				could start on one line
				and finish on another
			""
	"

	The Runner should contain
	"
		Step_Arg_with_multiple_lines(
		@""
			could start on one line
			and finish on another
		"");
	"

Scenario: Step starting with Arg

	Given the Feature contains
	"
		Scenario: Name
			
			""
				Multiline Arg
			""
			before a Step
	"

	The Runner should contain
	"
		before_a_Step(
		@""
			Multiline Arg
		"");
	"

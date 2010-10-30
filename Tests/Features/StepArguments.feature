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
	When an Arg is not finish in a Sentence
	It should expand until it's closed in another line

Feature: Rename Step
	In order to make changes easier
	it should be possible to rename a step 
	having the changes propagated accordingly

Scenario: Rename within feature

	Given the Feature contains
	"
		Scenario: Name
			Step
			Step
	"

	When "Step" is renamed to "new Step"

	The Feature should contain
	"
		Scenario: Name
			new Step
			new Step
	"

Scenario: Step with Args

	Given the Feature contains
	"
		Scenario: Name
			simple Step
			Step ""with Arg""
	"

	When "Step" is renamed to "new Step"

	The Feature should contain
	"
		Scenario: Name
			simple Step
			new Step ""with Arg""
	"

Scenario: Step with Multiline

	Given the Feature contains
	"
		Scenario: With Multiline Arg in Step
			Step 
			""
				Multiline Arg
			""
	"

	When "Step" is renamed to "new Step"

	The Feature should contain
	"
		Scenario: With Multiline Arg in Step
			new Step
			""
				Multiline Arg
			""
	"	
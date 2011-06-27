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

@wip
Scenario: Step with Args

	Given the Feature contains
	"
		Scenario: Name
			Step
			Step ""with arg""
	"

	When "Step " is renamed to "new Step"

	The Feature should contain
	"
		Scenario: Name
			Step
			new Step ""with Arg""
	"

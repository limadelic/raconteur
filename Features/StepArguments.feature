Feature: Step Arguments
	In order to reuse steps
	I want to paramerize them

Scenario: Generate Arguments
	When a step contains arguments
	The runner should pass them in the call

Scenario: Type Inference
	When an argument is an integer
	It should be passed as a number

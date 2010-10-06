Feature: Refactor Feature
	In order to easily change my feature definitions
	I want to automatically refactor the feature

Scenario: Rename Feature
	Given I have already defined a feature
	If I rename it
	Then the steps and the runner should reflect the change

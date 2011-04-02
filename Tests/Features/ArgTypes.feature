Feature: Arg Types
	In order to allow specifiying Args types
	Raconteur should use the type declared in the Step implementation
	(otherwise try guessing the type)

@wip
Scenario: Simple Args
	
	Given the Feature is
	"
		Feature: Arg Types

		Scenario: Use string instead of int
			Given the zipcode is ""33131""
	"
	The Runner should contain 
	"
		Given_the_zipcode_is(""33131"");
	"

Scenario: Tables
Scenario: Tables with Header
Scenario: Scenario Outlines
Scenario: Tables with Args
Scenario: Tables with Header and Args

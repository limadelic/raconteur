Feature: Arg Types
	In order to allow specifiying Args types
	Raconteur should use the type declared in the Step implementation
	(otherwise try guessing the type)

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

	Given the Feature is
	"
		Feature: Arg Types

		Scenario: Use string instead of int in table
			Given the Board
			|0| | |
			| |X| |
			| | |X|
	"
	The Runner should contain 
	"
		Given_the_Board
		(
			new[] {""0"", """", """"},
			new[] {"""", ""X"", """"},
			new[] {"""", """", ""X""}
		);
	"

Scenario: Tables with Header

	Given the Feature is
	"
		Feature: Arg Types

		Scenario: Tables with Header
			Given the Addresses:
			[ state | zip  ]
			|FL	    |33131 |
			|NY	    |10001 |
	"

	The Runner should contain 
	"
		Given_the_Addresses_(""FL"", ""33131"");
		Given_the_Addresses_(""NY"", ""10001"");
	"

@wip
Scenario: Scenario Outlines
Scenario: Tables with Args
Scenario: Tables with Header and Args

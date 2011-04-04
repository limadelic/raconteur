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

Scenario: Multiline Args
	
	Given the Feature is
	"
		Feature: Arg Types

		Scenario: multiline arg
			Given the zipcode is 
			""
				33131
				33133
			""
	"
	The Runner should contain 
	"
		Given_the_zipcode_is(
		@""
			33131
			33133
		"");
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

Scenario: Tables with Args

	Given the Feature is
	"
		Feature: Arg Types

		Scenario: Use string instead of int in table
			Given ""0"" is next on the Board
			|0| | |
			| |X| |
			| | |X|
	"
	The Runner should contain 
	"
		Given__is_next_on_the_Board
		(
			""0"",
			new[] {""0"", """", """"},
			new[] {"""", ""X"", """"},
			new[] {"""", """", ""X""}
		);
	"
Scenario: Tables with Header and Args

	Given the Feature is
	"
		Feature: Arg Types

		Scenario: Tables with Header
			Given the ""US"" Addresses:
			[ state | zip  ]
			|FL	    |33131 |
			|NY	    |10001 |
	"
	The Runner should contain 
	"
		Given_the__Addresses_(""US"", ""FL"", ""33131"");
		Given_the__Addresses_(""US"", ""NY"", ""10001"");
	"

@wip
Scenario: Scenario Outlines
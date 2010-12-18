Feature: Table Values
	In order to enhance the UX
	Raconteur should highlight values inside Tables

Scenario: Table Values are displayed like Strings

	Given the Feature contains
	"
		Scenario: Name
			
			Step with Table
			[ X | Y ]
			| a | b |
			| c | d |
	"

	Raconteur should highlight like a "String"
	[ Text ]
	|   a  |
	|   b  |
	|   c  |
	|   d  |

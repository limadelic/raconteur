Feature: Mine sweeper
	
Scenario: Step on a mine die

	Given the board
	|*| |
	| | |
	When I click on "0", "0"
	I should lose

Scenario: When u die u see the board

	Given the board
	|*| |
	| | |
	When I click on "0", "0"
	I should see
	|*|1|
	|1|1|

Scenario: When you die you see the board

	Given the board
	|*|*|
	|*| |
	When I click on "0", "0"
	I should see
	|*|*|
	|*|3|

Scenario: Got lucky n' won

	Given the board
	| |*|
	|*|*|
	When I click on "0", "0"
	I should win
	and I should see
	|3|*|
	|*|*|

Scenario: Step on clear spot with 2 adjacent bombs

	Given the board
	| |*|
	| |*|
	When I click on "0", "0"
	I should be alive
	and I should see
	|2| |
	| | |

Scenario: Clicking a free space opens up the entire board

	Given the board
	| | | |
	| | | |
	| | |*|
	When I click on "0", "0"
	I should win
	and I should see
	| | | |
	| |1|1|
	| |1|*|
	
Feature: Tic Tac Toe Rules

Scenario: X is first player
	When a game is started
	The current player should be "X"

Scenario: Game state by turn
	When a game is started
	And "X" plays in the "top" "left"
	The current player should be "O"
	And the board state should be
	|X| | |
	| | | |
	| | | |


Feature: Tic Tac Toe Rules

Scenario: X is first player
	Given a game is started
	The current player should be "X"

Scenario: Game state by turn
	Given a game is started
	When the "top" "left" square is played
	The current player should be "O"
	And the board state should be
	|X| | |
	| | | |
	| | | |
	When the "bottom" "right" square is played
	The board state should be
	|X| | |
	| | | |
	| | |O|

Scenario: O wins
	With the following board
	|X|X|O|
	|X|O| |
	| | | |
	When the "bottom" "left" square is played
	The winner should be "O"

Scenario: X wins
	With the following board
	|X|X|O|
	|X|X|O| 
	|O|O| |
	When the "bottom" "right" square is played
	The winner should be "X"

Scenario: Cat's game
	With the following board
	|X|O|X|
	|O|O|X|
	|X|X|O|
	It should be a cat's game

Scenario: horizontal win
	With the following board
	|X|X|X|
	|X|O|O| 
	|O|O|X|
	The winner should be "X"

Scenario: horizontal win in middle
	With the following board
	|O|X|O|
	|X|X|X| 
	|X|O|O|
	The winner should be "X"

Scenario: horizontal win in bottom
	With the following board
	|X|X| |
	|X|O|X| 
	|O|O|O|
	The winner should be "O"

Scenario: vertical win in middle
	With the following board
	|X|X|O|
	|O|X|O| 
	|O|X|X|
	The winner should be "X"

Scenario: vertical win in right
	With the following board
	|X|O|X|
	|O|O|X| 
	|O|X|X||
	The winner should be "X"

Scenario: vertical win in left
	With the following board
	|X|O|O|
	|X|O|X| 
	|X|X|O|
	The winner should be "X"

Scenario: diagonal win 
	With the following board
	|X|O|O|
	|X|O|X| 
	|O|X| |
	The winner should be "O"

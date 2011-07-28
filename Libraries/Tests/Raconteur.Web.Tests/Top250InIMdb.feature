Feature: Find 250 movies in IMdb
	In order to find good movies I haven't watched
	I need to look at the 250 vies in IMDb

Scenario: Find 250 movies

	Visit "http://www.imdb.com"	

	Click on "//a" with text "Top 250"

	Title should be "IMDb Top 250"

	Find link with
	[Text]
	|Pulp Fiction |
	|The Matrix	  |
	|Memento      |

Scenario: Scores of my favorite movies 

	Visit "http://www.imdb.com/chart/top"

	My favorite movies should be there
	[ Rank | Rating | Title        ]
	|  5   | 8.9    | Pulp Fiction | 
	| 22   | 8.7    | The Matrix   |
	| 31   | 8.6    | Memento      | 

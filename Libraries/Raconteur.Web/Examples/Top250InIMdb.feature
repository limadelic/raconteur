Feature: Find 250 movies in IMdb
	In order to find good movies I haven't watched
	I need to look at the 250 vies in IMDb

Scenario: Find 250 movies

	Use "Firefox"
	Visit "http://www.imdb.com"

	Click on "//a" with text "Top 250"

	Title should be "IMDb Top 250"

	Find link with
	[Text]
	|Pulp Fiction |
	|The Matrix	  |
	|Memento      |

	End


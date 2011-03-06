Feature: Highlight Tags
	In order to enhance the UX
	Raconteur should highlight the Tags 

Scenario: Highlight all Tags

	Given the Feature contains
	"
		@tag @tag
		@multiword tag
		Scenario: Name
	"

	Raconteur should highlight like a "Comment" 
	[ Count | Text           ]
	|     1 | @tag @tag		 |
	|     1 | @multiword tag |
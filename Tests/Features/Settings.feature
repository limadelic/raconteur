Feature: User Settings 
	In order to allow users to select different feature
	Raconteur should provide User Settings
	

Scenario: Setting the xUnit runner

	Given the settings
	"
		xUnit: MbUnit
		language: es
		using: Step Library	
		using: Another Step Library	
	"

	When the project is loaded

	The Settings should be:
	[ xUnit  | language ]
	| MbUnit | Spanish  |

	The Step Libraries should be:
	|StepLibrary|
	|AnotherStepLibrary|
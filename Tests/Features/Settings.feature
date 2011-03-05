Feature: User Settings 
	In order to allow users to select different feature
	Raconteur should provide User Settings
	

Scenario: Setting the xUnit runner

	Given the settings
	"
		xUnit: MbUnit
		language: es
		using: StepDefinitions
		using: Another Step Definitions
	"

	When the project is loaded

	The Settings should be:
	[ xUnit  | language ]
	| MbUnit | Spanish  |

	The Step Definitions should be:
	|StepDefinitions|
	|AnotherStepDefinitions|
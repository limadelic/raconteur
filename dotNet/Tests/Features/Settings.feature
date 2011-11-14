Feature: User Settings 
	In order to allow users to select different feature
	Raconteur should provide User Settings
	

Scenario: Default settings
	
	Given the settings ""
	When the project is loaded
	The Settings should be:
	[ xUnit  | language ]
	| MsTest | English  |

Scenario: All settings

	Given the settings
	"
		xUnit: MbUnit
		language: es
		lib: Library
		lib: another Library
		using: StepDefinitions
		using: Another Step Definitions
	"

	When the project is loaded

	The Settings should be:
	[ xUnit  | language ]
	| MbUnit | Spanish  |

	The Libraries should be:
	|Library|
	|another Library|
	
	The Step Definitions should be:
	|StepDefinitions|
	|AnotherStepDefinitions|
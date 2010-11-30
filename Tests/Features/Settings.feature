Feature: User Settings 
	In order to allow users to select different feature
	Raconteur should provide User Settings
	

Scenario: Setting the xUnit runner

	Given the settings
	"
		<configuration>
			<raconteur>
				<xUnit name=""MbUnit"" />
				<language code=""es"" />
			</raconteur>
		</configuration>	
	"

	When the project is loaded

	The Settings should be:
	[ xUnit  | language ]
	| MbUnit | Spanish  |

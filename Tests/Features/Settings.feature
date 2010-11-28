Feature: User Settings 
	In order to allow users to select different feature
	Raconteur should provide User Settings
	

Scenario: Setting the xUnit runner

	Given the settings
	"
		<configuration>
			<raconteur>
				<xUnit name=""MbUnit"" />
			</raconteur>
		</configuration>	
	"
	When the project is loaded
	The xUnit runner should be "MbUnit"
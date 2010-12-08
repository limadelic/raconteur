Feature: Comments
	In order to enhance communication/documentation
	Raconteur should allow comments in the Feature

Scenario: Single line Comments
	
	Given the Feature is
	"
		Feature: Comments

		Scenario: The Doors
		// When the doors of perception are cleansed, 
		// man will see things as they truly are 
		Infinite
	"

	The Runner should contain
	"
		public void TheDoors()
		{
			Infinite();
		}
	"

Scenario: Multiline Comments

Feature: Comments
	In order to enhance communication/documentation
	Raconteur should allow comments in the Feature

Scenario: Single line Comments
	
	Given the Feature contains
	"
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

	Given the Feature contains
	"
		Scenario: The Doors
		/*
			When the doors of perception are cleansed, 
			man will see things as they truly are 
		*/
			Infinite
	"

	The Runner should contain
	"
		public void TheDoors()
		{
			Infinite();
		}
	"

// this Scenarios are implemented differently
// 'cos cannot nest Multiline Args

Scenario: Comments inside Multiline Args
	When lines a line in a Multiline Args starts with a comment
	It will still be passed in as part of the Arg
	
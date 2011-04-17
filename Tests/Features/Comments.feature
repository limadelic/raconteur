Feature: Comments
	In order to enhance communication/documentation
	Raconteur should allow comments in the Feature

using Feature Runner

Scenario: Single line Comments
	
	//Given the Feature is the "thing"
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

Scenario: Comments inside Multiline Args

	Given the Feature contains
	"
		Scenario: The Doors
			
			When 
			"" 
			// the doors of perception are cleansed, 
			// man will see things as they truly are 
			""
			Infinite
	"

	The Runner should contain
	"
		When(
		@""
		// the doors of perception are cleansed,
		// man will see things as they truly are
		"");
		Infinite();
	"
	
Feature: Tags
	In order to allow scenario classifications
	Raconteur should allow to place Tags on Scenarios

Scenario: Single Tag

	Given the Feature contains
	"
		@tag
		Scenario: Tagged	
	"

	The Runner should contain
	"
		[TestMethod]
		[TestCategory(""tag"")]
		public void Tagged()
		{
		}
	"

Scenario: Multiple Tags

Scenario: Multiple Tags in on line

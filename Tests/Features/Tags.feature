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

	Given the Feature contains
	"
		@one tag
		@another tag
		Scenario: Tagged	
	"

	The Runner should contain
	"
		[TestCategory(""one tag"")]
		[TestCategory(""another tag"")]
	"

Scenario: Multiple Tags in one line

	Given the Feature contains
	"
		@one_tag @another_tag
		Scenario: Tagged	
	"

	The Runner should contain
	"
		[TestCategory(""one_tag"")]
		[TestCategory(""another_tag"")]
	"

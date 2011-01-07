Feature: Ignoring Scenarios
	In order to avoid running scenarios
	Raconteur should allow to use an @ignore Tag

Scenario: Ignore a Scenario

	Given the Feature contains
	"
		@ignore
		Scenario: Ignored
	"

	The Runner should contain
	"
		[TestMethod]
		[Ignore]
		public void Ignored()
		{
		}
	"

@ignore
Scenario: Ignore with a reason

	Given the Feature contains
	"
		@ignore just because
		Scenario: Ignored
	"

	The Runner should contain
	"
		[TestMethod]
		[Ignore] // just because
		public void Ignored()
		{
		}
	"
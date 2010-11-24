Feature: Using Multiple Runners
	In order to allow a dev to use her favorite xUnit
	Raconteur should generate runners for multiple frameworks

Scenario: MbUnit Runner

	Using "MbUnit"

	Given the Feature is
	"
		Feature: Feature Name
	"

	The Runner should be
	"
		using MbUnit.Framework;

		namespace Features
		{
			[TestFixture]
			public partial class FeatureName 
			{
			}
		}
	"
Feature: Generate Feature Runner It
	In order to run a Feature
	Raconteur should generate its Runner

Scenario: Generate Runner Class

	Given the Feature is
	"
		Feature: Feature Name
	"

	The Runner should be
	"
		using Microsoft.VisualStudio.TestTools.UnitTesting;

		namespace Features
		{
			[TestClass]
			public partial class FeatureName 
			{
			}
		}
	"
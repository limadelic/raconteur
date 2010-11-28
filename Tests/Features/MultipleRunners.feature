Feature: Using Multiple Runners
	In order to allow a dev to use her favorite xUnit
	Raconteur should generate runners for multiple frameworks

Scenario: MbUnit Runner

	Using "MbUnit"

	Given the Feature is
	"
		Feature: Feature

		Scenario: Scenario
	"

	The Runner should be
	"
		using MbUnit.Framework;

		namespace Features
		{
			[TestFixture]
			public partial class Feature
			{
				[Test]
				public void Scenario()
				{
				}
			}
		}
	"

Scenario: NUnit Runner

	Using "NUnit"

	Given the Feature is
	"
		Feature: Feature

		Scenario: Scenario
	"

	The Runner should be
	"
		using NUnit.Framework;

		namespace Features
		{
			[TestFixture]
			public partial class Feature
			{
				[Test]
				public void Scenario()
				{
				}
			}
		}
	"

Scenario: xUnit Runner

	Using "xUnit"

	Given the Feature is
	"
		Feature: Feature

		Scenario: Scenario
	"

	The Runner should be
	"
		using Xunit;

		namespace Features
		{
			public partial class Feature
			{
				[Fact]
				public void Scenario()
				{
				}
			}
		}
	"
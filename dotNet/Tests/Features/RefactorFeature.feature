Feature: Refactor Feature
	In order to easily change my feature definitions
	I want to automatically refactor the feature

Scenario: Rename Feature
	
	Given the Step Definition
	"
		public partial class FeatureName {}
	"
	
	When the Feature is renamed
	"
		Feature: Renamed Feature
	"

	Then the Step Definitions should be
	"
		public partial class RenamedFeature {}
	"

Scenario: Change default Namespace

	Given the Step Definition
	"
		namespace Features
		{
			public partial class FeatureName {}
		}
	"
	for the Feature
	"
		Feature: Feature Name
	"

	When the default namespace changes to "NewFeatures"

	Then the Step Definitions should be
	"
		namespace NewFeatures
		{
			public partial class FeatureName {}
		}
	"

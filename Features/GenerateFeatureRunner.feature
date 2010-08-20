Feature: Generate Feature Runner
	In order to run a Feature
	Raconteur should generate its Runner

Scenario: Generate Runner Class
	When the Runner for a Feature is generated
	Then it should be a TestClass
	And it should be named FeatureFileNameRunner
	And it should be on the Feature Namespace

Scenario: Generate Step Definition Reference
	When the Runner for a Feature is generated
	Then it should generate a class reference named FeatureName under StepDefinitions
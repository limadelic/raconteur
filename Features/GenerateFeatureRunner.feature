
Feature: Generate Feature Runner
	In order to run a Feature
	Raconteur should generate its Runner

Scenario: Generate Runner Class
	When the Runner for a Feature is generated
	Then it should be a TestClass
	And it should be named FeatureFileNameRunner
	And it should be on the Feature Namespace
Feature: Invalid Features
	In order to provide a stable UX
	Raconteur should handle invalid Features

Scenario: Empty Feature
	Given the Feature is Empty
	The Runner should be "Feature file is Empty"

Scenario: Unparseable Feature
	Given the Feature is "unparseable feature"
	The Runner should be "Missing Feature declaration"

Scenario: Unnamed Feature
	Given the Feature is "Feature:"
	The Runner should be "Missing Feature Name"



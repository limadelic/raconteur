Feature: Find Raconteur 
	In order to find Raconteur
	Needs to be googled

Scenario: Find Raconteur in Google

	Given I go to "http://google.com"
	When I look for "Raconteur"
	The page title should be "raconteur - Google Search"
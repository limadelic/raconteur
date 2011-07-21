Feature: Find Raconteur 
	In order to find Raconteur
	Needs to be googled

Scenario: Using Firefox

	Use "Firefox"
	Visit "http://google.com"
	Set "q" to "Raconteur"
	Title should be "raconteur - Google Search"
	End

Scenario: Using Chrome

	Use "Chrome"
	Visit "http://google.com"
	Set "q" to "Raconteur"
	Title should be "raconteur - Google Search"
	End
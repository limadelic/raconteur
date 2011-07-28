Feature: Find Raconteur 
	In order to find Raconteur
	Needs to be googled

using Raconteur Web Browser

Scenario: Using Firefox (default)

	Visit "http://google.com"
	Set "q" to "Raconteur"
	Title should be "raconteur - Google Search"

Scenario: Using Chrome

	Use "Chrome"
	Visit "http://google.com"
	Set "q" to "Raconteur"
	Title should be "raconteur - Google Search"
Feature: Employee Search
	In order to put a face to a name
	As a fellow Employee
	I want to find coworkes by name and see their info

Scenario: Find existing Employees

	When I search for existing Employees
	I should be able to find them

Scenario: Find by First Name

	Given an Employee named "Marco" "Polo"
	When I search for Employees whose "first name" "is" "Marco"
	I should find one whose "last name" is "Polo"

Scenario: Find by Last Name

	Given an Employee named "Marco" "Polo"
	When I search for Employees whose "last name" "is" "Polo"
	I should find one whose "first name" is "Marco"

Feature: Employee Search
	In order to put a face to a name
	As a fellow Employee
	I want to find coworkes by name and see their info

Scenario: Find existing Employees

	When I search for existing Employees
	I should be able to find them

Scenario: Find by First Name

	Given an Employee named "Marco" "Polo"
	When I search for Employees with first name "Marco"
	I should find one with last name "Polo"

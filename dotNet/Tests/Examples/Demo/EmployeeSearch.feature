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

Scenario: Find Waldo

	Given the Employees:
	[ first name | last name ]
	| Robinson   | Crusoe    |
	| Long John  | Silver    |
	| Waldo      |           |
	| Ali        | Baba      |
	When I search for Employees whose "first name" "is" "Waldo"
	I should find one whose "first name" is "Waldo"

Scenario: Find Waldo & Crusoe

	Given the Employees:
	[ first name | last name ]
	| Robinson   | Crusoe    |
	| Long John  | Silver    |
	| Waldo      |           |
	| Ali        | Baba      |
	
	When I search for Employees whose:
	|first name| contains |o|
	|last name| is not |Silver|
	
	I should find 
	[ first name | last name ]
	| Robinson   | Crusoe    |
	| Waldo      |	         |

Scenario Outline: Find Waldo, Crusoe & Ali Baba

	When I search for Employees whose "criteria" "operator" "expected value"
	I should find one whose "attribute" is "actual value"

	Examples:
	[ criteria    | operator    | expected value | attribute  | actual value ]
	| first name  | is          | Waldo          | first name | Waldo        |
	| first name  | starts with | Robin          | last name  | Crusoe       |
	| last name   | contains    | Baba           | first name | Ali          |

Feature: Step Table
	In order to write tabular style tests
	I want to be able to pass a table into a step

Scenario: Using Tables
	When a Table is declared
	It should be passed into a Step Args

Scenario: Tables with Args
	When a Table declaration has Args
	Each Step will start with the Args

Scenario: Tables with Header
	When a Table declaration has a Header row
	The Header should be skipped
	And each row should become a Step with cols as Args


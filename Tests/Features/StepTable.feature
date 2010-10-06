Feature: Step Table
	In order to write tabular style tests
	I want to be able to pass a table into a step

Scenario: Using Tables
	When a Table is declared
	Each row should become a Step with cols as Args

Scenario: Tables with Args
	When a Table declaration has Args
	Each Step will start with the Args

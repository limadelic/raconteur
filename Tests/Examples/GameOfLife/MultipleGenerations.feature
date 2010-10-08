Feature: Evolving a grid over multiple generations
  In order to create a functioning rules engine
  As a programmer of Conway's Game of Life
  I can evolve a grid over multiple generations

  Scenario: Cells come alive then die off
    Given the following setup
      |___|___|___|___|___|
      | . | . | . | . | . |
      | . | . | . | . | . |
      | . | x | x | x | . |
      | . | . | . | . | . |
      | . | . | . | . | . |
    When I evolve the board
    Then I should see the following board
      |___|___|___|___|___|
      | . | . | . | . | . |
      | . | . | x | . | . |
      | . | . | x | . | . |
      | . | . | x | . | . |
      | . | . | . | . | . |
    When I evolve the board
    Then I should see the following board
      |___|___|___|___|___|
      | . | . | . | . | . |
      | . | . | . | . | . |
      | . | x | x | x | . |
      | . | . | . | . | . |
      | . | . | . | . | . |
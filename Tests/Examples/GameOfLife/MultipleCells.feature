Feature: Evolving a grid with some cells in it
  In order to create a functioning rules engine
  As a programmer of Conway's Game of Life
  I can evolve a multiple cell grid

  Scenario: Sparse grid with nobody staying alive
    Given the following setup
      |___|___|___|___|___|
      | . | . | . | . | . |
      | . | x | . | x | . |
      | . | . | . | . | . |
      | . | x | . | x | . |
      | . | . | . | . | . |
    When I evolve the board
    Then I should see the following board
      |___|___|___|___|___|
      | . | . | . | . | . |
      | . | . | . | . | . |
      | . | . | . | . | . |
      | . | . | . | . | . |
      | . | . | . | . | . |

  Scenario: Over_crowded grid
    Given the following setup
      |___|___|___|___|___|
      | . | . | . | . | . |
      | . | x | x | x | . |
      | . | x | x | x | . |
      | . | x | x | x | . |
      | . | . | . | . | . |
    When I evolve the board
    Then I should see the following board
      |___|___|___|___|___|
      | . | . | x | . | . |
      | . | x | . | x | . |
      | x | . | . | . | x |
      | . | x | . | x | . |
      | . | . | x | . | . |

  Scenario: Multiple dead cells coming to life
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
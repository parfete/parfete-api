Feature: Get parties
    It should return a list of parties

    Scenario: I want to retrieve a list of parties
        Given I use the API version 1
        Given I have a list of parties
            | Name    | Date       | Address   |
            | Party 1 | 01/01/2000 | Address 1 |
            | Party 2 | 01/01/2000 | Address 2 |
            | Party 3 | 01/01/2000 | Address 3 |
        When I retrieve the list of parties
        Then I should get a list of parties
            | Name    | Date       | Address   |
            | Party 1 | 01/01/2000 | Address 1 |
            | Party 2 | 01/01/2000 | Address 2 |
            | Party 3 | 01/01/2000 | Address 3 |

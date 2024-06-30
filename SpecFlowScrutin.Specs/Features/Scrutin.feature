Feature: Majority Vote Election
	As a client
	I want to calculate the result of a majority vote election
	So that I can get the winner of the vote
	
@mytag
Scenario: Election must be closed to determine a winner
	Given the candidate Alice is added
	And the candidate Bob is added
	And the candidate Alice receives 6 votes
	And the candidate Bob receives 4 votes
	When the election is closed
	And the winner is determined
	Then the winner should be Alice
	
@mytag
Scenario: Candidate wins with more than 50% of the votes
	Given the candidate Alice is added
	And the candidate Bob is added
	And the candidate Alice receives 7 votes
	And the candidate Bob receives 3 votes
	When the election is closed
	And the winner is determined
	Then the winner should be Alice
	And the votes should be:
	  | Candidate | Votes |
	  | Alice     | 7     |
	  | Bob       | 3     |
	And the percentages should be:
	  | Candidate | Percentage |
	  | Alice     | 70.0       |
	  | Bob       | 30.0       |
   
@mytag
Scenario: Display votes and percentages for each candidate at the close of election
	Given the candidate Alice is added
	And the candidate Bob is added
	And the candidate Alice receives 5 votes
	And the candidate Bob receives 5 votes
	When the election is closed
	Then the votes should be:
	  | Candidate | Votes |
	  | Alice     | 5     |
	  | Bob       | 5     |
	And the percentages should be:
	  | Candidate | Percentage |
	  | Alice     | 50.0       |
	  | Bob       | 50.0       |
   
@mytag
Scenario: Second round needed when no candidate has more than 50% of the votes
	Given the candidate Alice is added
	And the candidate Bob is added
	And the candidate Leo is added
	And the candidate Alice receives 35 votes
	And the candidate Bob receives 30 votes
	And the candidate Leo receives 30 votes
	When the election is closed
	And the winner is determined
	Then the winner should be Second round needed.
	And the votes should be:
	  | Candidate | Votes |
	  | Alice     | 0     |
	  | Bob       | 0     |
	And the percentages should be:
	  | Candidate | Percentage |
	  | Alice     | 0.0        |
	  | Bob       | 0.0        |

@mytag
Scenario: Maximum two rounds of election are allowed
	Given the candidate Alice is added
	And the candidate Bob is added
	And the candidate Leo is added
	And the candidate Alice receives 40 votes
	And the candidate Bob receives 35 votes
	And the candidate Leo receives 35 votes
	When the election is closed
	And the winner is determined
	Then the winner should be Second round needed.
	And the votes should be:
	  | Candidate | Votes |
	  | Alice     | 0     |
	  | Bob       | 0     |
	And the percentages should be:
	  | Candidate | Percentage |
	  | Alice     | 0.0        |
	  | Bob       | 0.0        |
	Given the candidate Alice receives 40 votes
	And the candidate Bob receives 35 votes
	And the candidate Leo receives 35 votes
	When the election is closed
	And the winner is determined
	Then the winner should be Maximum rounds reached.
	And the votes should be:
	  | Candidate | Votes |
	  | Alice     | 0     |
	  | Bob       | 0     |
	And the percentages should be:
	  | Candidate | Percentage |
	  | Alice     | 0.0        |
	  | Bob       | 0.0        |
   
@mytag
Scenario: Winner determined by highest percentage in final round
	Given the candidate Alice is added
	And the candidate Bob is added
	And the candidate Leo is added
	And the candidate Alice receives 30 votes
	And the candidate Bob receives 30 votes
	And the candidate Leo receives 30 votes
	When the election is closed
	And the winner is determined
	Then the winner should be Second round needed.
	Given the candidate Alice receives 15 votes
	And the candidate Bob receives 35 votes
	And the candidate Leo receives 15 votes
	When the election is closed
	And the winner is determined
	Then the winner should be Bob
	
@mytag
Scenario: Tie in final round results in no winner
	Given the candidate Alice is added
	And the candidate Bob is added
	And the candidate Alice receives 25 votes
	And the candidate Bob receives 25 votes
	When the election is closed
	And the winner is determined
	Then the winner should be Second round needed.
	Given the candidate Alice receives 20 votes
	And the candidate Bob receives 20 votes
	When the election is closed
	And the winner is determined
	Then the winner should be Tie
	
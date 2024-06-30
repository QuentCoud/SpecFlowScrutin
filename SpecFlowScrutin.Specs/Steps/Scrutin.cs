using Xunit.Abstractions;

namespace SpecFlowScrutin.Specs.Steps;
using Xunit;

[Binding]
public sealed class ScrutinStepDefinitions
{
    private readonly ScenarioContext _scenarioContext;
    private readonly Scrutin _scrutin;
    private string _winner;
    private int _result;
    private readonly ITestOutputHelper _output;

    public ScrutinStepDefinitions(ScenarioContext scenarioContext, ITestOutputHelper output)
    {
        _scenarioContext = scenarioContext;
        _scrutin = new Scrutin(output);
        _output = output;
    }

    [Given("the candidate (.*) is added")]
    public void GivenTheCandidateIsAdded(string candidate)
    {
        _scrutin.AddCandidate(candidate);
    }

    [Given("the candidate (.*) receives (.*) votes")]
    public void GivenTheCandidateReceivesVotes(string candidate, int votes)
    {
        for (int i = 0; i < votes; i++)
        {
            _scrutin.AddVote(candidate);
        }
    }

    [When("the election is closed")]
    public void WhenTheElectionIsClosed()
    {
        _scrutin.Close();
    }

    [When("the winner is determined")]
    public void WhenTheWinnerIsDetermined()
    {
        _winner = _scrutin.GetWinner();
    }

    [Then("the winner should be (.*)")]
    public void ThenTheWinnerShouldBe(string expectedWinner)
    {
        Assert.Equal(expectedWinner, _winner);
    }

    [Then("the votes should be:")]
    public void ThenTheVotesShouldBe(Table table)
    {
        foreach (var row in table.Rows)
        {
            string candidate = row["Candidate"];
            int expectedVotes = int.Parse(row["Votes"]);
            Assert.Equal(expectedVotes, _scrutin.GetVotes(candidate));
        }
    }

    [Then("the percentages should be:")]
    public void ThenThePercentagesShouldBe(Table table)
    {
        foreach (var row in table.Rows)
        {
            string candidate = row["Candidate"];
            double expectedPercentage = double.Parse(row["Percentage"]);
            Assert.Equal(expectedPercentage, _scrutin.GetPercentage(candidate), 2);
        }
    }
    [Then("Second round needed. is returned")]
    public void ThenSecondRoundNeededIsReturned()
    {
        Assert.Equal("Second round needed.", _winner);
    }
}
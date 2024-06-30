using System;
using Xunit.Abstractions;

namespace SpecFlowScrutin
{
    public class Scrutin
    {
        private readonly ITestOutputHelper output;
        private Dictionary<string, int> _votes;
        public bool IsClosed { get; private set; }
        public int Round { get; private set; }

        public Scrutin(ITestOutputHelper output)
        {
            _votes = new Dictionary<string, int>();
            IsClosed = false;
            Round = 1;
            this.output = output;
        }

        public void AddCandidate(string candidate)
        {
            if (!_votes.ContainsKey(candidate))
            {
                _votes[candidate] = 0;
                output.WriteLine($"Candidate {candidate} added.");
            }
        }

        public void AddVote(string candidate)
        {
            if (IsClosed)
            {
                throw new InvalidOperationException("Cannot add votes to a closed election.");
            }

            if (_votes.ContainsKey(candidate))
            {
                _votes[candidate]++;
            }
            else
            {
                _votes[candidate] = 1;
            }
            output.WriteLine($"Vote added for {candidate}. Total votes for {candidate}: {_votes[candidate]}");
        }

        public void Close()
        {
            IsClosed = true;
            output.WriteLine("Election closed.");
        }

        public string GetWinner()
        {
            if (!IsClosed)
            {
                throw new InvalidOperationException("Cannot determine winner of an open election.");
            }

            int totalVotes = _votes.Values.Sum();
            var candidatePercentages = _votes.ToDictionary(k => k.Key, v => (double)v.Value / totalVotes * 100);

            foreach (var candidate in candidatePercentages)
            {
                output.WriteLine($"{candidate.Key}: {candidate.Value}% ({_votes[candidate.Key]} votes)");
            }

            var topCandidates = candidatePercentages.OrderByDescending(c => c.Value).Take(3).ToList();
            if (topCandidates.First().Value > 50)
            {
                return topCandidates.First().Key;
            }
            else if (Round == 1)
            {
                Round++;
                IsClosed = false;
                _votes = _votes.Where(v => topCandidates.Any(c => c.Key == v.Key))
                    .ToDictionary(v => v.Key, v => 0);
                return "Second round needed.";
            }
            else if (Round == 2 && topCandidates.First().Value == topCandidates.Last().Value)
            {
                return "Tie";
            }
            else if (Round == 2)
            {
                Round++;
                IsClosed = true;
                _votes = _votes.Where(v => topCandidates.Any(c => c.Key == v.Key))
                    .ToDictionary(v => v.Key, v => 0);
                return "Maximum rounds reached.";
            }
            else
            {
                return topCandidates.First().Key;
            }
        }

        public int GetVotes(string candidate)
        {
            return _votes.ContainsKey(candidate) ? _votes[candidate] : 0;
        }

        public double GetPercentage(string candidate)
        {
            int totalVotes = _votes.Values.Sum();
            if (totalVotes == 0) return 0;
            return _votes.ContainsKey(candidate) ? (double)_votes[candidate] / totalVotes * 100 : 0;
        }
        public List<string> GetCandidates()
        {
            return _votes.Keys.ToList();
        }
    }
}
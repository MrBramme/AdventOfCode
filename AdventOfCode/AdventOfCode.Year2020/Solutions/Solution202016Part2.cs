using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
namespace AdventOfCode.Year2020.Solutions
{
    public class Solution202016Part2 : ISolution
    {
        private readonly ILogger<Solution202016Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2020\\Day16.txt";
        public Solution202016Part2(ILogger<Solution202016Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }
        public string GetSolution()
        {
            var inputData = _inputService.GetInput(resourceLocation);
            var regexRestrictions = new Regex("(?'name'^.*): (?'l1'\\d*)-(?'l2'\\d*) or (?'r1'\\d*)-(?'r2'\\d*)");

            var restrictions = new List<Restriction>();
            var validTickets = new List<int[]>();
            long[] myTicket = new long[0];
            var inputStage = 0;
            foreach (var input in inputData)
            {
                switch (inputStage)
                {
                    case 0:
                        if (string.IsNullOrEmpty(input))
                        {
                            inputStage++;
                            break;
                        }
                        var matches = regexRestrictions.Match(input);
                        restrictions.Add(
                            new Restriction(
                                matches.Groups["name"].Value,
                                int.Parse(matches.Groups["l1"].Value),
                                int.Parse(matches.Groups["l2"].Value),
                                int.Parse(matches.Groups["r1"].Value),
                                int.Parse(matches.Groups["r2"].Value)));
                        break;
                    case 1:
                        if (string.IsNullOrEmpty(input))
                        {
                            inputStage++;
                            break;
                        }
                        if (!input.Equals("your ticket:"))
                        {
                            myTicket = input.Split(',').Select(long.Parse).ToArray();
                        }
                        break;
                    case 2:
                        if (!input.Equals("nearby tickets:"))
                        {
                            var ticketData = input.Split(',').Select(int.Parse).ToArray();
                            if (ticketData.All(ticket => restrictions.Any(x => (ticket >= x.Min1 && ticket <= x.Max1) || ticket >= x.Min2 && ticket <= x.Max2)))
                            {
                                validTickets.Add(ticketData);
                            }
                        }
                        break;
                }
            }

            var possibilities = restrictions.ToDictionary(r => r.Name, r => new List<int>());
            var numberOfFields = validTickets.First().Length;
            for (var index = 0; index < numberOfFields; index++)
            {
                var currentFieldData = validTickets.Select(t => t[index]);
                foreach (var validRestriction in restrictions.Where(r => currentFieldData.All(f => (f >= r.Min1 && f <= r.Max1) || (f >= r.Min2 && f <= r.Max2))))
                {
                    possibilities[validRestriction.Name].Add(index);
                }
            }

            for (var i = 0; i < numberOfFields; i++)
            {
                var singlePossibility = possibilities.First(p => p.Value.Count == 1);
                var index = singlePossibility.Value.First();
                restrictions.First(r => r.Name.Equals(singlePossibility.Key)).Index = index;
                possibilities.Remove(singlePossibility.Key);
                foreach (var possibility in possibilities.Where(p => p.Value.Contains(index)))
                {
                    possibility.Value.Remove(index);
                }
            }
            var departureIndices = restrictions.Where(r => r.Name.StartsWith("departure")).Select(r => r.Index);
            var result = departureIndices.Aggregate(1L, (current, departureIndex) => current * myTicket[departureIndex]);
            return $"{result}";
        }

        class Restriction
        {
            public string Name { get; }
            public int Min1 { get; }
            public int Max1 { get; }
            public int Min2 { get; }
            public int Max2 { get; }
            public int Index { get; set; } = -1;

            public Restriction(string name, int min1, int max1, int min2, int max2)
            {
                Name = name;
                Min1 = min1;
                Max1 = max1;
                Min2 = min2;
                Max2 = max2;
            }
        }
    }
}
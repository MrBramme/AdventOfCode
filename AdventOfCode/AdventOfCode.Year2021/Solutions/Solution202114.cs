using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021.Solutions
{
    public class Solution202114 : ISolution
    {
        private readonly ILogger<Solution202114> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2021\\Day14.txt";

        public Solution202114(ILogger<Solution202114> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var input = _inputService.GetInput(resourceLocation);
            
            var polymer = input[0].ToCharArray();
            var rules = input.Skip(2).Select(x => new InsertionRule(x));

            var counters = new Dictionary<char, int>();
            foreach (var c in polymer)
            {   
                if (counters.ContainsKey(c))
                {
                    counters[c]++;
                } else
                {
                    counters[c] = 1;
                }
            }

            for(var iteration = 0; iteration < 10; iteration++)
            {
                var newPolymer = new List<char>();
                newPolymer.Add(polymer[0]);
                for (var i = 1; i < polymer.Length; i++)
                {
                    var first = polymer[i - 1];
                    var second = polymer[i];
                    var rule = rules.FirstOrDefault(x => x.First == first && x.Second == second);
                    if (rule != null)
                    {
                        newPolymer.Add(rule.Insert);
                        if (counters.ContainsKey(rule.Insert))
                        {
                            counters[rule.Insert]++;
                        }
                        else
                        {
                            counters[rule.Insert] = 1;
                        }
                    }
                    newPolymer.Add(second);
                }
                polymer = newPolymer.ToArray();
            }

            var orderValues = counters.Values.OrderBy(x => x);

            var result = orderValues.Last() - orderValues.First();
            return $"{result}";
        }

        private class InsertionRule
        {
            public char First { get; set; }
            public char Second { get; set; }
            public char Insert { get; set; }
            public InsertionRule(string input)
            {
                var parts = input.ToCharArray();
                First = parts[0];
                Second = parts[1];
                Insert = parts[6];
            }
        }
    }
}
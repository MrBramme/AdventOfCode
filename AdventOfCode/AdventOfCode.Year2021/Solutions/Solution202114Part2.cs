using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode.Year2021.Solutions
{
    public class Solution202114Part2 : ISolution
    {
        private readonly ILogger<Solution202114Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2021\\Day14.txt";

        public Solution202114Part2(ILogger<Solution202114Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var input = _inputService.GetInput(resourceLocation);
            
            var polymer = GetPolymer(input[0]);
            var rules = GetRules(input);
            
            for (var iteration = 1; iteration <= 40; iteration++)
            {
                var newPolymer = new Dictionary<string, long>();
                foreach (var pair in polymer)
                {
                    if (rules.ContainsKey(pair.Key))
                    {
                        var insert = rules[pair.Key];
                        var new1 = $"{pair.Key.Substring(0, 1)}{insert}";
                        var new2 = $"{insert}{pair.Key.Substring(1, 1)}";
                        if (newPolymer.ContainsKey(new1))
                        {
                            newPolymer[new1]+= pair.Value;
                        }
                        else
                        {
                            newPolymer[new1] = pair.Value;
                        }

                        if (newPolymer.ContainsKey(new2))
                        {
                            newPolymer[new2]+= pair.Value;
                        }
                        else
                        {
                            newPolymer[new2] = pair.Value;
                        }
                    } else
                    {
                        if (newPolymer.ContainsKey(pair.Key))
                        {
                            newPolymer[pair.Key] += pair.Value;
                        }
                        else
                        {
                            newPolymer[pair.Key] = pair.Value;
                        }
                    }
                }
                polymer = newPolymer;
            }

            var counters = new Dictionary<char, long>();
            foreach (var pair in polymer)
            {
                var chars = pair.Key.ToCharArray();
                if (counters.ContainsKey(chars[0]))
                {
                    counters[chars[0]] += pair.Value;
                }
                else
                {
                    counters.Add(chars[0], pair.Value);
                }
            }
            counters[input[0].Last()]++; // this one is off by 1 due to how we counted
            var orderCounters = counters.Values.OrderBy(x => x);
            var result = orderCounters.Last() - orderCounters.First();

            return $"{result}";
        }

        private Dictionary<string, long> GetPolymer(string input)
        {
            var polymer = new Dictionary<string, long>();
            foreach (var i in Enumerable.Range(0, input.Length - 1))
            {
                var pair = input.Substring(i, 2);
                if (polymer.ContainsKey(pair))
                {
                    polymer[pair]++;
                }
                else
                {
                    polymer[pair] = 1;
                }
            }
            return polymer;
        }

        private Dictionary<string, string> GetRules(string[] input)
        {
            var rulesList = input.Skip(2).ToList();
            var rules = new Dictionary<string, string>();
            foreach(var ruleString in rulesList)
            {
                rules.Add(ruleString.Substring(0,2), ruleString.Substring(6,1));
            }
            return rules;
}
    }
}
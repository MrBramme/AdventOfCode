using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2020.Solutions
{
    public class Solution202007Part2 : ISolution
    {
        private readonly ILogger<Solution202007Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2020\\Day07.txt";

        public Solution202007Part2(ILogger<Solution202007Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var inputData = _inputService.GetInput(resourceLocation);
            var bags = new Dictionary<string, Bag>();
            foreach (var row in inputData)
            {
                var parts = row.Split(new[] { "contain" }, StringSplitOptions.RemoveEmptyEntries);
                var bagName = parts[0].Replace(" bags ", "");
                if (!bags.ContainsKey(bagName))
                {
                    bags.Add(bagName, new Bag(bagName));
                }
                var bag = bags[bagName];

                var childBagInputs = parts[1].Replace(".", "").Split(',');
                if (childBagInputs[0] != " no other bags")
                {
                    var childRegex = new Regex("(?>(?>(?'amount'[0-9]) (?'bag'[a-z]* [a-z]*)) bags?.{0,2})");
                    foreach (var childBagInput in childBagInputs)
                    {
                        var childMatch = childRegex.Match(childBagInput);
                        var childBagName = childMatch.Groups["bag"].Value;
                        var childBagCount = int.Parse(childMatch.Groups["amount"].Value);
                        if (!bags.ContainsKey(childBagName))
                        {
                            var childBag = new Bag(childBagName);
                            bags.Add(childBag.Name, childBag);
                        }
                        for (var i = 0; i < childBagCount; i++)
                        {
                            bag.Add(bags[childBagName]);
                        }
                    }
                }
            }

            var result = bags["shiny gold"].CountChildren();
            return $"{result}";
        }

        class Bag
        {

            public List<Bag> Children = new List<Bag>();
            public string Name { get; }

            public Bag(string name)
            {
                Name = name;
            }
            public void Add(Bag bag)
            {
                Children.Add(bag);
            }

            public int CountChildren()
            {
                if (!Children.Any())
                {
                    return 0;
                }

                var currentChildren = Children.Count;
                return currentChildren + Children.Sum(x => x.CountChildren());
            }
        }
    }
}
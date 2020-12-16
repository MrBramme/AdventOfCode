using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
namespace AdventOfCode.Year2020.Solutions
{
    public class Solution202016 : ISolution
    {
        private readonly ILogger<Solution202016> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2020\\Day16.txt";
        public Solution202016(ILogger<Solution202016> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }
        public string GetSolution()
        {
            var inputData = _inputService.GetInput(resourceLocation);
            var regexRestrictions = new Regex("^.*: (?'l1'\\d*)-(?'l2'\\d*) or (?'r1'\\d*)-(?'r2'\\d*)");

            var restrictions = new List<(int min, int max)>();
            var data = new List<int>();
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
                        restrictions.Add((int.Parse(matches.Groups["l1"].Value), int.Parse(matches.Groups["l2"].Value)));
                        restrictions.Add((int.Parse(matches.Groups["r1"].Value), int.Parse(matches.Groups["r2"].Value)));
                        break;
                    case 1:
                        if (string.IsNullOrEmpty(input))
                        {
                            inputStage++;
                        }
                        break;
                    case 2:
                        if (!input.Equals("nearby tickets:"))
                        {
                            data.AddRange(input.Split(',').Select(int.Parse));
                        }
                        break;
                }
            }

            var result = 0;
            foreach (var i in data)
            {
                if (!restrictions.Any(x => i >= x.min && i <= x.max))
                {
                    result += i;
                }
            }

            return $"{result}";
        }
    }
}
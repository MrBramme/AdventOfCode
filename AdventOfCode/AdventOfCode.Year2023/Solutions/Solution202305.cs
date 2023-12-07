using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2023.Solutions
{
    public class Solution202305 : ISolution
    {
        private readonly ILogger<Solution202305> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2023\\Day05.txt";

        public Solution202305(ILogger<Solution202305> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }
        public string GetSolution()
        {
            var values = _inputService.GetInput(resourceLocation);
            var seedValues = ExtractNumbers(values[0]);

            var mapping = new NumberMapping();
            for (var i = 3; i < values.Length; i++)
            {
                var numbers = ExtractNumbers(values[i]);
                if (numbers.Length == 0)
                {
                    if (string.IsNullOrEmpty(values[i]))
                    {
                        for (var j = 0; j < seedValues.Length; j++)
                        {
                            seedValues[j] = mapping.ProcessInput(seedValues[j]);
                        }
                        mapping = new NumberMapping();
                    }
                    else
                    {
                        _logger.LogInformation(values[i]);
                    }
                    continue;
                }

                mapping.AddMapping(numbers[0], numbers[1], numbers[2]);

            }

            for (var j = 0; j < seedValues.Length; j++)
            {
                seedValues[j] = mapping.ProcessInput(seedValues[j]);
            }

            var result = seedValues.Min();

            return $"{result}";
        }

        private long[] ExtractNumbers(string input)
        {
            var matches = Regex.Matches(input, @"\d+");
            return matches.Select(x => long.Parse(x.Value)).ToArray();
        }

        private class NumberMapping
        {
            private readonly List<(long sourceStart, long sourceEnd, long destinationStart)> _ranges = new();
            public void AddMapping(long destination, long source, long rangeLength)
            {
                (long sourceStart, long sourceEnd, long destinationStart) range = (source, source + rangeLength - 1, destination);
                _ranges.Add(range);
            }

            public long ProcessInput(long input)
            {
                var ranges = _ranges.Where(x => x.sourceStart <= input && x.sourceEnd >= input).ToList();
                if (!ranges.Any()) return input;
                var range = ranges.Last();
                return input - range.sourceStart + range.destinationStart;
            }
        }
    }
}
using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2023.Solutions
{
    public class Solution202306 : ISolution
    {
        private readonly ILogger<Solution202306> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2023\\Day06.txt";

        public Solution202306(ILogger<Solution202306> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }
        public string GetSolution()
        {
            var values = _inputService.GetInput(resourceLocation);
            var times = ExtractNumbers(values[0]);
            var distances = ExtractNumbers(values[1]);
            var races = times.Select((t, i) => new Race(t, distances[i])).ToList();

            var result = races.Select(x => x.CalculateWinningRaces()).Aggregate((currentProduct, nextNumber) => currentProduct * nextNumber);

            return $"{result}";
        }

        private int[] ExtractNumbers(string input)
        {
            var matches = Regex.Matches(input, @"\d+");
            return matches.Select(x => int.Parse(x.Value)).ToArray();
        }

        private class Race(int Time, int Distance)
        {
            public int CalculateWinningRaces()
            {
                var winningRaces = 0;
                for (var speed = 0; speed < Time; speed++)
                {
                    var raceDistance = speed * (Time - speed);
                    if (raceDistance > Distance) winningRaces++;
                }
                return winningRaces;
            }
        }
    }
}
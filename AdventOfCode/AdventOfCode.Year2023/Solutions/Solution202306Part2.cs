using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2023.Solutions
{
    public class Solution202306Part2 : ISolution
    {
        private readonly ILogger<Solution202306Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2023\\Day06.txt";

        public Solution202306Part2(ILogger<Solution202306Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }
        public string GetSolution()
        {
            var values = _inputService.GetInput(resourceLocation);
            var time = ExtractNumbers(values[0].Replace(" ", ""))[0];
            var distance = ExtractNumbers(values[1].Replace(" ", ""))[0];
            var race = new Race(time, distance);

            return $"{race.CalculateWinningRaces()}";
        }

        private long[] ExtractNumbers(string input)
        {
            var matches = Regex.Matches(input, @"\d+");
            return matches.Select(x => long.Parse(x.Value)).ToArray();
        }

        private class Race(long Time, long Distance)
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
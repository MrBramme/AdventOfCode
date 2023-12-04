using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AdventOfCode.Year2023.Solutions
{
    public class Solution202304 : ISolution
    {
        private readonly ILogger<Solution202304> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2023\\Day04.txt";

        public Solution202304(ILogger<Solution202304> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }
        public string GetSolution()
        {
            var values = _inputService.GetInput(resourceLocation).Select(x => x.Split(":")[1].Split("|")).ToArray();
            var result = 0;
            foreach (var value in values)
            {
                var winningNumbers = value[0].Trim().Replace("  ", " ").Split(" ").Select(x => int.Parse(x)).ToList();
                var numbers = value[1].Trim().Replace("  ", " ").Split(" ").Select(x => int.Parse(x)).ToList();
                var commonNumberCount = winningNumbers.Intersect(numbers).Count();
                var score = Enumerable.Range(1, commonNumberCount).Aggregate(0, (current, x) => current == 0 ? current = 1 : current = current * 2);
                result += score;
            }
            return $"{result}";
        }
    }
}
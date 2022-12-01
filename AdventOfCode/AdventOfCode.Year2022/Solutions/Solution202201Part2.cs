using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AdventOfCode.Year2022.Solutions
{
    public class Solution202201Part2 : ISolution
    {
        private readonly ILogger<Solution202201Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2022\\Day01.txt";

        public Solution202201Part2(ILogger<Solution202201Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }
        public string GetSolution()
        {
            var caloriesInput = _inputService.GetInput(resourceLocation).ToList();
            var caloriesPerElf = new List<int>();
            var currentCalories = 0;

            foreach (var calories in caloriesInput)
            {
                if (string.IsNullOrWhiteSpace(calories))
                {
                    caloriesPerElf.Add(currentCalories);
                    currentCalories = 0;
                }
                else
                {
                    currentCalories += int.Parse(calories);
                }
            }
            caloriesPerElf.Add(currentCalories);
            return $"{caloriesPerElf.OrderDescending().Take(3).Sum()}";
        }
    }
}
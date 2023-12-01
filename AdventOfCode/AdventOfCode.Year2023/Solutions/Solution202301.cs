using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AdventOfCode.Year2023.Solutions
{
    public class Solution202301 : ISolution
    {
        private readonly ILogger<Solution202301> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2023\\Day01.txt";

        public Solution202301(ILogger<Solution202301> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }
        public string GetSolution()
        {
            var caloriesInput = _inputService.GetInput(resourceLocation).ToList();
            var maxCalories = 0;
            var currentCalories = 0;

            foreach (var calories in caloriesInput)
            {
                if (string.IsNullOrWhiteSpace(calories))
                {
                    maxCalories = GetMaxCalories(currentCalories, maxCalories);
                    currentCalories = 0;
                }
                else
                {
                    currentCalories += int.Parse(calories);
                }
            }
            maxCalories = GetMaxCalories(currentCalories, maxCalories);
            return $"{maxCalories}";
        }

        private static int GetMaxCalories(int currentCalories, int maxCalories)
        {
            if (currentCalories > maxCalories)
            {
                maxCalories = currentCalories;
            }

            return maxCalories;
        }
    }
}
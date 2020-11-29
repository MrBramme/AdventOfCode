using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;

namespace AdventOfCode.Year2019.Solutions
{
    public class Solution201901 : ISolution
    {
        private readonly ILogger<Solution201901> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2019\\Day01.txt";

        public Solution201901(ILogger<Solution201901> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }
        public string GetSolution()
        {
            var massInput = _inputService.GetInput(resourceLocation);
            var result = 0;
            foreach (var input in massInput)
            {
                var mass = decimal.Parse(input);
                var fuel = Math.Floor(mass / 3) - 2;
                if (fuel < 0)
                {
                    fuel = 0;
                }
                result += (int)fuel;
            }
            return $"{result}";
        }
    }
}
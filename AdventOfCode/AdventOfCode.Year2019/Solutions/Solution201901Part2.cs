using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;

namespace AdventOfCode.Year2019.Solutions
{
    public class Solution201901Part2 : ISolution
    {
        private readonly ILogger<Solution201901Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2019\\Day01.txt";

        public Solution201901Part2(ILogger<Solution201901Part2> logger, IInputService inputService)
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
                var currentFuel = 0;
                var mass = decimal.Parse(input);
                while (mass > 0)
                {
                    var fuelRequirement = GetFuelRequirementForMass(mass);
                    currentFuel += fuelRequirement;
                    mass = fuelRequirement;
                }

                result += currentFuel;
            }
            return $"{result}";
        }

        private int GetFuelRequirementForMass(decimal mass)
        {
            var fuel = Math.Floor(mass / 3) - 2;
            if (fuel < 0)
            {
                fuel = 0;
            }

            return (int)fuel;
        }
    }
}
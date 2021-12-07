using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021.Solutions
{
    public class Solution202107 : ISolution
    {
        private readonly ILogger<Solution202107> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2021\\Day07.txt";

        public Solution202107(ILogger<Solution202107> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var crabs = _inputService.GetInput(resourceLocation).First().Split(",").Select(int.Parse).ToArray();

            (var min, var max) = (crabs.Min(), crabs.Max());

            var fuelCost = int.MaxValue;
            for(var position = min; position <= max; position++)
            {
                var totalFuel = crabs.Sum(c => Math.Abs(c - position));
                if (totalFuel < fuelCost)
                {
                    fuelCost = totalFuel;
                }
            }

            return $"{fuelCost}";
        }

    }

    
}
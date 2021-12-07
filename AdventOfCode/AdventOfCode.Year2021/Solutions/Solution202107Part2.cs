using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021.Solutions
{
    public class Solution202107Part2 : ISolution
    {
        private readonly ILogger<Solution202107Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2021\\Day07.txt";

        public Solution202107Part2(ILogger<Solution202107Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var crabs = _inputService.GetInput(resourceLocation).First().Split(",").Select(i => new Crab(int.Parse(i))).ToArray();

            (var min, var max) = (crabs.Min(c => c.position), crabs.Max(c => c.position));

            var optimalFuelCost = int.MaxValue;
            for(var position = min; position <= max; position++)
            {
                var totalFuel = crabs.Sum(c => c.FuelcostForMoveTo(position));
                if (totalFuel < optimalFuelCost)
                {
                    optimalFuelCost = totalFuel;
                }
            }
            return $"{optimalFuelCost}";
        }

        private record Crab(int position)
        {
            public int FuelcostForMoveTo(int target)
            {
                var distance = Math.Abs(position - target);
                return (1 + distance) * distance / 2;
            }
        }

    }

    
}
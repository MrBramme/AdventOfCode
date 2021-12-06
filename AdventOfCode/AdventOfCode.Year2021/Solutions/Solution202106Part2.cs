using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021.Solutions
{
    public class Solution202106Part2 : ISolution
    {
        private readonly ILogger<Solution202106Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2021\\Day06.txt";

        public Solution202106Part2(ILogger<Solution202106Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var inputFishes = _inputService.GetInput(resourceLocation).First().Split(",").Select(int.Parse).ToArray();
            var fishCounter = new FishCounter(inputFishes);

            return $"{fishCounter.Iterate(256)}";
        }

        private class FishCounter
        {
            private long[] _lifeCycles = new long[9];

            public FishCounter(int[] fishes)
            {
                for (var i = 1; i <= 8; i++)
                {
                    _lifeCycles[i] = fishes.Count(f => f == i);
                }
            }

            public long Iterate(int days)
            {
                for(var day = 0; day < days; day++)
                {
                    var newFish = _lifeCycles[0]; // birthing placeholder
                    for (var i = 1; i <= 8; i++)
                    {
                        _lifeCycles[i - 1] = _lifeCycles[i]; // move everything 1 spot in the lifecycle
                    }
                    _lifeCycles[8] = newFish; // newborns
                    _lifeCycles[6] += newFish; // parents getting back at it
                }

                return _lifeCycles.Sum();
            }
}
    }
}
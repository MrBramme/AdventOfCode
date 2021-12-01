using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace AdventOfCode.Year2021.Solutions
{
    public class Solution202101Part2 : ISolution
    {
        private readonly ILogger<Solution202101Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2021\\Day01.txt";

        public Solution202101Part2(ILogger<Solution202101Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }
        public string GetSolution()
        {
            var depthMeasurements = _inputService.GetInput(resourceLocation).Select(int.Parse).ToArray();
            var steps = 3;
            var nrOfGroups = depthMeasurements.Length - steps + 1;

            var lastMeasurement = int.MaxValue;
            var counter = 0;
            for(var i = 0; i < nrOfGroups; i++)
            {
                var currentDepth = depthMeasurements.Skip(i).Take(steps).Sum();
                if(currentDepth > lastMeasurement)
                {
                    counter++;
                }
                lastMeasurement = currentDepth;
            }

            return $"{counter}";
        }
    }
}
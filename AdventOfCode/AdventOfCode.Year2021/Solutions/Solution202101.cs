using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace AdventOfCode.Year2021.Solutions
{
    public class Solution202101 : ISolution
    {
        private readonly ILogger<Solution202101> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2021\\Day01.txt";

        public Solution202101(ILogger<Solution202101> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }
        public string GetSolution()
        {
            var depthMeasurements = _inputService.GetInput(resourceLocation).Select(int.Parse);
            return depthMeasurements.Aggregate((increaseCounter: 0, lastDepth: int.MaxValue), (counter, depthMeasurement) =>
            {
                if (depthMeasurement > counter.lastDepth)
                {
                    counter.increaseCounter++;
                }
                counter.lastDepth = depthMeasurement;
                return counter;
            }).increaseCounter.ToString();
        }
    }
}
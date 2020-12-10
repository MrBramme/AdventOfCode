using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace AdventOfCode.Year2020.Solutions
{
    public class Solution202010 : ISolution
    {
        private readonly ILogger<Solution202010> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2020\\Day10.txt";

        public Solution202010(ILogger<Solution202010> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var inputData = _inputService.GetInput(resourceLocation).Select(int.Parse).ToArray();
            var jolt1Jumps = 0;
            var jolt3Jumps = 0;

            var data = inputData.OrderBy(x => x).ToArray();
            data = data.Prepend(0).ToArray();
            // assuming all is valid:
            for (var i = 1; i < data.Length; i++)
            {
                var jumpSize = data[i] - data[i - 1];
                switch (jumpSize)
                {
                    case 1:
                        jolt1Jumps++;
                        break;
                    case 3:
                        jolt3Jumps++;
                        break;
                }
            }
            jolt3Jumps++; // Jump to the device
            return $"{jolt1Jumps * jolt3Jumps}";
        }
    }
}
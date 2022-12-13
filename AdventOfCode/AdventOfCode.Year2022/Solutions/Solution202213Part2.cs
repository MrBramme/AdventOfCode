using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AdventOfCode.Year2022.Solutions
{
    public class Solution202213Part2 : ISolution
    {
        private readonly ILogger<Solution202213Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2022/Day13.txt";

        public Solution202213Part2(ILogger<Solution202213Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var input = _inputService.GetInput(resourceLocation).ToList();
            var result = 0;

            return $"{result}";
        }
    }
}
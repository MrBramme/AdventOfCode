using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AdventOfCode.Year2022.Solutions
{
    public class Solution202210 : ISolution
    {
        private readonly ILogger<Solution202210> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2022/Day10.txt";

        public Solution202210(ILogger<Solution202210> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var input = _inputService.GetInput(resourceLocation);
            var result = -1;
            return $"{result}";
        }
    }
}
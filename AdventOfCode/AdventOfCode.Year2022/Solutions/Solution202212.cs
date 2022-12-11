using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AdventOfCode.Year2022.Solutions
{
    public class Solution202212 : ISolution
    {
        private readonly ILogger<Solution202212> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2022/Day12.txt";

        public Solution202212(ILogger<Solution202212> logger, IInputService inputService)
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
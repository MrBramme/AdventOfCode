using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AdventOfCode.Year2022.Solutions
{
    public class Solution202205Part2 : ISolution
    {
        private readonly ILogger<Solution202205Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2022\\Day05.txt";

        public Solution202205Part2(ILogger<Solution202205Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            throw new NotImplementedException();
            var input = _inputService.GetInput(resourceLocation);
            var result = 0;
            return $"{result}";
        }
    }
}
using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace AdventOfCode.Year2020.Solutions
{
    public class Solution202002Part2 : ISolution
    {
        private readonly ILogger<Solution202002Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2020\\Day02.txt";

        public Solution202002Part2(ILogger<Solution202002Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }
        public string GetSolution()
        {
            var input = _inputService.GetInput(resourceLocation).Select(int.Parse);
            var result = 0;

            return $"{result}";
        }
    }
}
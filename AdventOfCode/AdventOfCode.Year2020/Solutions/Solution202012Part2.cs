using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
namespace AdventOfCode.Year2020.Solutions
{
    public class Solution202012Part2 : ISolution
    {
        private readonly ILogger<Solution202012Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2020\\Day12.txt";
        public Solution202012Part2(ILogger<Solution202012Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }
        public string GetSolution()
        {
            var inputData = _inputService.GetInput(resourceLocation);

            var result = "TODO";
            return $"{result}";
        }
    }
}
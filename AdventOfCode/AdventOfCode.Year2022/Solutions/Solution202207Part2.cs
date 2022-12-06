using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AdventOfCode.Year2022.Solutions
{
    public class Solution202207Part2 : ISolution
    {
        private readonly ILogger<Solution202207Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2022/Day07.txt";

        public Solution202207Part2(ILogger<Solution202207Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            throw new NotImplementedException();
            var input = _inputService.GetInput(resourceLocation).First().ToCharArray();
            var result = -1;

            return $"{result}";
        }
    }
}
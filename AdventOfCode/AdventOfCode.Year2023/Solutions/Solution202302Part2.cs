using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AdventOfCode.Year2023.Solutions
{
    public class Solution202302Part2 : ISolution
    {
        private readonly ILogger<Solution202302Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2023\\Day02.txt";

        public Solution202302Part2(ILogger<Solution202302Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }
        public string GetSolution()
        {
            throw new NotImplementedException();

            return $"";
        }
    }
}
using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AdventOfCode.Year2022.Solutions
{
    public class Solution202204 : ISolution
    {
        private readonly ILogger<Solution202204> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2022\\Day04.txt";

        public Solution202204(ILogger<Solution202204> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            throw new NotImplementedException();
            var input = _inputService.GetInput(resourceLocation).Select(x => x.ToCharArray()).ToList();
            var result = 0;
            return $"{result}";
        }
    }
}
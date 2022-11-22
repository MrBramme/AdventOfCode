using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace AdventOfCode.Year2022.Solutions
{
    public class Solution202201 : ISolution
    {
        private readonly ILogger<Solution202201> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2022\\Day01.txt";

        public Solution202201(ILogger<Solution202201> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }
        public string GetSolution()
        {
            throw new NotImplementedException();
        }
    }
}
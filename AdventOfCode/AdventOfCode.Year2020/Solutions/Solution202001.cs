using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace AdventOfCode.Year2020.Solutions
{
    public class Solution202001 : ISolution
    {
        private readonly ILogger<Solution202001> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2020\\Day01.txt";

        public Solution202001(ILogger<Solution202001> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }
        public string GetSolution()
        {
            var expenses = _inputService.GetInput(resourceLocation).Select(int.Parse);
            var result = 0;
            foreach (var expense in expenses)
            {
                if (expenses.Contains(2020 - expense))
                {
                    result = expense * (2020 - expense);
                    break;
                }
            }
            return $"{result}";
        }
    }
}
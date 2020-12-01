using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace AdventOfCode.Year2020.Solutions
{
    public class Solution202001Part2 : ISolution
    {
        private readonly ILogger<Solution202001Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2020\\Day01.txt";

        public Solution202001Part2(ILogger<Solution202001Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }
        public string GetSolution()
        {
            var expenses = _inputService.GetInput(resourceLocation).Select(int.Parse);

            var result = 0;
            foreach (var expense1 in expenses)
            {
                foreach (var expense2 in expenses.Where(ex => ex + expense1 < 2020))
                {
                    var residual = 2020 - expense1 - expense2;
                    if (expenses.Contains(residual))
                    {
                        result = expense1 * expense2 * residual;
                        break;
                    }
                }

                if (result > 0)
                {
                    break;
                }
            }
            return $"{result}";
        }
    }
}
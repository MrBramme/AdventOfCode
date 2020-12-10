using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace AdventOfCode.Year2020.Solutions
{
    public class Solution202010Part2 : ISolution
    {
        private readonly ILogger<Solution202010Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2020\\Day10.txt";

        public Solution202010Part2(ILogger<Solution202010Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var inputData = _inputService.GetInput(resourceLocation).Select(int.Parse).ToArray();
            var data = inputData.OrderBy(x => x).ToArray();

            (long validWays0, long validWays1, long validWays2) = (0, 0, 1);
            var (previous0, previous1, previous2) = (int.MinValue, int.MinValue, 0);

            foreach (var num in data)
            {
                var ways = validWays2;
                if (num - previous1 <= 3)
                {
                    ways += validWays1;
                    if (num - previous0 <= 3)
                    {
                        ways += validWays0;
                    }
                }

                (previous0, previous1, previous2) = (previous1, previous2, num);
                (validWays0, validWays1, validWays2) = (validWays1, validWays2, ways);
            }

            return $"{validWays2}";
        }
    }
}
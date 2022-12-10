using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AdventOfCode.Year2022.Solutions
{
    public class Solution202210 : ISolution
    {
        private readonly ILogger<Solution202210> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2022/Day10.txt";

        public Solution202210(ILogger<Solution202210> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var input = _inputService.GetInput(resourceLocation).ToList();
            var x = 1;
            var cycle = 0;
            var result = 0;

            var cyclesToCheck = new[] { 20, 60, 100, 140, 180, 220 };

            foreach (var command in input)
            {
                if (command == "noop")
                {
                    cycle++;
                    if (cyclesToCheck.Contains(cycle))
                    {
                        result += cycle * x;
                    }
                    continue;
                }

                var amount = int.Parse(command.Split(" ")[1]);
                cycle++;
                if (cyclesToCheck.Contains(cycle))
                {
                    result += cycle * x;
                }
                cycle++;
                if (cyclesToCheck.Contains(cycle))
                {
                    result += cycle * x;
                }
                x += amount;
            }


            return $"{result}";
        }
    }
}
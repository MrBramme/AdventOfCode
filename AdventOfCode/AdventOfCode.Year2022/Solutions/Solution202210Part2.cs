using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text;

namespace AdventOfCode.Year2022.Solutions
{
    public class Solution202210Part2 : ISolution
    {
        private readonly ILogger<Solution202210Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2022/Day10.txt";

        public Solution202210Part2(ILogger<Solution202210Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var input = _inputService.GetInput(resourceLocation).ToList();
            var x = 1;
            var cycle = 0;

            var sb = new StringBuilder();
            var lineBreaks = new[] { 40, 80, 120, 160, 200, 240 };
            var line = 0;
            foreach (var command in input)
            {
                if (command == "noop")
                {
                    cycle++;
                    sb.Append(Math.Abs(x + (40 * line) - (cycle - 1)) <= 1 ? "#" : ".");
                    if (lineBreaks.Contains(cycle))
                    {
                        sb.Append("\r\n");
                        line++;
                    }
                    continue;
                }

                var amount = int.Parse(command.Split(" ")[1]);
                cycle++;
                sb.Append(Math.Abs(x + (40 * line) - (cycle - 1)) <= 1 ? "#" : ".");
                if (lineBreaks.Contains(cycle))
                {
                    sb.Append("\r\n");
                    line++;
                }

                cycle++;
                sb.Append(Math.Abs(x + (40 * line) - (cycle - 1)) <= 1 ? "#" : ".");
                if (lineBreaks.Contains(cycle))
                {
                    sb.Append("\r\n");
                    line++;
                }
                x += amount;
            }

            var result = sb.ToString();
            _logger.LogInformation($"\r\n{result}");
            return $"{result}";
        }
    }
}
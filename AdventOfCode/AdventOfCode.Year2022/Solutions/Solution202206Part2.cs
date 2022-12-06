using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AdventOfCode.Year2022.Solutions
{
    public class Solution202206Part2 : ISolution
    {
        private readonly ILogger<Solution202206Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2022/Day06.txt";

        public Solution202206Part2(ILogger<Solution202206Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var input = _inputService.GetInput(resourceLocation).First().ToCharArray();
            var result = -1;
            for (var i = 14; i < input.Length; i++)
            {
                var cnt = 0;
                for (var j = 14; j > 0; j--)
                {
                    cnt += input[(i - 14)..i].Count(x => x == input[i - j]);
                }

                if (cnt == 14)
                {
                    result = i;
                    break;
                }
            }

            return $"{result}";
        }
    }
}
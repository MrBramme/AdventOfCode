using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AdventOfCode.Year2022.Solutions
{
    public class Solution202206 : ISolution
    {
        private readonly ILogger<Solution202206> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2022/Day06.txt";

        public Solution202206(ILogger<Solution202206> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var input = _inputService.GetInput(resourceLocation).First().ToCharArray();
            var result = -1;
            for (var i = 4; i < input.Length; i++)
            {
                var cnt = 0;
                for (var j = 4; j > 0; j--)
                {
                    cnt += input[(i - 4)..i].Count(x => x == input[i - j]);
                }

                if (cnt == 4)
                {
                    result = i;
                    break;
                }
            }

            return $"{result}";
        }
    }
}
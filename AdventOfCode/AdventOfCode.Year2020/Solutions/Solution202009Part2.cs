using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace AdventOfCode.Year2020.Solutions
{
    public class Solution202009Part2 : ISolution
    {
        private readonly ILogger<Solution202009Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2020\\Day09.txt";

        public Solution202009Part2(ILogger<Solution202009Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var inputData = _inputService.GetInput(resourceLocation).Select(long.Parse).ToArray();
            long expectedValue = 15353384;
            long result = 0;
            for (var i = 0; i < inputData.Length; i++)
            {
                long currentResult = 0;
                var isMatched = false;
                for (var j = i; j < inputData.Length; j++)
                {
                    currentResult += inputData[j];
                    if (currentResult > expectedValue)
                    {
                        break;
                    }

                    if (currentResult == expectedValue)
                    {
                        isMatched = true;
                        var subset = inputData.Skip(i).Take(j - i).ToList();
                        result = subset.Min() + subset.Max();
                        break;
                    }
                }

                if (isMatched)
                {
                    break;
                }

            }
            return $"{result}";
        }
    }
}
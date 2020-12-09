using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace AdventOfCode.Year2020.Solutions
{
    public class Solution202009 : ISolution
    {
        private readonly ILogger<Solution202009> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2020\\Day09.txt";

        public Solution202009(ILogger<Solution202009> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var inputData = _inputService.GetInput(resourceLocation).Select(long.Parse).ToArray();
            var stepsize = 25;
            long result = 0;
            for (var i = stepsize; i < inputData.Length; i++)
            {
                var current = inputData[i];
                var startIndex = i - stepsize;
                var range = inputData.Skip(startIndex).Take(stepsize).ToArray();
                var isMatched = false;
                for (var j = 0; j < stepsize; j++)
                {
                    var value = range[j];
                    var diff = current - value;
                    isMatched = range.Contains(diff);
                    if (isMatched)
                    {
                        break;
                    }
                }
                if (!isMatched)
                {
                    result = current;
                    break;
                }
            }
            return $"{result}";
        }
    }
}
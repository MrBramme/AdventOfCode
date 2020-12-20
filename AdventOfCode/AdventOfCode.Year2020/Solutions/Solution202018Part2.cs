using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Text.RegularExpressions;
namespace AdventOfCode.Year2020.Solutions
{
    public class Solution202018Part2 : ISolution
    {
        private readonly ILogger<Solution202018Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2020\\Day18.txt";
        private readonly Regex _parenthesisRegex = new Regex("\\(([^(]*?)\\)");

        public Solution202018Part2(ILogger<Solution202018Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }
        public string GetSolution()
        {
            var inputData = _inputService.GetInput(resourceLocation);

            var result = 0L;
            foreach (var input in inputData)
            {
                var s = input;
                var isDone = false;
                while (!isDone)
                {
                    var matches = _parenthesisRegex.Match(s);
                    if (matches.Success)
                    {
                        var thisResult = Calculate(matches.Groups[0].Value.Substring(1, matches.Groups[0].Value.Length - 2));
                        s = _parenthesisRegex.Replace(s, $"{thisResult}", 1);
                    }
                    else
                    {
                        var thisResult = Calculate(s);
                        result += thisResult;
                        isDone = true;
                    }

                }
            }

            return $"{result}";
        }

        private long Calculate(string input)
        {
            var inputArray = input.Split(' ');
            for (var i = 1; i < inputArray.Length; i += 2)
            {
                var operation = inputArray[i];
                if (operation.Equals("+"))
                {
                    var sumResult = long.Parse(inputArray[i - 1]) + long.Parse(inputArray[i + 1]);
                    inputArray[i - 1] = null;
                    inputArray[i] = null;
                    inputArray[i + 1] = $"{sumResult}";
                }
            }

            var multiplyArray = inputArray.Where(i => i != null).ToArray();
            var result = long.Parse(multiplyArray[0]);
            for (var i = 1; i < multiplyArray.Length; i += 2)
            {
                var operation = multiplyArray[i];
                var nr = long.Parse(multiplyArray[i + 1]);
                if (operation.Equals("+"))
                {
                    result += nr;
                }
                if (operation.Equals("*"))
                {
                    result *= nr;
                }
            }

            return result;
        }
    }
}
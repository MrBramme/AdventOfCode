using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
namespace AdventOfCode.Year2020.Solutions
{
    public class Solution202018 : ISolution
    {
        private readonly ILogger<Solution202018> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2020\\Day18.txt";
        private readonly Regex _parenthesisRegex = new Regex("\\(([^(]*?)\\)");

        public Solution202018(ILogger<Solution202018> logger, IInputService inputService)
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
            var current = long.Parse(inputArray[0]);
            for (var i = 1; i < inputArray.Length; i += 2)
            {
                var operation = inputArray[i];
                var nr = long.Parse(inputArray[i + 1]);
                if (operation.Equals("+"))
                {
                    current += nr;
                }
                if (operation.Equals("*"))
                {
                    current *= nr;
                }
            }

            return current;
        }
    }
}
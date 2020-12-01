using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace AdventOfCode.Year2019.Solutions
{
    public class Solution201902 : ISolution
    {
        private readonly ILogger<Solution201902> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2019\\Day02.txt";

        public Solution201902(ILogger<Solution201902> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }
        public string GetSolution()
        {
            var input = _inputService.GetInput(resourceLocation)[0].Split(',').Select(int.Parse).ToArray();
            var result = ProcessIntOp(input);
            return $"{result[0]}";
        }

        internal int[] ProcessIntOp(int[] input)
        {
            var currentIndex = 0;
            var operation = input[0];
            while (operation != 99)
            {
                switch (operation)
                {
                    case 1:
                        var sum = input[input[currentIndex + 1]] + input[input[currentIndex + 2]];
                        input[input[currentIndex + 3]] = sum;
                        break;
                    case 2:
                        var multiplication = input[input[currentIndex + 1]] * input[input[currentIndex + 2]];
                        input[input[currentIndex + 3]] = multiplication;
                        break;
                    default:
                        throw new InvalidOperationException($"Operation '{operation}' not found.");
                }

                currentIndex += 4;
                operation = input[currentIndex];
            }

            return input;
        }
    }
}
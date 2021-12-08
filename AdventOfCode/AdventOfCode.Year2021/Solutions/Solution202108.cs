using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021.Solutions
{
    public class Solution202108 : ISolution
    {
        private readonly ILogger<Solution202108> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2021\\Day08.txt";

        public Solution202108(ILogger<Solution202108> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var patterns = _inputService.GetInput(resourceLocation).Select(p => p.Split(" | ").Select(i => i.Split(" ")));
            var validLengths = new[] { 2, 3, 4, 7}; // length of segment input for 1 = 2, 7 = 3, 4 = 4, 7 = 8
            var result = patterns.Sum(p => p.Last().Count(i => validLengths.Contains(i.Length)));
            return $"{result}";
        }
    }
}
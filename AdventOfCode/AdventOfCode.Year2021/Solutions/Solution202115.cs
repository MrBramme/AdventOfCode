using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021.Solutions
{
    public class Solution202115 : ISolution
    {
        private readonly ILogger<Solution202115> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2021\\Day15.txt";

        public Solution202115(ILogger<Solution202115> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var input = _inputService.GetInput(resourceLocation);
            var map = input.Select(x => x.ToCharArray().Select(y => int.Parse(y.ToString())).ToArray()).ToArray();
            // Todo: Dijkstra Algo
            var result = 1;
            return $"{result}";
        }

        public class RiskPoint
        {
            public Point Point { get; set; }
            public int RiskLevel { get; set; }
            public RiskPoint(int x, int y, int risk)
            {
                Point = new Point(x, y);
                RiskLevel = risk;
            }
        }

        public record Point(int X, int Y);
    }
}
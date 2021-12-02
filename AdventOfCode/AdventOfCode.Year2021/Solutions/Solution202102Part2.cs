using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace AdventOfCode.Year2021.Solutions
{
    public class Solution202102Part2 : ISolution
    {
        private readonly ILogger<Solution202102Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2021\\Day02.txt";

        public Solution202102Part2(ILogger<Solution202102Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var course = _inputService.GetInput(resourceLocation);
            var position = new Position();
            return course.Aggregate(position, (acc, step) =>
            {
                var stepParts = step.Split(" ");
                var stepSize = int.Parse(stepParts[1]);
                switch (stepParts[0])
                {
                    case "forward":
                        acc.X += stepSize;
                        acc.Depth += stepSize * acc.Aim;
                        break;
                    case "down":
                        acc.Aim += stepSize;
                        break;
                    case "up":
                        acc.Aim -= stepSize;
                        break;
                    default:
                        throw new NotImplementedException();
                }
                return acc;
            }).GetResult;
        }

        private class Position
        {
            public int X { get; set; }
            public int Depth { get; set; }
            public int Aim { get; set; }
            public string GetResult => Math.Abs(X * Depth).ToString();
        }
    }
}
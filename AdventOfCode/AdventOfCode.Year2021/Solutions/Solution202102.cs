using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace AdventOfCode.Year2021.Solutions
{
    public class Solution202102 : ISolution
    {
        private readonly ILogger<Solution202102> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2021\\Day02.txt";

        public Solution202102(ILogger<Solution202102> logger, IInputService inputService)
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
                switch (stepParts[0])
                {
                    case "forward":
                        acc.X += int.Parse(stepParts[1]);
                        break;
                    case "down":
                        acc.Depth -= int.Parse(stepParts[1]);
                        break;
                    case "up":
                        acc.Depth += int.Parse(stepParts[1]);
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
            public string GetResult => Math.Abs(X * Depth).ToString();
        }
    }
}
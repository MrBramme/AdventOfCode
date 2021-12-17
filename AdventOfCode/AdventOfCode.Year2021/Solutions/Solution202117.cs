using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace AdventOfCode.Year2021.Solutions
{
    public class Solution202117 : ISolution
    {
        private readonly ILogger<Solution202117> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2021\\Day17.txt";

        // inspired by https://github.com/encse/adventofcode/blob/master/2021/Day17/Solution.cs
        public Solution202117(ILogger<Solution202117> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var input = _inputService.GetInput(resourceLocation)[0];
            var targetArea = GetTargetArea(input);

            var MinVelocityX = 0;
            var MaxVelocityX = targetArea.x2;
            var MinVelocityY = targetArea.y1;
            var MaxVelocityY = -MinVelocityY;

            var maxYResult = int.MinValue;
            for (var velocityX = MinVelocityX; velocityX <= MaxVelocityX; velocityX++)
            {
                for (var velocityY = MinVelocityY; velocityY <= MaxVelocityY; velocityY++)
                {
                    var (x, y, vx, vy) = (0, 0, velocityX, velocityY);
                    var currentMaxY = 0;

                    while (TargetAreaCanBeReached(targetArea, x, y))
                    {
                        x += vx;
                        y += vy;
                        vy -= 1;
                        vx = Math.Max(0, vx - 1);
                        currentMaxY = Math.Max(y, currentMaxY);

                        if (TargetAreaReached(targetArea, x, y))
                        {
                            maxYResult = currentMaxY > maxYResult ? currentMaxY : maxYResult;
                            break; // break in case you hit the target twice for the same launch
                        }
                    }
                }
            }

            return $"{maxYResult}";
        }

        private static bool TargetAreaReached(Rectangle targetArea, int x, int y)
        {
            return x >= targetArea.x1 && x <= targetArea.x2 && y >= targetArea.y1 && y <= targetArea.y2;
        }

        private static bool TargetAreaCanBeReached(Rectangle targetArea, int x, int y)
        {
            return x <= targetArea.x2 && y >= targetArea.y1;
        }

        private Rectangle GetTargetArea(string input)
        {
            var coordinates = input.Replace("target area: x=", "").Replace("..",";").Replace(", y=", ";").Split(';').Select(int.Parse).ToArray();
            return new Rectangle(coordinates[0], coordinates[1], coordinates[2], coordinates[3]);
        }

        private record Rectangle(int x1, int x2, int y1, int y2);
    }
}
using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021.Solutions
{
    public class Solution202109 : ISolution
    {
        private readonly ILogger<Solution202109> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2021\\Day09.txt";

        public Solution202109(ILogger<Solution202109> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var heights = _inputService.GetInput(resourceLocation).Select(i => i.ToCharArray().Select(c => int.Parse(c.ToString())).ToArray()).ToArray();

            var mapWidth = heights[0].Length;
            var mapHeight = heights.Length;

            var lowestPoints = new List<int>();
            for(var y = 0; y < mapHeight; y++)
            {
                for(var x = 0; x < mapWidth; x++)
                {
                    var top = y - 1;
                    if (top >= 0)
                    {
                        var topValue = heights[top][x];
                        if (topValue <= heights[y][x])
                        {
                            continue;
                        }
                    }

                    var bottom = y + 1;
                    if (bottom < mapHeight)
                    {
                        var bottomValue = heights[bottom][x];
                        if (bottomValue <= heights[y][x])
                        {
                            continue;
                        }
                    }

                    var left = x - 1;
                    if (left >= 0)
                    {
                        var leftValue = heights[y][left];
                        if (leftValue <= heights[y][x])
                        {
                            continue;
                        }
                    }

                    var right = x + 1;
                    if (right < mapWidth)
                    {
                        var rightValue = heights[y][right];
                        if (rightValue <= heights[y][x])
                        {
                            continue;
                        }
                    }
                    lowestPoints.Add(heights[y][x]);
                }
            }

            return $"{lowestPoints.Sum(x => x + 1)}";
        }
    }
}
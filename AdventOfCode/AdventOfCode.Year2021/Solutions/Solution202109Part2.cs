using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021.Solutions
{
    public class Solution202109Part2 : ISolution
    {
        private readonly ILogger<Solution202109Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2021\\Day09.txt";

        public Solution202109Part2(ILogger<Solution202109Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var heights = _inputService.GetInput(resourceLocation).Select(i => i.ToCharArray().Select(c => int.Parse(c.ToString())).ToArray()).ToArray();
            var heightMap = new HeightMap(heights, heights[0].Length, heights.Length);

            var lowestPoints = GetLowPoints(heightMap);
            var basinSizes = new List<int>();
            foreach(var lowPoint in lowestPoints)
            {
                var handled = new List<string>();
                basinSizes.Add(GetBasinSize(handled, heightMap, lowPoint.Y, lowPoint.X));
            }

            return $"{basinSizes.OrderByDescending(size => size).Take(3).Aggregate(1, (acc, s) => acc * s)}";
        }

        private int GetBasinSize(List<string> handled, HeightMap heightMap, int y, int x)
        {
            var basinSize = 0;
            if (!handled.Contains($"{x};{y}") && y != -1 && x != -1 && y < heightMap.Height && x < heightMap.Width)
            {
                var point = heightMap.Heights[y][x];
                handled.Add($"{x};{y}");
                if (point != 9)
                {
                    basinSize++;
                    basinSize += GetBasinSize(handled, heightMap, y + 1, x);
                    basinSize += GetBasinSize(handled, heightMap, y - 1, x);
                    basinSize += GetBasinSize(handled, heightMap, y, x + 1);
                    basinSize += GetBasinSize(handled, heightMap, y, x - 1);
                }
            }
            return basinSize;
        }

        private record HeightMap(int[][] Heights, int Width, int Height);
        private record Point(int X, int Y, int Value);

        private List<Point> GetLowPoints(HeightMap heightMap)
        {
            var lowestPoints = new List<Point>();
            for (var y = 0; y < heightMap.Height; y++)
            {
                for (var x = 0; x < heightMap.Width; x++)
                {
                    var top = y - 1;
                    if (top >= 0)
                    {
                        var topValue = heightMap.Heights[top][x];
                        if (topValue <= heightMap.Heights[y][x])
                        {
                            continue;
                        }
                    }

                    var bottom = y + 1;
                    if (bottom < heightMap.Height)
                    {
                        var bottomValue = heightMap.Heights[bottom][x];
                        if (bottomValue <= heightMap.Heights[y][x])
                        {
                            continue;
                        }
                    }

                    var left = x - 1;
                    if (left >= 0)
                    {
                        var leftValue = heightMap.Heights[y][left];
                        if (leftValue <= heightMap.Heights[y][x])
                        {
                            continue;
                        }
                    }

                    var right = x + 1;
                    if (right < heightMap.Width)
                    {
                        var rightValue = heightMap.Heights[y][right];
                        if (rightValue <= heightMap.Heights[y][x])
                        {
                            continue;
                        }
                    }
                    lowestPoints.Add(new Point(x, y, heightMap.Heights[y][x]));
                }
            }
            return lowestPoints;
        }
    }
}
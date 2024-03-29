﻿using AdventOfCode.Domain.Interfaces;
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

        // Learned & borrowed from https://github.com/encse/adventofcode/blob/master/2021/Day15/Solution.cs
        public string GetSolution()
        {
            var input = _inputService.GetInput(resourceLocation);
            var riskMap = GetRiskMap(input);
            // Dijkstra Algo: https://www.youtube.com/watch?v=GazC3A4OQTE

            var topLeft = new Point(0, 0);
            var bottomRight = new Point(riskMap.Keys.Max(p => p.X), riskMap.Keys.Max(p => p.Y));

            var prioQueue = new PriorityQueue<Point, int>();
            var totalRiskMap = new Dictionary<Point, int>();

            totalRiskMap[topLeft] = 0;
            prioQueue.Enqueue(topLeft, 0);

            while (true)
            {
                var p = prioQueue.Dequeue();

                if (p == bottomRight)
                {
                    break;
                }

                foreach (var neighbour in p.Neighbours)
                {
                    if (riskMap.ContainsKey(neighbour))
                    {
                        var totalRiskThroughP = totalRiskMap[p] + riskMap[neighbour];
                        if (totalRiskThroughP < totalRiskMap.GetValueOrDefault(neighbour, int.MaxValue))
                        {
                            totalRiskMap[neighbour] = totalRiskThroughP;
                            prioQueue.Enqueue(neighbour, totalRiskThroughP);
                        }
                    }
                }
            }

            var result = totalRiskMap[bottomRight];
            return $"{result}";
        }

        private Dictionary<Point, int> GetRiskMap(string[] input)
        {
            var mapHeight = input.Length;
            var mapWidth = input[0].Length;
            var riskArray = input.Select(x => x.ToCharArray().Select(y => int.Parse(y.ToString())).ToArray()).ToArray();
            var map = new Dictionary<Point, int>();
            for(var y = 0; y < mapHeight; y++)
            {
                for(var x =0; x < mapWidth; x++)
                {
                    map.Add(new Point(x, y), riskArray[y][x]);
                }
            }
            return map;
        }
        private record Point(int X, int Y)
        {
            public Point[] Neighbours =>
                new[] {
                    new Point(X-1, Y),
                    new Point(X+1, Y),
                    new Point(X, Y-1),
                    new Point(X, Y+1),
                };
        };
    }
}
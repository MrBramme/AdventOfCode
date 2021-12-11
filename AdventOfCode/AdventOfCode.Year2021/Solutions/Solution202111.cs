using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021.Solutions
{
    public class Solution202111 : ISolution
    {
        private readonly ILogger<Solution202111> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2021\\Day11.txt";

        public Solution202111(ILogger<Solution202111> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var grid = _inputService.GetInput(resourceLocation).Select(i => i.ToCharArray().Select(c => int.Parse(c.ToString())).ToArray()).ToArray();
            var gridWidth = grid[0].Length;
            var gridHeight = grid.Length;
            var lastPoint = new Point(gridWidth - 1, gridHeight - 1);

            var octopusses = new List<Octopus>();
            for(var y = 0; y < gridHeight; y++)
            {
                for(var x = 0; x < gridWidth; x++)
                {
                    octopusses.Add(new Octopus(new Point(x, y), grid[y][x], lastPoint));
                }
            }

            for(var step = 1; step <= 100; step++)
            {
                octopusses.ForEach(o => o.StartStep(step));
                var neighboursToFlash = new List<Point>();
                octopusses.ForEach(o => neighboursToFlash.AddRange(o.Flash(0)));
                while (neighboursToFlash.Any())
                {
                    var nextCycle = new List<Point>();
                    foreach(var neighbour in neighboursToFlash)
                    {
                        nextCycle.AddRange(octopusses.First(o => neighbour.X == o.Position.X && neighbour.Y == o.Position.Y).Flash(1));
                    }
                    neighboursToFlash = nextCycle;
                }
                octopusses.ForEach(o => o.FinalStage());
            }

            return $"{octopusses.Sum(o => o.FlashCount)}";
        }

        private class Octopus
        {
            public int Energy;
            public Point Position;
            private List<Point> _neighbours;
            public bool IsFlash => Energy > 9 && _lastFlashStep != _stepCount;
            public int FlashCount = 0;
            private int _stepCount;
            private int _lastFlashStep;

            public Octopus(Point position, int energy, Point LastPoint)
            {
                Position = position;
                Energy = energy;
                _neighbours = GetNeighbours(position, LastPoint);
            }

            private List<Point> GetNeighbours(Point point, Point LastPoint)
            {
                var neighbours = new List<Point>();
                for(var x = -1; x <= 1; x++)
                {
                    for(var y = -1; y <= 1; y++)
                    {
                        if (!(x == 0 && y == 0))
                        {
                            var neighbour = new Point(point.X + x, point.Y + y);
                            if (neighbour.X >= 0 && neighbour.Y >= 0 && neighbour.X <= LastPoint.X && neighbour.Y <= LastPoint.Y)
                            {
                                neighbours.Add(neighbour);
                            }
                        }
                    }
                }
                return neighbours;
            }

            public void StartStep(int stepCount)
            {
                _stepCount = stepCount;
                Energy++;
            }

            public List<Point> Flash(int energyIncrease)
            {
                Energy += energyIncrease;
                if (IsFlash)
                {
                    _lastFlashStep = _stepCount;
                    FlashCount++;
                    return _neighbours;
                }
                return new List<Point>(0);
            }

            public void FinalStage()
            {
                if(Energy > 9)
                {
                    Energy = 0;
                }
            }
        }

        private record Point (int X, int Y);
    }
}
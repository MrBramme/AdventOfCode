using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021.Solutions
{
    public class Solution202105Part2 : ISolution
    {
        private readonly ILogger<Solution202105Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2021\\Day05.txt";

        public Solution202105Part2(ILogger<Solution202105Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var input = _inputService.GetInput(resourceLocation).ToList();
            var lines = input.Select(i => new Line(i)).Where(l => l.IsValid());

            // DebugPrintGrid(lines);

            var grid = new Dictionary<Point, int>();
            foreach (var line in lines)
            {
                var coordinates = line.GetLineCoordinates();
                foreach(var coordinate in coordinates)
                {
                    if (grid.ContainsKey(coordinate)) {
                        grid[coordinate]++;
                    } else
                    {
                        grid.Add(coordinate, 1);
                    }
                }
            }

            var higherCount = grid.Values.Count(x => x > 1);
            return $"{higherCount}"; //19544 too low
        }

        private void DebugPrintGrid(IEnumerable<Line> lines)
        {
            var coordinates = lines.Select(x => x.GetLineCoordinates()).SelectMany(x => x);
            var maxX = coordinates.Select(c => c.x).Max();
            var maxY = coordinates.Select(c => c.y).Max();

            for(var y = 0; y <= maxY; y++)
            {
                var row = "";
                for (var x = 0; x <= maxX; x++)
                {
                    var cnt = coordinates.Count(p => p.x == x && p.y == y);
                    var result = cnt == 0 ? "." : $"{cnt}";
                    row = $"{row}{result}";
                }
                Console.WriteLine(row);
            }
        }
        private class Line
        {
            private Point Start { get; set; }
            private Point End { get; set; }
            public Line(string input)
            {
                var coordinates = input.Replace(" -> ", ";").Split(";");
                var startCoordinates = coordinates[0].Split(",").Select(int.Parse);
                var endCoordinates = coordinates[1].Split(",").Select(int.Parse);
                Start = new Point(startCoordinates.First(), startCoordinates.Last());
                End = new Point(endCoordinates.First(), endCoordinates.Last());
            }

            public bool IsValid()
            {
                var diagCheck = Math.Abs(Start.x - End.x) == Math.Abs(Start.y - End.y);
                return Start.x == End.x || Start.y == End.y | diagCheck;
            }

            public IEnumerable<Point> GetLineCoordinates()
            {
                var isHorizontal = Start.y == End.y;
                if (isHorizontal)
                {
                    var range = GetOrdered(Start.x, End.x);
                    var cnt = (range.highest - range.lowest) + 1;
                    return Enumerable.Range(range.lowest, cnt).Select(i => new Point(i, Start.y));
                }
                var isVertical = Start.x == End.x;
                if (isVertical)
                {
                    var range = GetOrdered(Start.y, End.y);
                    var cnt = (range.highest - range.lowest) + 1;
                    return Enumerable.Range(range.lowest, cnt).Select(i => new Point(Start.x, i));
                }
                var directionX = Start.x > End.x ? -1 : 1;
                var directionY = Start.y > End.y ? -1 : 1;
                var currentPos = Start;
                var lines = new List<Point>();
                while(currentPos != End)
                {
                    lines.Add(currentPos);
                    currentPos = new Point(currentPos.x + directionX, currentPos.y + directionY);
                }
                lines.Add(currentPos);
                return lines;
            }

            private (int highest, int lowest) GetOrdered(int a, int b)
            {
                if (a < b) return (b, a);
                return (a, b);
            }
        }

        record Point(int x, int y);
    }
}
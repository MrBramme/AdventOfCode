using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Drawing;

namespace AdventOfCode.Year2023.Solutions
{
    public class Solution202303Part2 : ISolution
    {
        private readonly ILogger<Solution202303Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2023\\Day03.txt";

        public Solution202303Part2(ILogger<Solution202303Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }
        public string GetSolution()
        {
            var values = _inputService.GetInput(resourceLocation).Select(x => x.ToCharArray()).ToArray();
            var width = values[0].Length;
            var height = values.Length;
            var (gears, partNumbers) = GetCoordinatesOfGearsAndPartNumbers(width, height, values);
            var gearRatios = new List<int>();
            foreach (var gear in gears)
            {
                var foundParts = partNumbers.Where(pn => pn.GetSurroundingPoints().Contains(gear)).ToList();
                if (foundParts.Count == 2)
                {
                    gearRatios.Add(foundParts[0].GetNumber * foundParts[1].GetNumber);
                }
            }

            return $"{gearRatios.Sum()}";
        }

        private static (List<Point> gears, List<PartNumber> partNumbers) GetCoordinatesOfGearsAndPartNumbers(int width, int height, char[][] values)
        {
            var coordinatesOfGears = new List<Point>();
            var partNumbers = new List<PartNumber>();
            PartNumber partNumber = null;

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    if (char.IsDigit(values[y][x]))
                    {
                        if (partNumber == null)
                        {
                            partNumber = new PartNumber();
                        }
                        partNumber.Coordinates.Add(new Point(x, y));
                        partNumber.Characters.Add(values[y][x]);
                    }
                    else
                    {
                        if (values[y][x] == '*')
                        {
                            coordinatesOfGears.Add(new Point(x, y));
                        }

                        if (partNumber != null)
                        {
                            partNumbers.Add(partNumber);
                            partNumber = null;
                        }
                    }
                }
                if (partNumber != null)
                {
                    partNumbers.Add(partNumber);
                    partNumber = null;
                }
            }

            if (partNumber != null)
            {
                partNumbers.Add(partNumber);
            }

            return (coordinatesOfGears, partNumbers);
        }

        class PartNumber
        {
            public List<Point> Coordinates = new List<Point>();
            public List<char> Characters = new List<char>();
            public int GetNumber => int.Parse(string.Concat(Characters));

            private List<Point> _surroundingPoints;
            public List<Point> GetSurroundingPoints()
            {
                if (_surroundingPoints != null) { return _surroundingPoints; }
                _surroundingPoints = new List<Point>();
                var firstCoordinate = Coordinates.First();
                var lastCoordinate = Coordinates.Last();
                var leftEdge = firstCoordinate.X - 1;
                var rightEdge = lastCoordinate.X + 1;

                _surroundingPoints.AddRange(Enumerable.Range(leftEdge, (rightEdge - leftEdge) + 1).Select(x => new Point(x, firstCoordinate.Y - 1)));
                _surroundingPoints.Add(new Point(leftEdge, firstCoordinate.Y));
                _surroundingPoints.Add(new Point(rightEdge, firstCoordinate.Y));
                _surroundingPoints.AddRange(Enumerable.Range(leftEdge, (rightEdge - leftEdge) + 1).Select(x => new Point(x, lastCoordinate.Y + 1)));
                return _surroundingPoints;
            }
        }
    }
}
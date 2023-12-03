using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Drawing;

namespace AdventOfCode.Year2023.Solutions
{
    public class Solution202303 : ISolution
    {
        private readonly ILogger<Solution202303> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2023\\Day03.txt";

        public Solution202303(ILogger<Solution202303> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }
        public string GetSolution()
        {
            var values = _inputService.GetInput(resourceLocation).Select(x => x.ToCharArray()).ToArray();
            var width = values[0].Length;
            var height = values.Length;
            var (symbols, partNumbers) = GetCoordinatesOfSymbolsAndPartNumbers(width, height, values);
            var validPartNumbers = new List<int>();
            foreach (var symbol in symbols)
            {
                foreach (var foundPart in partNumbers.Where(pn => !pn.IsTaken && pn.GetSurroundingPoints().Contains(symbol)))
                {
                    validPartNumbers.Add(foundPart.GetNumber);
                    foundPart.IsTaken = true;
                }
            }

            return $"{validPartNumbers.Sum()}";
        }

        private static (List<Point> symbols, List<PartNumber> partNumbers) GetCoordinatesOfSymbolsAndPartNumbers(int width, int height, char[][] values)
        {
            var coordinatesOfSymbols = new List<Point>();
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
                        if (values[y][x] != '.')
                        {
                            coordinatesOfSymbols.Add(new Point(x, y));
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

            return (coordinatesOfSymbols, partNumbers);
        }

        class PartNumber
        {
            public bool IsTaken { get; set; }
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
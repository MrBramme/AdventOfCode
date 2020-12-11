using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
namespace AdventOfCode.Year2020.Solutions
{
    public class Solution202011 : ISolution
    {
        private readonly ILogger<Solution202011> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2020\\Day11.txt";
        public Solution202011(ILogger<Solution202011> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }
        public string GetSolution()
        {
            var inputData = _inputService.GetInput(resourceLocation);
            var spots = PrepareInput(inputData);
            var isDone = false;
            while (!isDone)
            {
                var newSpots = Shuffle(spots);
                var newSet = newSpots.SelectMany(row => row.Select(col => col.IsOccupied));
                var oldSet = spots.SelectMany(row => row.Select(col => col.IsOccupied));
                isDone = newSet.SequenceEqual(oldSet);
                spots = newSpots;
            }
            var result = spots.Sum(row => row.Count(col => col.IsOccupied));
            return $"{result}";
        }
        private Spot[][] Shuffle(Spot[][] input)
        {
            var rowCount = input.Length;
            var colCount = input[0].Length;
            var result = new Spot[rowCount][];
            for (var row = 0; row < rowCount; row++)
            {
                result[row] = new Spot[colCount];
            }
            for (var row = 0; row < rowCount; row++)
            {
                for (var col = 0; col < colCount; col++)
                {
                    var currentSpot = input[row][col];
                    result[row][col] = currentSpot.GetCopy();
                    if (currentSpot.IsSeat)
                    {

                        var adjacentSpots = GetAdjacentSpots(input, col, colCount, row, rowCount);
                        var occupiedSeats = adjacentSpots.Count(seat => seat.IsOccupied);
                        if (currentSpot.IsOccupied && occupiedSeats >= 4)
                        {
                            result[row][col].IsOccupied = false;
                        }
                        if (!currentSpot.IsOccupied && occupiedSeats == 0)
                        {
                            result[row][col].IsOccupied = true;
                        }
                    }
                }
            }
            return result;
        }
        private static List<Spot> GetAdjacentSpots(Spot[][] input, int col, int colCount, int row, int rowCount)
        {
            var adjacentSpots = new List<Spot>();
            var prevColIsValid = col - 1 >= 0;
            var nextColIsValid = col + 1 < colCount;
            var prevRowIsValid = row - 1 >= 0;
            var nextRowIsValid = row + 1 < rowCount;
            if (prevColIsValid)
            {
                adjacentSpots.Add(input[row][col - 1]);
            }
            if (nextColIsValid)
            {
                adjacentSpots.Add(input[row][col + 1]);
            }
            if (prevRowIsValid)
            {
                if (prevColIsValid)
                {
                    adjacentSpots.Add(input[row - 1][col - 1]);
                }
                adjacentSpots.Add(input[row - 1][col]);
                if (nextColIsValid)
                {
                    adjacentSpots.Add(input[row - 1][col + 1]);
                }
            }
            if (nextRowIsValid)
            {
                if (prevColIsValid)
                {
                    adjacentSpots.Add(input[row + 1][col - 1]);
                }
                adjacentSpots.Add(input[row + 1][col]);
                if (nextColIsValid)
                {
                    adjacentSpots.Add(input[row + 1][col + 1]);
                }
            }
            return adjacentSpots;
        }
        private static Spot[][] PrepareInput(string[] inputData)
        {
            var result = new Spot[inputData.Length][];
            for (var row = 0; row < inputData.Length; row++)
            {
                var charRow = inputData[row].ToCharArray();
                var rowResult = new Spot[charRow.Length];
                for (var col = 0; col < charRow.Length; col++)
                {
                    rowResult[col] = new Spot(row, col, charRow[col]);
                }
                result[row] = rowResult;
            }
            return result;
        }
        private class Spot
        {
            public Spot(int row, int col, char spot)
            {
                Row = row;
                Col = col;
                IsSeat = !spot.Equals('.');
                IsOccupied = false;
            }
            public Spot(int row, int col, bool isSeat, bool isOccupied)
            {
                Row = row;
                Col = col;
                IsSeat = isSeat;
                IsOccupied = isOccupied;
            }
            public int Row { get; }
            public int Col { get; }
            public bool IsSeat { get; }
            public bool IsOccupied { get; set; }
            public Spot GetCopy()
            {
                return new Spot(Row, Col, IsSeat, IsOccupied);
            }
        }
    }
}
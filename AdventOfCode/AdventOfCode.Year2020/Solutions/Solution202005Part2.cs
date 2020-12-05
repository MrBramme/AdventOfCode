using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2020.Solutions
{
    public class Solution202005Part2 : ISolution
    {
        private readonly ILogger<Solution202005Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2020\\Day05.txt";

        public Solution202005Part2(ILogger<Solution202005Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }
        public string GetSolution()
        {
            var inputData = _inputService.GetInput(resourceLocation);
            var takenSeats = new List<Seat>();
            foreach (var input in inputData)
            {
                var rowSeatFinder = new SeatFinder('F', 0, 127);
                var rowSection = input.Substring(0, 7);
                var row = rowSeatFinder.Handle(rowSection.ToCharArray());
                var seatFinder = new SeatFinder('L', 0, 7);
                var columnSection = input.Substring(7, 3);
                var column = seatFinder.Handle(columnSection.ToCharArray());
                var seatId = row * 8 + column;
                takenSeats.Add(new Seat(seatId, row, column));
            }

            var sortedSeatsWithoutFirstAndLastRow = takenSeats.Where(y => y.Row > 0 && y.Row < 127).OrderBy(y => y.SeatId).ThenBy(y => y.Column).ToArray();
            var nextSeat = sortedSeatsWithoutFirstAndLastRow.Where((s, i) => i != 0 && sortedSeatsWithoutFirstAndLastRow[i - 1].SeatId == s.SeatId - 2).Single();
            return $"{nextSeat.SeatId - 1}";
        }

        class Seat
        {
            public Seat(int seatId, int row, int column)
            {
                SeatId = seatId;
                Row = row;
                Column = column;
            }
            public int SeatId { get; }
            public int Row { get; }
            public int Column { get; }
        }

        class SeatFinder
        {
            private readonly char _firstHalfIdentifier;
            private readonly int _min;
            private readonly int _max;

            public SeatFinder(char firstHalfIdentifier, int min, int max)
            {
                _firstHalfIdentifier = firstHalfIdentifier;
                _min = min;
                _max = max;
            }

            public int Handle(char[] input)
            {
                bool isFirstHalf = input[0] == _firstHalfIdentifier;
                if (_min + 1 == _max)
                {
                    return isFirstHalf ? _min : _max;
                }

                var midPoint = _min + (_max - _min + 1) / 2;
                var seatFinder = new SeatFinder(_firstHalfIdentifier, isFirstHalf ? _min : midPoint, isFirstHalf ? midPoint - 1 : _max);
                return seatFinder.Handle(input.Skip(1).ToArray());
            }
        }
    }
}
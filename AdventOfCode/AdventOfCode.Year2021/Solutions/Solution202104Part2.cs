using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021.Solutions
{
    public class Solution202104Part2 : ISolution
    {
        private readonly ILogger<Solution202104Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2021\\Day04.txt";

        public Solution202104Part2(ILogger<Solution202104Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var input = _inputService.GetInput(resourceLocation).ToArray();

            var numberDraws= input[0].Split(',').Select(int.Parse).ToArray();

            var bingoFields = new List<BingoField>();
            for(var i = 2; i < input.Length; i += 6)
            {
                bingoFields.Add(new BingoField(input[(i)..(i + 5)]));
            }

            numberDraws.Take(5).ToList().ForEach(nr => bingoFields.ForEach(f => f.Mark(nr)));
            
            var drawNrIndex = 4;
            BingoField losingField = null;
            while (losingField == null)
            {
                drawNrIndex++;
                var newNumber = numberDraws[drawNrIndex];
                bingoFields.ForEach(f => f.Mark(newNumber));
                var losingFields = bingoFields.Where(b => !b.CheckForBingo());
                
                if (losingFields.Count() == 1)
                {
                     losingField = losingFields.FirstOrDefault();
                }
            }

            var isBingo = false;
            while (!isBingo)
            {
                drawNrIndex++;
                var newNumber = numberDraws[drawNrIndex];
                losingField.Mark(newNumber);
                isBingo = losingField.CheckForBingo();
            }

            var sumOfUnmarked = losingField.GetUnmarked().Sum();
            return $"{sumOfUnmarked * numberDraws[drawNrIndex]}";
        }

        private class BingoField
        {
            private List<BingoNumber> Numbers = new List<BingoNumber>();

            public BingoField(string[] fieldData)
            {
                var rowNr = 0;
                foreach (var fieldRow in fieldData)
                {
                    var colNr = 0;
                    var cols = fieldRow.Trim().Replace("  ", " ").Split(' ').Select(int.Parse);
                    foreach(var colData in cols)
                    {
                        Numbers.Add(new BingoNumber(colData, rowNr, colNr));
                        colNr++;
                    }
                    rowNr++;
                }
            }

            public void Mark(int nrToMark)
            {
                foreach(var nr in Numbers)
                {
                    if (nr.Number == nrToMark) nr.IsMarked = true;
                }
            }

            public bool CheckForBingo()
            {
                var markedNumbers = Numbers.Where(n => n.IsMarked).ToList();
                var gridSize = 5;

                var rowMarkedCounts = Enumerable.Range(0, gridSize - 1).Select(i => Numbers.Where(n => n.Row == i).Count(n => n.IsMarked));
                if (rowMarkedCounts.Any(n => n == 5)) return true;

                var colMarkedCounts = Enumerable.Range(0, gridSize - 1).Select(i => Numbers.Where(n => n.Col == i).Count(n => n.IsMarked));
                if (colMarkedCounts.Any(n => n == 5)) return true;
                return false;
            }

            public IEnumerable<int> GetUnmarked()
            {
                return Numbers.Where(n => !n.IsMarked).Select(n => n.Number);
            }
        }

        private class BingoNumber
        {
            public BingoNumber(int number, int row, int col)
            {
                Number = number;
                Row = row;
                Col = col;
            }
            public int Number { get; }
            public bool IsMarked { get; set; }
            public int Row { get; }
            public int Col { get; }
        }
    }
}
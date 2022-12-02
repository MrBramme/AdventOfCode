using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AdventOfCode.Year2022.Solutions
{
    public class Solution202202Part2 : ISolution
    {
        private readonly ILogger<Solution202202Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2022\\Day02.txt";

        public Solution202202Part2(ILogger<Solution202202Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        // 0 lose X, 3 draw Y, 6 win Z
        // PLUS
        // 1 rock A, 2 paper B, 3 scissor C
        public string GetSolution()
        {
            var games = _inputService.GetInput(resourceLocation).Select(game =>
            {
                var input = game.Split(' ').ToArray();
                var (hand1, outcome) = (input[0], input[1]);

                return outcome switch
                {
                    "X" => GetLosingHandPoints(hand1), // lose
                    "Y" => 3 + GetDrawHandPoints(hand1), // draw
                    "Z" => 6 + GetWinningHandPoints(hand1), // win
                    _ => throw new ArgumentOutOfRangeException()
                };
            });
            return $"{games.Sum()}";
        }

        private int GetLosingHandPoints(string opponent)
        {
            switch (opponent)
            {
                case "A":
                    return 3;
                case "B":
                    return 1;
                case "C":
                    return 2;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private int GetWinningHandPoints(string opponent)
        {
            switch (opponent)
            {
                case "A":
                    return 2;
                case "B":
                    return 3;
                case "C":
                    return 1;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private int GetDrawHandPoints(string opponent)
        {
            switch (opponent)
            {
                case "A":
                    return 1;
                case "B":
                    return 2;
                case "C":
                    return 3;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AdventOfCode.Year2022.Solutions
{
    public class Solution202202 : ISolution
    {
        private readonly ILogger<Solution202202> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2022\\Day02.txt";

        public Solution202202(ILogger<Solution202202> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var games = _inputService.GetInput(resourceLocation).Select(game =>
            {
                var input = game.Split(' ').Select(ConvertToHandPoints).ToArray();
                var (hand1, hand2) = (input[0], input[1]);
                if (hand1 == hand2) // draw
                {
                    return 3 + hand2;
                }

                if ((hand2 == 1 && hand1 == 3) ||
                    (hand2 == 2 && hand1 == 1) ||
                    (hand2 == 3 && hand1 == 2)
                    ) // win
                {
                    return 6 + hand2;
                }

                return hand2;

            });
            return $"{games.Sum()}";
        }

        private int ConvertToHandPoints(string hand)
        {
            switch (hand)
            {
                case "A":
                case "X":
                    return 1;
                case "B":
                case "Y":
                    return 2;
                case "C":
                case "Z":
                    return 3;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
namespace AdventOfCode.Year2020.Solutions
{
    public class Solution202015Part2 : ISolution
    {
        private readonly ILogger<Solution202015Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2020\\Day15.txt";
        public Solution202015Part2(ILogger<Solution202015Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }
        public string GetSolution()
        {
            var gameData = _inputService
                .GetInput(resourceLocation)[0]
                .Split(',')
                .Select((value, index) => (long.Parse(value), index + 1))
                .ToDictionary(x => x.Item1, x => (x.Item2, -1));

            var lastNumber = gameData.Last().Key;

            for (var turn = gameData.Count + 1; turn <= 30000000; turn++)
            {
                if (gameData.ContainsKey(lastNumber))
                {
                    lastNumber = GetLastNumber(gameData, lastNumber);

                    if (gameData.ContainsKey(lastNumber))
                    {
                        gameData[lastNumber] = (turn, gameData[lastNumber].Item1);
                    }
                    else
                    {
                        gameData.Add(lastNumber, (turn, -1));
                    }
                }

            }

            return $"{lastNumber}";
        }

        private static long GetLastNumber(Dictionary<long, (int, int)> gameData, long lastNumber)
        {
            return gameData[lastNumber].Item2 == -1 ? 0 : gameData[lastNumber].Item1 - gameData[lastNumber].Item2;
        }
    }
}
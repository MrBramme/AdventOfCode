﻿using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2023.Solutions
{
    public class Solution202302Part2 : ISolution
    {
        private readonly ILogger<Solution202302Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2023\\Day02.txt";

        public Solution202302Part2(ILogger<Solution202302Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }
        public string GetSolution()
        {
            var values = _inputService.GetInput(resourceLocation).ToList();
            var games = ParseGames(values);
            return $"{games.Sum(x => x.maxPerCube["red"] * x.maxPerCube["blue"] * x.maxPerCube["green"])}";
        }

        private List<Game> ParseGames(List<string> input)
        {
            var result = new List<Game>();
            var regex = new Regex(@"(\d+)\s+(\w+)");
            foreach (var value in input)
            {
                (int gameId, string[] gameInput) = (int.Parse(value.Split(":")[0].Replace("Game ", "")), value.Split(":")[1].Split(";"));
                var game = new Game(gameId)
                {
                    maxPerCube = new Dictionary<string, int>
                    {
                        {"red", 0},
                        {"blue", 0},
                        {"green", 0}
                    }
                };
                foreach (var singleGame in gameInput)
                {
                    var cubeInput = singleGame.Split(",");
                    foreach (var cubeValue in cubeInput)
                    {
                        var matches = regex.Matches(cubeValue);

                        foreach (Match match in matches)
                        {
                            var numberOfCubes = int.Parse(match.Groups[1].Value);
                            var cubeColor = match.Groups[2].Value;

                            if (game.maxPerCube[cubeColor] < numberOfCubes)
                            {
                                game.maxPerCube[cubeColor] = numberOfCubes;
                            }
                        }
                    }
                }
                result.Add(game);
            }
            return result;
        }

        class Game(int id)
        {
            public int Id { get; } = id;
            public Dictionary<string, int> maxPerCube { get; init; }
        }
    }
}
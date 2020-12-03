using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;

namespace AdventOfCode.Year2020.Solutions
{
    public class Solution202003 : ISolution
    {
        private readonly ILogger<Solution202003> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2020\\Day03.txt";

        public Solution202003(ILogger<Solution202003> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }
        public string GetSolution()
        {
            var slopeSection = _inputService.GetInput(resourceLocation);
            var currentPosition = 0;
            var movement = new Movement { Right = 3, Down = 1 };

            var slopeLength = slopeSection.Length;
            var slopeWidth = slopeSection[0].Length;
            var stepsDown = (slopeLength - 1) / movement.Down;
            decimal requiredWidth = movement.Right * stepsDown;

            var requiredSlopeSections = (int)Math.Ceiling(requiredWidth / slopeWidth);
            var newSlope = new char[slopeLength * requiredSlopeSections * slopeWidth];
            var totalSlopeWidth = slopeWidth * requiredSlopeSections;
            for (int i = 0; i < slopeLength; i++)
            {
                var currentRow = slopeSection[i].ToCharArray();
                var newSlopePosition = i * totalSlopeWidth;
                for (int j = 0; j < requiredSlopeSections; j++)
                {
                    foreach (var position in currentRow)
                    {
                        newSlope[newSlopePosition] = position;
                        newSlopePosition++;
                    }
                }
            }

            var result = 0;
            for (int i = 0; i < stepsDown; i++)
            {
                currentPosition = currentPosition + movement.Right + (totalSlopeWidth * movement.Down);
                result += newSlope[currentPosition] == '.' ? 0 : 1;
            }
            return $"{result}";
        }
    }

    class Movement
    {
        public int Right { get; set; }
        public int Down { get; set; }
    }
}
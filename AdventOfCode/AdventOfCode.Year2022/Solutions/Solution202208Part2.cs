using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AdventOfCode.Year2022.Solutions
{
    public class Solution202208Part2 : ISolution
    {
        private readonly ILogger<Solution202208Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2022/Day08.txt";

        public Solution202208Part2(ILogger<Solution202208Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var input = _inputService.GetInput(resourceLocation).Select(x => x.ToArray().Select(y => int.Parse(y.ToString())).ToArray()).ToArray();
            var transposedInput = GetTransposedMatrix(input);

            var (gridWidth, gridHeight) = (input[0].Length, input.Length);

            var result = -1;

            for (var x = 1; x < gridWidth - 1; x++)
            {
                for (var y = 1; y < gridHeight - 1; y++)
                {
                    var topDistance = transposedInput[x].Take(y).Reverse().TakeWhile(tree => tree < input[y][x]).Count();
                    topDistance = topDistance == y ? topDistance : topDistance + 1;

                    var bottomDistance = transposedInput[x].Skip(y + 1).TakeWhile(tree => tree < input[y][x]).Count();
                    bottomDistance = bottomDistance == gridHeight - (y + 1) ? bottomDistance : bottomDistance + 1;

                    var leftDistance = input[y].Take(x).Reverse().TakeWhile(tree => tree < input[y][x]).Count();
                    leftDistance = leftDistance == x ? leftDistance : leftDistance + 1;

                    var rightDistance = input[y].Skip(x + 1).TakeWhile(tree => tree < input[y][x]).Count();
                    rightDistance = rightDistance == gridWidth - (x + 1) ? rightDistance : rightDistance + 1;

                    var scenicScore = topDistance * bottomDistance * leftDistance * rightDistance;
                    if (scenicScore > result)
                    {
                        result = scenicScore;
                    }
                }
            }

            return $"{result}";
        }

        private int[][] GetTransposedMatrix(IReadOnlyList<int[]> input)
        {
            var (gridWidth, gridHeight) = (input[0].Length, input.Count);
            var transposed = new int[gridWidth][];
            for (var y = 0; y < gridWidth; y++)
            {
                transposed[y] = new int[gridHeight];
                for (var x = 0; x < gridHeight; x++)
                {
                    transposed[y][x] = input[x][y];
                }
            }

            return transposed;
        }
    }
}
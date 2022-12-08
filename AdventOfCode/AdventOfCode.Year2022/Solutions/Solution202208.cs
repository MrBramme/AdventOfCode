using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AdventOfCode.Year2022.Solutions
{
    public class Solution202208 : ISolution
    {
        private readonly ILogger<Solution202208> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2022/Day08.txt";

        public Solution202208(ILogger<Solution202208> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var input = _inputService.GetInput(resourceLocation).Select(x => x.ToArray().Select(y => int.Parse(y.ToString())).ToArray()).ToArray();
            var transposedInput = GetTransposedMatrix(input);

            var (gridWidth, gridHeight) = (input[0].Length, input.Length);

            var result = (gridWidth * 2) + ((gridHeight - 2) * 2); // outer trees

            for (var x = 1; x < gridWidth - 1; x++)
            {
                for (var y = 1; y < gridHeight - 1; y++)
                {
                    var top = transposedInput[x].Take(y).Max();
                    if (top < input[y][x])
                    {
                        result++;
                        continue;
                    }

                    var bottom = transposedInput[x].Skip(y + 1).Max();
                    if (bottom < input[y][x])
                    {
                        result++;
                        continue;
                    }

                    var left = input[y].Take(x).Max();
                    if (left < input[y][x])
                    {
                        result++;
                        continue;
                    }

                    var right = input[y].Skip(x + 1).Max();
                    if (right < input[y][x])
                    {
                        result++;
                        continue;
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
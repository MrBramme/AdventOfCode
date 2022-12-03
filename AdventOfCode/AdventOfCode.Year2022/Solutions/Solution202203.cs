using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AdventOfCode.Year2022.Solutions
{
    public class Solution202203 : ISolution
    {
        private readonly ILogger<Solution202203> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2022\\Day03.txt";

        public Solution202203(ILogger<Solution202203> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var input = _inputService.GetInput(resourceLocation).Select(x => x.ToCharArray()).ToList();
            var result = 0;
            foreach (var backpack in input)
            {
                var backpackSize = backpack.Length;
                var compartment1 = backpack.Take(backpackSize / 2);
                var compartment2 = backpack.Skip(backpackSize / 2);
                var duplicate = compartment1.First(x => compartment2.Contains(x));
                result += Priorities[duplicate];
            }

            return $"{result}";
        }

        private Dictionary<char, int> Priorities = new Dictionary<char, int>
        {
            {'a', 1},
            {'b', 2},
            {'c', 3},
            {'d', 4},
            {'e', 5},
            {'f', 6},
            {'g', 7},
            {'h', 8},
            {'i', 9},
            {'j', 10},
            {'k', 11},
            {'l', 12},
            {'m', 13},
            {'n', 14},
            {'o', 15},
            {'p', 16},
            {'q', 17},
            {'r', 18},
            {'s', 19},
            {'t', 20},
            {'u', 21},
            {'v', 22},
            {'w', 23},
            {'x', 24},
            {'y', 25},
            {'z', 26},
            {'A', 27},
            {'B', 28},
            {'C', 29},
            {'D', 30},
            {'E', 31},
            {'F', 32},
            {'G', 33},
            {'H', 34},
            {'I', 35},
            {'J', 36},
            {'K', 37},
            {'L', 38},
            {'M', 39},
            {'N', 40},
            {'O', 41},
            {'P', 42},
            {'Q', 43},
            {'R', 44},
            {'S', 45},
            {'T', 46},
            {'U', 47},
            {'V', 48},
            {'W', 49},
            {'X', 50},
            {'Y', 51},
            {'Z', 52}
        };
    }
}
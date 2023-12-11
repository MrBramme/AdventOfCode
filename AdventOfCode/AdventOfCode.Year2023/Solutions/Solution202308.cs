using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AdventOfCode.Year2023.Solutions;

public class Solution202308 : ISolution
{
    private readonly ILogger<Solution202308> _logger;
    private readonly IInputService _inputService;
    private readonly string resourceLocation = "Resources2023\\Day08.txt";

    public Solution202308(ILogger<Solution202308> logger, IInputService inputService)
    {
        _logger = logger;
        _inputService = inputService;
    }
    public string GetSolution()
    {
        var values = _inputService.GetInput(resourceLocation);

        var instructions = values[0].ToCharArray();
        var map = new Dictionary<string, string[]>();
        for (var i = 2; i < values.Length; i++)
        {
            var node = values[i].Substring(0, 3);
            var left = values[i].Substring(7, 3);
            var right = values[i].Substring(12, 3);
            map.Add(node, new[] { left, right });
        }

        var result = 0;
        var currentNode = "AAA";
        var indexTracker = new IndexTracker(0, instructions.Length - 1);
        while (currentNode != "ZZZ")
        {
            result++;
            var instruction = instructions[indexTracker.GetNextIndex()];
            currentNode = instruction switch
            {
                'L' => map[currentNode][0],
                'R' => map[currentNode][1],
                _ => throw new ArgumentOutOfRangeException()
            };
        }


        return $"{result}";
    }

    private class IndexTracker(int minValue, int maxValue)
    {
        private int _index = minValue - 1;

        public int GetNextIndex()
        {
            _index = (_index + 1) % (maxValue - minValue + 1);
            return _index;
        }
    }
}

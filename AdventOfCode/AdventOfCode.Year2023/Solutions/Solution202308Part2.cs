using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AdventOfCode.Year2023.Solutions;

public class Solution202308Part2 : ISolution
{
    private readonly ILogger<Solution202308Part2> _logger;
    private readonly IInputService _inputService;
    private readonly string resourceLocation = "Resources2023\\Day08.txt";

    public Solution202308Part2(ILogger<Solution202308Part2> logger, IInputService inputService)
    {
        _logger = logger;
        _inputService = inputService;
    }
    public string GetSolution()
    {
        var values = _inputService.GetInput(resourceLocation);

        throw new NotImplementedException("not working fast enought");

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
        var currentNodes = map.Keys.Where(x => x.EndsWith('A')).ToArray();
        var indexTracker = new IndexTracker(0, instructions.Length - 1);
        while (!currentNodes.All(x => x.EndsWith('Z')))
        {
            result++;
            var instruction = instructions[indexTracker.GetNextIndex()];
            for (var nodeIndex = 0; nodeIndex < currentNodes.Length; nodeIndex++)
            {
                currentNodes[nodeIndex] = instruction switch
                {
                    'L' => map[currentNodes[nodeIndex]][0],
                    'R' => map[currentNodes[nodeIndex]][1],
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
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

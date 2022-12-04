using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AdventOfCode.Year2022.Solutions
{
    public class Solution202204Part2 : ISolution
    {
        private readonly ILogger<Solution202204Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2022\\Day04.txt";

        public Solution202204Part2(ILogger<Solution202204Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var input = _inputService.GetInput(resourceLocation).Select(x =>
            {
                var ids = x.Split(',');
                var firstIdSet = ids[0].Split("-").Select(int.Parse).ToArray();
                var secondIdSet = ids[1].Split("-").Select(int.Parse).ToArray();
                return (new IdSet(firstIdSet[0], firstIdSet[1]), new IdSet(secondIdSet[0], secondIdSet[1]));
            }).ToList();

            var result = 0;
            foreach (var workPairs in input)
            {
                if (workPairs.Item1.GetSequence.Union(workPairs.Item2.GetSequence).Distinct().Count() != workPairs.Item1.GetCountOfIds + workPairs.Item2.GetCountOfIds)
                {
                    result++;
                }
            }
            return $"{result}";
        }

        private record IdSet(int Start, int End)
        {
            public IEnumerable<int> GetSequence => Enumerable.Range(Start, GetCountOfIds);
            public int GetCountOfIds => End - Start + 1;
        };
    }
}
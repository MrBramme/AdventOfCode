using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AdventOfCode.Year2022.Solutions
{
    public class Solution202204 : ISolution
    {
        private readonly ILogger<Solution202204> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2022\\Day04.txt";

        public Solution202204(ILogger<Solution202204> logger, IInputService inputService)
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
                if (workPairs.Item1.Start <= workPairs.Item2.Start && workPairs.Item1.End >= workPairs.Item2.End)
                {
                    result++;
                    continue;
                }
                if (workPairs.Item2.Start <= workPairs.Item1.Start && workPairs.Item2.End >= workPairs.Item1.End)
                {
                    result++;
                }
            }
            return $"{result}";
        }

        private record IdSet(int Start, int End);
    }
}
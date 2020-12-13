using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace AdventOfCode.Year2020.Solutions
{
    public class Solution202013Part2 : ISolution
    {
        private readonly ILogger<Solution202013Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2020\\Day13.txt";
        public Solution202013Part2(ILogger<Solution202013Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var inputData = _inputService.GetInput(resourceLocation);
            var busses = inputData[1]
                .Split(',')
                .Select((inputString, index) => (inputString, index))
                .Where(input => !input.inputString.Equals("x"))
                .Select(input => new Bus(input.inputString, input.index))
                .ToList();

            long increment = 1;
            long counter = 0;
            for (var i = 2; i <= busses.Count; i++)
            {
                var currentList = busses.Take(i).ToList();

                var isDone = false;
                while (!isDone)
                {
                    if (currentList.All(bus => bus.DoesBusDepart(counter)))
                    {
                        increment = currentList.Aggregate(1L, (agg, bus) => agg * bus.Id);
                        isDone = true;
                        continue;
                    }
                    counter += increment;
                }
            }
            return $"{counter}";
        }

        class Bus
        {
            public readonly long Id;
            public readonly long Offset;

            public Bus(string busId, int offset)
            {
                Id = long.Parse(busId);
                Offset = (long)offset;
            }

            public bool DoesBusDepart(long baseTime)
            {
                return (baseTime + Offset) % Id == 0;
            }
        }
    }
}
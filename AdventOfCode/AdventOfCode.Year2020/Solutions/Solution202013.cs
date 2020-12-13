using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
namespace AdventOfCode.Year2020.Solutions
{
    public class Solution202013 : ISolution
    {
        private readonly ILogger<Solution202013> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2020\\Day13.txt";
        public Solution202013(ILogger<Solution202013> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }
        public string GetSolution()
        {
            var inputData = _inputService.GetInput(resourceLocation);
            var earliestStart = long.Parse(inputData[0]);
            var busses = new List<Bus>();
            foreach (var busId in inputData[1].Split(','))
            {
                if (!busId.Equals("x"))
                {
                    busses.Add(new Bus(busId));
                }
            }

            Bus earliestBus = null;
            var currentTime = earliestStart - 1;
            while (earliestBus == null)
            {
                currentTime++;
                foreach (var bus in busses)
                {
                    if (currentTime % bus.Id == 0)
                    {
                        earliestBus = bus;
                    }
                }
            }

            var result = (currentTime - earliestStart) * earliestBus.Id;
            return $"{result}";
        }

        class Bus
        {
            public int Id { get; set; }

            public Bus(string busId)
            {
                Id = int.Parse(busId);
            }
        }
    }
}
using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021.Solutions
{
    public class Solution202106 : ISolution
    {
        private readonly ILogger<Solution202106> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2021\\Day06.txt";

        public Solution202106(ILogger<Solution202106> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var fishes = _inputService.GetInput(resourceLocation).First().Split(",").Select(x => new LanternFish(int.Parse(x))).ToList();
            
            for(var i = 0; i < 80; i++)
            {
                fishes.ToList().ForEach(x => x.Next());
                var newFishes = fishes.Where(x => x.currentCounter < 0).Select(x => x.CreateNew()).ToList();
                fishes.AddRange(newFishes);
            }
            return $"{fishes.Count()}";
        }

        private class LanternFish
        {
            public int currentCounter;
            public LanternFish(int startNr)
            {
                currentCounter = startNr;
            }

            public void Next()
            {
                currentCounter--;
            }

            public LanternFish CreateNew()
            {
                currentCounter = 6;
                return new LanternFish(8);
            }
        }

    }

    
}
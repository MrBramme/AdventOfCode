using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021.Solutions
{
    public class Solution202112 : ISolution
    {
        private readonly ILogger<Solution202112> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2021\\Day12.txt";

        public Solution202112(ILogger<Solution202112> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var map = CreateMap();
            var visitedCaves = new List<string>() { "start" };
            var result = ExploreMap(map, "start", visitedCaves);

            return $"{result}";
        }

        private Dictionary<string, string[]> CreateMap()
        {
            return _inputService.GetInput(resourceLocation)
                .Select(i => i.Split('-'))
                .SelectMany(i => new[] { (From: i[0], To: i[1]) , (From: i[1], To: i[0]) })
                .GroupBy(x => x.From)
                .ToDictionary(g => g.Key, g => g.Select(conn => conn.To).ToArray());
        }

        private int ExploreMap(Dictionary<string, string[]> map, string currentCave, List<string> visitedCaves)
        {
            var result = 0;
            if (currentCave == "end")
            {
                result++;
            }
            else
            {
                foreach (var cave in map[currentCave])
                {
                    if (IsUnivistedCave(visitedCaves, cave) || IsBigCave(cave))
                    {
                        var newVisitedCaves = new List<string>(visitedCaves);
                        newVisitedCaves.Add(cave);
                        result += ExploreMap(map, cave, newVisitedCaves);
                    }
                }
            }
            
            return result;
        }

        private bool IsUnivistedCave(List<string> visitedCaves, string currentCave)
        {
            return !visitedCaves.Contains(currentCave);
        }

        private bool IsBigCave(string caveName)
        {
            return caveName.ToLower() != caveName;
        }
    }
}
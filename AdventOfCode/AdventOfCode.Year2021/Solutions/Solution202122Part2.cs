using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2021.Solutions
{
    public class Solution202122Part2 : ISolution
    {
        private readonly ILogger<Solution202122Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2021\\Day22.txt";

        public Solution202122Part2(ILogger<Solution202122Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            throw new NotImplementedException("not done yet");
            var input = _inputService.GetInput(resourceLocation).Select(i => new Instruction(i));

            var cubeData = new Dictionary<string, Position>();

            foreach (var instruction in input)
            {
                if (instruction.State)
                {
                    foreach (var pos in instruction.Positions)
                    {
                        cubeData.TryAdd(pos.ToString(), pos);
                    }
                }
                else
                {
                    foreach (var pos in instruction.Positions)
                    {
                        cubeData.Remove(pos.ToString());
                    }
                }
            }
            var result = cubeData.Count();
            return $"{result}";
        }

        private class Instruction
        {
            public Instruction(string input)
            {
                var rx = new Regex(@"(on|off) x=(-?\d*)..(-?\d*),y=(-?\d*)..(-?\d*),z=(-?\d*)..(-?\d*)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                Match match = rx.Matches(input).First();
                State = match.Groups[1].Value.Equals("on");

                var x1 = int.Parse(match.Groups[2].Value);
                var x2 = int.Parse(match.Groups[3].Value);
                var y1 = int.Parse(match.Groups[4].Value);
                var y2 = int.Parse(match.Groups[5].Value);
                var z1 = int.Parse(match.Groups[6].Value);
                var z2 = int.Parse(match.Groups[7].Value);

                Positions = new List<Position>();
                for (var x = x1; x <= x2; x++)
                {
                    for (var y = y1; y <= y2; y++)
                    {
                        for (var z = z1; z <= z2; z++)
                        {
                            Positions.Add(new Position(x, y, z));
                        }
                    }
                }
            }

            public List<Position> Positions;
            public bool State;
        }

        private record Position(int x, int y, int z)
        {
            public override string ToString()
            {
                return $"{x}-{y}-{z}";
            }
        }
    }
}
using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
namespace AdventOfCode.Year2020.Solutions
{
    public class Solution202014Part2 : ISolution
    {
        private readonly ILogger<Solution202014Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2020\\Day14.txt";
        public Solution202014Part2(ILogger<Solution202014Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        // Looked at https://github.com/LennardF1989/AdventOfCode2020/blob/master/Src/AdventOfCode2020/Days/Day14.cs to get a clue
        public string GetSolution()
        {
            var inputData = _inputService.GetInput(resourceLocation);

            var maskLength = 36;
            var mem = new Dictionary<long, long>();
            var currentMask = new char[maskLength];
            var numberOfCombinations = 0L;
            var valueRegex = new Regex("mem\\[(?'memlocation'\\d*)\\] = (?'value'\\d*)");
            foreach (var input in inputData)
            {
                if (input.StartsWith("mask"))
                {
                    currentMask = input.Substring(7).ToCharArray();
                    numberOfCombinations = (long)BigInteger.Pow(2, currentMask.Count(m => m.Equals('X')));
                }
                else
                {
                    var matches = valueRegex.Match(input);
                    var memLocation = long.Parse(matches.Groups["memlocation"].Value);
                    var value = long.Parse(matches.Groups["value"].Value);

                    for (long i = 0; i < numberOfCombinations; i++)
                    {
                        var currentMemLocation = memLocation;

                        var offset = 0;
                        for (var maskIndex = 0; maskIndex < maskLength; maskIndex++)
                        {
                            var currentMaskChar = currentMask[maskLength - 1 - maskIndex];

                            switch (currentMaskChar)
                            {
                                case '1':
                                    currentMemLocation |= 1L << maskIndex;
                                    break;
                                case 'X':
                                    {
                                        var onOrOff = (i >> offset) & 1;

                                        if (onOrOff == 0)
                                        {
                                            currentMemLocation &= ~(1L << maskIndex);
                                        }
                                        else
                                        {
                                            currentMemLocation |= (1L << maskIndex);
                                        }

                                        offset++;
                                        break;
                                    }
                            }
                        }

                        mem[currentMemLocation] = value;
                    }
                }
            }

            var result = mem.Values.Sum();
            return $"{result}";
        }
    }
}
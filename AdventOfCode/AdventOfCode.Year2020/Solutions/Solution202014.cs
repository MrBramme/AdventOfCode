using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
namespace AdventOfCode.Year2020.Solutions
{
    public class Solution202014 : ISolution
    {
        private readonly ILogger<Solution202014> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2020\\Day14.txt";
        public Solution202014(ILogger<Solution202014> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }
        public string GetSolution()
        {
            var inputData = _inputService.GetInput(resourceLocation);

            var mem = new Dictionary<string, long>();
            var currentMask = string.Empty;
            var valueRegex = new Regex("mem\\[(?'memlocation'\\d*)\\] = (?'value'\\d*)");
            for (var index = 0; index < inputData.Length; index++)
            {
                if (inputData[index].StartsWith("mask"))
                {
                    currentMask = inputData[index].Substring(7);
                }
                else
                {
                    var matches = valueRegex.Match(inputData[index]);
                    var memLocation = matches.Groups["memlocation"].Value;
                    var value = long.Parse(matches.Groups["value"].Value);
                    var valueAsBinary = Convert.ToString(value, 2).PadLeft(36, '0');
                    var maskedValue = valueAsBinary.Zip(currentMask, (originalBit, maskBit) =>
                    {
                        return maskBit switch
                        {
                            'X' => originalBit,
                            _ => maskBit
                        };
                    });
                    mem[memLocation] = Convert.ToInt64(new string(maskedValue.ToArray()), 2);
                }
            }

            var result = mem.Values.Sum();
            return $"{result}";
        }
    }
}
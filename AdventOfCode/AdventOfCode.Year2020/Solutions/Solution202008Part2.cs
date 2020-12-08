using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2020.Solutions
{
    public class Solution202008Part2 : ISolution
    {
        private readonly ILogger<Solution202008Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2020\\Day08.txt";

        public Solution202008Part2(ILogger<Solution202008Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var inputData = _inputService.GetInput(resourceLocation);

            var result = -1;
            for (var i = 0; i < inputData.Length; i++)
            {
                var index = 0;
                var value = 0;
                var pastIndices = new List<int>();
                var currentValue = inputData[i].Split()[0];
                if (currentValue.Equals("nop") || currentValue.Equals("jmp"))
                {
                    var isDone = false;
                    var isSuccess = false;

                    var input = new List<string>(inputData).ToArray();
                    input[i] = currentValue.Equals("nop") ? inputData[i].Replace("nop", "jmp") : inputData[i].Replace("jmp", "nop");

                    while (!isDone)
                    {
                        if (index >= input.Length)
                        {
                            isSuccess = true;
                            isDone = true;
                            result = value;
                            break;
                        }
                        if (pastIndices.Contains(index))
                        {
                            isDone = true;
                            break;
                        }
                        pastIndices.Add(index);
                        var instruction = GetInstruction(input[index]);
                        switch (instruction.Item1)
                        {
                            case "nop":
                                index++;
                                break;
                            case "acc":
                                index++;
                                value += instruction.Item2;
                                break;
                            case "jmp":
                                index += instruction.Item2;
                                break;
                        }
                    }

                    if (isSuccess)
                    {
                        break;
                    }
                }
            }
            return $"{result}";
        }

        private static ValueTuple<string, int> GetInstruction(string input)
        {
            var parts = input.Split(' ');
            var value = int.Parse(parts[1]);
            return (parts[0], value);
        }
    }
}
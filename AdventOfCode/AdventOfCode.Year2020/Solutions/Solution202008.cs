using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2020.Solutions
{
    public class Solution202008 : ISolution
    {
        private readonly ILogger<Solution202008> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2020\\Day08.txt";

        public Solution202008(ILogger<Solution202008> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var inputData = _inputService.GetInput(resourceLocation);
            var pastIndices = new List<int>();
            (int index, int value) state = (0, 0);
            var isDone = false;
            while (!isDone)
            {
                if (pastIndices.Contains(state.index))
                {
                    isDone = true;
                    break;
                }
                pastIndices.Add(state.index);
                var instruction = GetInstruction(inputData[state.index]);
                switch (instruction.Item1)
                {
                    case "nop":
                        state.index++;
                        break;
                    case "acc":
                        state.index++;
                        state.value += instruction.Item2;
                        break;
                    case "jmp":
                        state.index += instruction.Item2;
                        break;
                }
            }
            return $"{state.value}";
        }

        private static ValueTuple<string, int> GetInstruction(string input)
        {
            var parts = input.Split(' ');
            var value = int.Parse(parts[1]);
            return (parts[0], value);
        }
    }
}
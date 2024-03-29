﻿using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AdventOfCode.Year2023.Solutions
{
    public class Solution202301 : ISolution
    {
        private readonly ILogger<Solution202301> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2023\\Day01.txt";

        public Solution202301(ILogger<Solution202301> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }
        public string GetSolution()
        {
            var values = _inputService.GetInput(resourceLocation).ToList();
            var numbers = new List<int>();
            foreach (var value in values)
            {
                var chars = value.ToCharArray();
                var first = GetFirstDigit(chars);
                var second = GetLastDigit(chars);
                var number = int.Parse(first.ToString() + second.ToString());
                numbers.Add(number);
            }

            return $"{numbers.Sum()}";
        }

        private static char GetFirstDigit(char[] chars)
        {
            for (var i = 0; i < chars.Length; i++)
            {
                if (Char.IsDigit(chars[i]))
                {
                    return chars[i];
                }
            }
            return '\0';
        }

        private static char GetLastDigit(char[] chars)
        {
            for (var i = chars.Length - 1; i >= 0; i--)
            {
                if (Char.IsDigit(chars[i]))
                {
                    return chars[i];
                }
            }
            return '\0';
        }
    }
}
using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AdventOfCode.Year2023.Solutions
{
    public class Solution202301Part2 : ISolution
    {
        private readonly ILogger<Solution202301Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2023\\Day01.txt";

        public Solution202301Part2(ILogger<Solution202301Part2> logger, IInputService inputService)
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
                var text = ReplaceTextWithDigits(value);
                var chars = text.ToCharArray();
                var first = GetFirstDigit(chars);
                var second = GetLastDigit(chars);
                var number = int.Parse(first.ToString() + second.ToString());
                numbers.Add(number);
            }

            return $"{numbers.Sum()}";
        }

        private static string ReplaceTextWithDigits(string input)
        {
            // replacement to accomodate start/end of other digits
            var result = input
                .Replace("one", "one1one")
                .Replace("two", "two2two")
                .Replace("three", "three3three")
                .Replace("four", "four4four")
                .Replace("five", "five5five")
                .Replace("six", "six6six")
                .Replace("seven", "seven7seven")
                .Replace("eight", "eight8eight")
                .Replace("nine", "nine9nine");
            return result;
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
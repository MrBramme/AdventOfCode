using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2020.Solutions
{
    public class Solution202006 : ISolution
    {
        private readonly ILogger<Solution202006> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2020\\Day06.txt";

        public Solution202006(ILogger<Solution202006> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var inputData = _inputService.GetInput(resourceLocation);

            var answers = GetAnswerCountPerGroup(inputData);

            var result = answers.Sum();
            return $"{result}";
        }

        private static List<int> GetAnswerCountPerGroup(string[] inputData)
        {
            var answers = new List<int>();
            var currentAnswers = new List<char>();
            foreach (var input in inputData)
            {
                if (!string.IsNullOrEmpty(input))
                {
                    currentAnswers.AddRange(input.ToCharArray());
                }
                else
                {
                    answers.Add(currentAnswers.Distinct().Count());
                    currentAnswers = new List<char>();
                }
            }

            answers.Add(currentAnswers.Distinct().Count());
            return answers;
        }
    }
}
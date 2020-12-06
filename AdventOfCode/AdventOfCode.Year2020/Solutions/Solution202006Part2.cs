using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2020.Solutions
{
    public class Solution202006Part2 : ISolution
    {
        private readonly ILogger<Solution202006Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2020\\Day06.txt";

        public Solution202006Part2(ILogger<Solution202006Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var inputData = _inputService.GetInput(resourceLocation);
            var groupedAnswers = GetGroupedResults(inputData);

            var answers = new List<int>();
            foreach (var groupAnswers in groupedAnswers)
            {
                var groupCount = groupAnswers.Count;
                var answerGroups = groupAnswers.SelectMany(x => x.ToCharArray()).GroupBy(x => x);
                answers.Add(answerGroups.Count(x => x.Count() == groupCount));
            }

            var result = answers.Sum();
            return $"{result}";
        }

        private static List<List<string>> GetGroupedResults(string[] inputData)
        {
            var result = new List<List<string>>();
            var currentResultSet = new List<string>();
            foreach (var input in inputData)
            {
                if (!string.IsNullOrEmpty(input))
                {
                    currentResultSet.Add(input);
                }
                else
                {
                    result.Add(currentResultSet);
                    currentResultSet = new List<string>();
                }
            }

            result.Add(currentResultSet);
            return result;
        }

    }
}
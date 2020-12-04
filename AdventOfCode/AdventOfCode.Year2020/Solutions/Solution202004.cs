using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2020.Domain;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Year2020.Solutions
{
    public class Solution202004 : ISolution
    {
        private readonly ILogger<Solution202004> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2020\\Day04.txt";

        public Solution202004(ILogger<Solution202004> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }
        public string GetSolution()
        {
            var inputData = _inputService.GetInput(resourceLocation);
            var passports = CreatePassports(inputData);
            return $"{passports.Count(p => p.RequiredFieldsPresent())}";
        }

        private static List<Passport202004> CreatePassports(string[] inputData)
        {
            var passports = new List<Passport202004>();
            var stringBuilder = new StringBuilder();
            foreach (var passport in inputData)
            {
                if (!string.IsNullOrEmpty(passport))
                {
                    stringBuilder.Append(passport);
                    stringBuilder.Append(" ");
                }
                else
                {
                    passports.Add(new Passport202004(stringBuilder.ToString().Trim()));
                    stringBuilder.Clear();
                }
            }

            passports.Add(new Passport202004(stringBuilder.ToString().Trim()));
            stringBuilder.Clear();
            return passports;
        }
    }
}
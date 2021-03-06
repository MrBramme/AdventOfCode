﻿using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2020.Solutions
{
    public class Solution202019Part2 : ISolution
    {
        private readonly ILogger<Solution202019Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2020\\Day19.txt";
        private Dictionary<string, string> _rules = new Dictionary<string, string>();

        // props to: https://github.com/viceroypenguin/adventofcode/blob/master/2020/day19.original.cs
        public Solution202019Part2(ILogger<Solution202019Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var inputData = _inputService.GetInput(resourceLocation);

            foreach (var input in inputData.TakeWhile(i => !string.IsNullOrEmpty(i)))
            {
                var parts = input.Split(':');
                _rules.Add(parts[0], parts[1].Trim());
            }

            _rules["8"] = "42 | 42 8";
            _rules["11"] = "42 31 | 42 11 31";

            var messages = inputData.SkipWhile(i => !string.IsNullOrEmpty(i)).Skip(1);

            var regexRules = new Dictionary<string, string>();
            string BuildRegex(string input)
            {
                if (regexRules.TryGetValue(input, out var s))
                    return s;

                var orig = _rules[input];
                if (orig.StartsWith("\""))
                    return regexRules[input] = orig.Replace("\"", "");

                if (!orig.Contains("|"))
                    return regexRules[input] = string.Join("", orig.Split().Select(BuildRegex));

                return regexRules[input] =
                    "(" +
                    string.Join("", orig.Split().Select(x => x == "|" ? x : BuildRegex(x))) +
                    ")";
            }

            var regex = new Regex($@"^({BuildRegex("42")})+(?<open>{BuildRegex("42")})+(?<close-open>{BuildRegex("31")})+(?(open)(?!))$");

            return $"{messages.Count(regex.IsMatch)}";
        }


    }
}
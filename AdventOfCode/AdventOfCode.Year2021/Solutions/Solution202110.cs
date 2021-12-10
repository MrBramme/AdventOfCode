using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021.Solutions
{
    public class Solution202110 : ISolution
    {
        private readonly ILogger<Solution202110> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2021\\Day10.txt";

        public Solution202110(ILogger<Solution202110> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var result = _inputService.GetInput(resourceLocation)
                .Select(i => new NavEntry(i))
                .Where(d => d.IsInvalid())
                .Sum(d => d.GetInvalidScore());
            return $"{result}";
        }

        private class NavEntry
        {
            private char[] _data;
            private char[] _closingTags = new[] { ')', ']', '}', '>' };
            private Dictionary<char, int> _points = new Dictionary<char, int>
            {
                { ' ', 0 },
                { ')', 3 },
                { ']', 57 },
                { '}', 1197 },
                { '>', 25137 }
            };
            private char _invalidChar;

            public NavEntry(string line)
            {
                _data = line.ToCharArray();
                _invalidChar = GetInvalidChar();
            }

            public bool IsInvalid()
            {
                return _invalidChar != ' ';
            }

            public int GetInvalidScore()
            {
                return _points[_invalidChar];
            }

            private char GetInvalidChar()
            {
                var stack = new Stack<char>();
                foreach (var currentTag in _data)
                {
                    if (IsClosingTag(currentTag))
                    {
                        var openingTag = stack.Pop();
                        if (isClosingInvalid(openingTag, currentTag))
                        {
                            return currentTag;
                        }
                    }
                    else
                    {
                        stack.Push(currentTag);
                    }
                }
                return ' ';
            }

            private bool IsClosingTag(char currentTag)
            {
                return _closingTags.Contains(currentTag);
            }

            private bool isClosingInvalid(char openingTag, char closingTag)
            {
                var result = 
                    (openingTag == '(' && closingTag != ')') || 
                    (openingTag == '[' && closingTag != ']' ) ||
                    (openingTag == '{' && closingTag != '}') ||
                    (openingTag == '<' && closingTag != '>');
                return result;
            }
        }

    }
}
using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021.Solutions
{
    public class Solution202110Part2 : ISolution
    {
        private readonly ILogger<Solution202110Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2021\\Day10.txt";

        public Solution202110Part2(ILogger<Solution202110Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var results = _inputService.GetInput(resourceLocation)
                .Select(i => new NavEntry(i))
                .Where(d => d.IsIncomplete())
                .Select(d => d.GetIncompleteScore())
                .OrderBy(score => score)
                .ToArray();

            var middleIndex = results.Length / 2;
            return $"{results[middleIndex]}";
        }

        private class NavEntry
        {
            private char[] _data;
            private char[] _closingTags = new[] { ')', ']', '}', '>' };
            private Dictionary<char, long> _points = new Dictionary<char, long>
            {
                { ')', 1 },
                { ']', 2 },
                { '}', 3 },
                { '>', 4 }
            };
            private Dictionary<char, char> _closingTagsDictionary = new Dictionary<char, char>
            {
                { '(', ')' },
                { '[', ']' },
                { '{', '}' },
                { '<', '>' }
            };

            private char _invalidChar;

            public NavEntry(string line)
            {
                _data = line.ToCharArray();
                _invalidChar = GetInvalidChar();
            }

            public bool IsIncomplete()
            {
                return _invalidChar == ' ';
            }

            public long GetIncompleteScore()
            {
                var unclosedTags = new Stack<char>();
                foreach (var currentTag in _data)
                {
                    if (IsClosingTag(currentTag))
                    {
                        unclosedTags.Pop();
                    }
                    else
                    {
                        unclosedTags.Push(currentTag);
                    }
                }

                long score = 0;
                foreach (var unclosedTag in unclosedTags)
                {
                    score *= 5;
                    score += _points[_closingTagsDictionary[unclosedTag]];
                }

                return score;
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
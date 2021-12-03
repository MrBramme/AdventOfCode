using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace AdventOfCode.Year2021.Solutions
{
    public class Solution202103 : ISolution
    {
        private readonly ILogger<Solution202103> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2021\\Day03.txt";

        public Solution202103(ILogger<Solution202103> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var input = _inputService.GetInput(resourceLocation);
            var binaryLength = input[0].Length;

            var binaryCounter = input
                .Select(input => input.ToCharArray())
                .Aggregate(new BinaryCounter(binaryLength), (acc, item) => Enumerable.Range(0, binaryLength).Aggregate(acc, (acc, i) => acc.AddCount(i, item[i])));

            var result = binaryCounter.GetGamma() * binaryCounter.GetEpsilon();
            return result.ToString();
        }

        private class BinaryCounter
        {
            private readonly int[] _countOfOne;
            private readonly int[] _countOfZero;
            private readonly int _length;

            public BinaryCounter(int length)
            {
                _length = length;
                _countOfOne = new int[length];
                _countOfZero = new int[length];
            }

            public BinaryCounter AddCount(int index, char bin)
            {
                if (bin == '1')
                {
                    _countOfOne[index]++;
                }
                else
                {
                    _countOfZero[index]++;
                }
                return this;
            }

            public int GetGamma()
            {
                return Convert.ToInt32(Get("1", "0"), 2);
            }

            public int GetEpsilon()
            {
                return Convert.ToInt32(Get("0", "1"), 2);
            }

            private string Get(string higher, string lower)
            {
                return string.Join(string.Empty, Enumerable.Range(0, _length).Select(i => _countOfOne[i] > _countOfZero[i] ? higher : lower));
            }

        }
    }
}
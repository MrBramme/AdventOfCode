using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021.Solutions
{
    public class Solution202108Part2 : ISolution
    {
        private readonly ILogger<Solution202108Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2021\\Day08.txt";

        public Solution202108Part2(ILogger<Solution202108Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var patterns = _inputService.GetInput(resourceLocation).Select(p => p.Split(" | ").Select(i => i.Split(" "))).Select(p => new Display(p.First(), p.Last()));
            var result = patterns.Sum( p => p.Output);
            return $"{result}";
        }

        private class Display
        {
            private SignalOutput[] _signals;
            private Dictionary<int, SignalOutput> _mapping = new Dictionary<int, SignalOutput>();
            private SignalOutput[] _digits;
            public long Output;
            
            public Display(string[] signals, string[] digits)
            {
                _signals = signals.Select(s => new SignalOutput(s.ToCharArray().OrderBy(c => c).ToArray(), s.Length)).ToArray();
                ProcessSignals();
                _digits = digits.Select(s => new SignalOutput(s.ToCharArray().OrderBy(c => c).ToArray(), s.Length)).ToArray();
                var firstDigit = _mapping.Single(x => x.Value.ToString().Equals(_digits[0].ToString())).Key;
                var secondDigit = _mapping.Single(x => x.Value.ToString().Equals(_digits[1].ToString())).Key;
                var thirdDigit = _mapping.Single(x => x.Value.ToString().Equals(_digits[2].ToString())).Key;
                var forthDigit = _mapping.Single(x => x.Value.ToString().Equals(_digits[3].ToString())).Key;
                Output = firstDigit * 1000 + secondDigit * 100 + thirdDigit * 10 + forthDigit;
            }

            // Warning, there be dangerous code below
            private void ProcessSignals()
            {
                _mapping.Add(1, _signals.Single(s => s.Length == 2));
                _mapping.Add(7, _signals.Single(s => s.Length == 3));
                _mapping.Add(4, _signals.Single(s => s.Length == 4));
                _mapping.Add(8, _signals.Single(s => s.Length == 7));

                var six = _signals.Where(s => s.Length == 6).Single(s => !_mapping[1].Characters.All(x => s.Characters.Contains(x)));
                _mapping.Add(6, six);

                var topRight = _mapping[1].Characters.First(c => !_mapping[6].Characters.Contains(c));
                var bottomRight = _mapping[1].Characters.First(c => c != topRight);

                var five = _signals.Where(s => s.Length == 5).Single(s => !s.Characters.Contains(topRight));
                _mapping.Add(5, five);

                var leftBottom = _mapping[6].Characters.Single(s => !_mapping[5].Characters.Contains(s));
                var nine = _signals.Where(s => s.Length == 6).Single(s => !s.Characters.Contains(leftBottom));
                _mapping.Add(9, nine);


                var zero = _signals.Where(s => s.Length == 6).Single(s => s.Characters.Contains(leftBottom) && s.Characters.Contains(topRight));
                _mapping.Add(0, zero);

                var two = _signals.Where(s => s.Length == 5).Single(s => s.Characters.Contains(leftBottom));
                _mapping.Add(2, two);

                var three = _signals.Where(s => s.Length == 5).Single(s => s.Characters.Contains(topRight) && s.Characters.Contains(bottomRight));
                _mapping.Add(3, three);
            }
        }

        private record SignalOutput(char[] Characters, int Length)
        {
            public override string ToString()
            {
                return new string(Characters);
            }
        }    }
}
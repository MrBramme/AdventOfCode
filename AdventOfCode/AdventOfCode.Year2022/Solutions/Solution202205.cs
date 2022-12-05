using System.Text.RegularExpressions;
using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AdventOfCode.Year2022.Solutions
{
    public class Solution202205 : ISolution
    {
        private Regex regexMoves = new Regex(@"^move (?'g1'\d+) from (?'g2'\d+) to (?'g3'\d+)$");
        private readonly ILogger<Solution202205> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2022/Day05.txt";

        public Solution202205(ILogger<Solution202205> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var input = _inputService.GetInput(resourceLocation).ToList();

            var cratesInput = new List<char[]>();
            var movements = new List<Movement>();
            var isMoves = false;
            foreach (string item in input)
            {
                if (string.IsNullOrWhiteSpace(item))
                {
                    isMoves = true;
                    continue;
                }
                if (isMoves) 
                {
                    var matchedMoves = regexMoves.Match(item);
                    var movement = new Movement(int.Parse(matchedMoves.Groups["g1"].Value), int.Parse(matchedMoves.Groups["g2"].Value), int.Parse(matchedMoves.Groups["g3"].Value));
                    movements.Add(movement); 
                } else
                {
                    if (item.IndexOf("[") > -1)
                    {
                        cratesInput.Add(item.ToCharArray());
                    }
                }
            }
            var cratesLength = cratesInput.First().Length;
            var crates = new Stack<char>[(cratesLength + 1) / 4];
            cratesInput.Reverse();
            foreach (var crateInput in cratesInput)
            {
                var stackIndex = 0;
                for (var i = 1; i < cratesLength; i += 4)
                {
                    if (crates[stackIndex] == null) crates[stackIndex] = new Stack<char>();
                    if (crateInput[i] != ' ') crates[stackIndex].Push(crateInput[i]);
                    stackIndex++;
                }
            }

            foreach(var movement in movements)
            {
                for(var i = 0; i < movement.Amount; i++)
                {
                    var crate = crates[movement.Source - 1].Pop();
                    crates[movement.Target - 1].Push(crate);
                }
            }

            var result = "";
            foreach (var crateStack in crates)
            {
                result += crateStack.Pop();
            }
            
            return $"{result}";
        }

        private record Movement(int Amount, int Source, int Target);
    }
}
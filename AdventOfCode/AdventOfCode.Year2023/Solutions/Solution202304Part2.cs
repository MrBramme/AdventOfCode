using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2023.Solutions
{
    public class Solution202304Part2 : ISolution
    {
        private readonly ILogger<Solution202304Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2023\\Day04.txt";

        public Solution202304Part2(ILogger<Solution202304Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }
        public string GetSolution()
        {
            var cards = _inputService.GetInput(resourceLocation).Select(Card.FromInput).ToArray();
            for (var i = 0; i < cards.Length; i++)
            {
                foreach (var winningCardIndex in Enumerable.Range(i + 1, cards[i].WinCount))
                {
                    cards[winningCardIndex].CardCount += cards[i].CardCount;
                }
            }
            return $"{cards.Sum(card => card.CardCount)}";
        }

        private class Card
        {
            public Card(int id, List<int> winningNumbers, List<int> numbers)
            {
                Id = id;
                WinningNumbers = winningNumbers;
                Numbers = numbers;
                WinCount = winningNumbers.Intersect(numbers).Count();
            }

            public int Id { get; }
            public List<int> WinningNumbers { get; }
            public List<int> Numbers { get; }
            public int WinCount { get; }
            public int CardCount { get; set; } = 1;

            public static Card FromInput(string input)
            {
                var regex = new Regex(@"Card\s{1,3}(\d+): (.*) \| (.*)");
                var matches = regex.Matches(input);
                var id = int.Parse(matches[0].Groups[1].Value);
                var winningNumbers = matches[0].Groups[2].Value.Trim().Replace("  ", " ").Split(" ").Select(int.Parse).ToList();
                var numbers = matches[0].Groups[3].Value.Trim().Replace("  ", " ").Split(" ").Select(int.Parse).ToList();
                return new Card(id, winningNumbers, numbers);
            }
        }


    }
}
using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AdventOfCode.Year2023.Solutions
{
    public class Solution202307Part2 : ISolution
    {
        private readonly ILogger<Solution202307Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2023\\Day07.txt";

        private readonly Dictionary<char, int> _strengthMapping = new Dictionary<char, int>
        {
            { '2', 1 },
            { '3', 2 },
            { '4', 3 },
            { '5', 4 },
            { '6', 5 },
            { '7', 6 },
            { '8', 7 },
            { '9', 8 },
            { 'T', 9 },
            { 'J', 0 },
            { 'Q', 11 },
            { 'K', 12 },
            { 'A', 13 },
        };

        public Solution202307Part2(ILogger<Solution202307Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }
        public string GetSolution()
        {
            var values = _inputService.GetInput(resourceLocation);
            var hands = new List<Hand>();
            foreach (var value in values)
            {
                var parts = value.Split(' ');
                var cards = parts[0].ToCharArray();
                var hand = new Hand(cards, cards.Select(x => _strengthMapping[x]).ToArray(), int.Parse(parts[1]));
                hands.Add(hand);
            }

            var orderedHands = hands.OrderBy(c => c.HandStrength).ThenBy(c => c.Strength[0]).ThenBy(c => c.Strength[1]).ThenBy(c => c.Strength[2]).ThenBy(c => c.Strength[3])
                .ThenBy(c => c.Strength[4]).ToArray();

            var result = 0;
            for (var i = 0; i < orderedHands.Length; i++)
            {
                result += (i + 1) * orderedHands[i].Bid;
            }

            return $"{result}";
        }

        private class Hand
        {
            public int Bid { get; }
            public char[] Cards { get; }
            public int[] Strength { get; }
            public Dictionary<char, int> CardCount = new();
            public int HandStrength;

            public Hand(char[] cards, int[] strength, int bid)
            {
                Bid = bid;
                Cards = cards;
                Strength = strength;

                foreach (var card in cards)
                {
                    if (CardCount.ContainsKey(card)) CardCount[card]++;
                    else
                    {
                        CardCount.Add(card, 1);
                    }
                }

                if (CardCount.ContainsKey('J') && CardCount.Count > 1)
                {
                    var bestCard = CardCount.Where(k => k.Key != 'J').MaxBy(c => c.Value);
                    CardCount[bestCard.Key] += CardCount['J'];
                    CardCount.Remove('J');
                }

                SetHandStrength();

            }

            private void SetHandStrength()
            {
                switch (CardCount.Count)
                {
                    case 1: // five of a kind
                        HandStrength = 6; break;
                    case 2: // four of a kind, full house
                        if (CardCount.ContainsValue(4)) HandStrength = 5;
                        else
                        {
                            HandStrength = 4;
                        }
                        break;
                    case 3: // three of a kind, two pair
                        if (CardCount.ContainsValue(3)) HandStrength = 3;
                        else
                        {
                            HandStrength = 2;
                        }
                        break;
                    case 4: // one pair
                        HandStrength = 1; break;
                    default: // high card
                        HandStrength = 0;
                        break;
                }
            }
        }
    }
}
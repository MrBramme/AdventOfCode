using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2022.Solutions
{
    public class Solution202211Part2 : ISolution
    {
        private readonly ILogger<Solution202211Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2022/Day11.txt";

        public Solution202211Part2(ILogger<Solution202211Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var input = _inputService.GetInput(resourceLocation);
            var monkeys = ParseMonkeys(input);

            var worryModifier = monkeys.Aggregate(1, (worryModifier, monkey) => worryModifier * monkey.Test);

            for (var i = 0; i < 10000; i++)
            {
                foreach (var monkey in monkeys)
                {
                    while (monkey.Items.Any())
                    {
                        monkey.InspectedItems++;

                        var item = monkey.Items.Dequeue();
                        item = monkey.Operation(item);
                        item %= worryModifier;

                        var targetMonkey = item % monkey.Test == 0 ?
                            monkey.MonkeyIfTrue :
                            monkey.MonkeyIfFalse;

                        monkeys[targetMonkey].Items.Enqueue(item);
                    }
                }
            }

            var result = monkeys
                    .OrderByDescending(monkey => monkey.InspectedItems)
                    .Take(2)
                    .Aggregate(1L, (res, monkey) => res * monkey.InspectedItems);

            return $"{result}";
        }

        private Monkey[] ParseMonkeys(string[] input)
        {
            var monkeys = new List<Monkey>();

            var monkey = new Monkey();
            foreach (var line in input)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    monkeys.Add(monkey);
                }
                else if (line.StartsWith("Monkey "))
                {
                    monkey = new Monkey();
                }
                else if (line.StartsWith("  Starting items: "))
                {
                    var items = (new Regex("Starting items: (?'items'.*)")).Match(line).Groups["items"].Value;
                    monkey.Items = new Queue<long>(items.Split(", ").Select(long.Parse));
                }
                else if (line.Equals("  Operation: new = old * old"))
                {
                    monkey.Operation = old => old * old;
                }
                else if (line.StartsWith("  Operation: new = old * "))
                {
                    var multiplier = (new Regex(@"Operation: new = old \* (?'multiplier'\d+)")).Match(line).Groups["multiplier"].Value;
                    monkey.Operation = old => old * int.Parse(multiplier);
                }
                else if (line.StartsWith("  Operation: new = old + "))
                {
                    var addition = (new Regex(@"Operation: new = old \+ (?'addition'\d+)")).Match(line).Groups["addition"].Value;
                    monkey.Operation = old => old + int.Parse(addition);
                }
                else if (line.StartsWith("  Test: divisible by "))
                {
                    var test = (new Regex(@"Test: divisible by (?'test'\d+)")).Match(line).Groups["test"].Value;
                    monkey.Test = int.Parse(test);
                }
                else if (line.StartsWith("    If true: throw to monkey "))
                {
                    var monkeyIfTrue = (new Regex(@"If true: throw to monkey (?'target'\d+)")).Match(line).Groups["target"].Value;
                    monkey.MonkeyIfTrue = int.Parse(monkeyIfTrue);
                }
                else if (line.StartsWith("    If false: throw to monkey "))
                {
                    var monkeyIfFalse = (new Regex(@"If false: throw to monkey (?'target'\d+)")).Match(line).Groups["target"].Value;
                    monkey.MonkeyIfFalse = int.Parse(monkeyIfFalse);
                }
                else
                {
                    throw new ArgumentException(line);
                }
            }
            monkeys.Add(monkey);
            return monkeys.ToArray();
        }

        private class Monkey
        {
            public Queue<long> Items;

            public int InspectedItems = 0;

            public Func<long, long> Operation;

            public int Test;

            public int MonkeyIfTrue;

            public int MonkeyIfFalse;

        }
    }
}
using System;
using System.Collections.Generic;
using AdventOfCode.Domain;

namespace AdventOfCode.Program
{
    class Program
    {
        static void Main(string[] args)
        {
            const bool requestInput = false;
            var (year, day) = GetYearAndDay(requestInput, 2019, 8);

            Console.WriteLine("Use test input (Y/N):");
            var useTestInput = Console.ReadKey();
            var useTest = useTestInput.Key == ConsoleKey.Y;
            Console.WriteLine($"\r\nUsing {(useTest ? "TEST" : "REAL")} data");

            var solution = GetSolution(year, day);
            solution.Run(useTest);
        }

        private static (int year, int day) GetYearAndDay(bool requestInput, int defaultYear, int defaultDay)
        {
            var year = defaultYear;
            var day = defaultDay;
            if (requestInput)
            {
                Console.WriteLine("What year should be used?");
                year = int.Parse(Console.ReadLine());

                Console.WriteLine("What day should be used?");
                day = int.Parse(Console.ReadLine());
            }

            return (year, day);
        }

        private static Solution GetSolution(int year, int day)
        {
            var solutions = new Dictionary<string, Solution>
            {
                { "2019-1", new _2019.Day01() },
                { "2019-2", new _2019.Day02() },
                { "2019-3", new _2019.Day03() },
                { "2019-4", new _2019.Day04() },
                { "2019-5", new _2019.Day05() },
                { "2019-6", new _2019.Day06() },
                { "2019-7", new _2019.Day07() },
                { "2019-8", new _2019.Day08() },
                { "2019-9", new _2019.Day09() },
                { "2019-10", new _2019.Day10() },
                { "2019-11", new _2019.Day11() },
                { "2019-12", new _2019.Day12() },
                { "2019-13", new _2019.Day13() },
                { "2019-14", new _2019.Day14() },
                { "2019-15", new _2019.Day15() },
                { "2019-16", new _2019.Day16() },
                { "2019-17", new _2019.Day17() },
                { "2019-18", new _2019.Day18() },
                { "2019-19", new _2019.Day19() },
                { "2019-20", new _2019.Day20() },
                { "2019-21", new _2019.Day21() },
                { "2019-22", new _2019.Day22() },
                { "2019-23", new _2019.Day23() },
                { "2019-24", new _2019.Day24() },
                { "2019-25", new _2019.Day25() }
            };

            var key = $"{year}-{day}";
            if (solutions.ContainsKey(key))
            {
                return solutions[key];
            }
            throw new NullReferenceException($"Solution not found for key '{key}'");
        }
    }
}

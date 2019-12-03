using System;
using System.Collections.Generic;
using AdventOfCode.Domain;

namespace AdventOfCode.Program
{
    class Program
    {
        static void Main(string[] args)
        {
            var solution = GetSolution(2019, 1);
            solution.Run();
        }

        private static Solution GetSolution(int year, int day)
        {

            var solutions = new Dictionary<string, Solution>
            {
                { "2019-1", new _2019.Day1() },
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

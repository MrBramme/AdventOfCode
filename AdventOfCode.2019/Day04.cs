using System;
using System.IO;
using AdventOfCode.Domain;

namespace AdventOfCode._2019
{
    public class Day04 : Solution
    {
        public override void Run(bool test = false)
        {
            var input = File.ReadAllText("Data/2019/04.txt").Split('-');
            var minRange = int.Parse(input[0]);
            var maxRange = int.Parse(input[1]);

            if (test)
            {
                Console.WriteLine($"111111 (should be ok) is: {IsValidNumber(new Password(111111))}");
                Console.WriteLine($"223450 (should be false) is: {IsValidNumber(new Password(223450))}");
                Console.WriteLine($"123789 (should be false) is: {IsValidNumber(new Password(123789))}");
                Console.WriteLine($"123456 (should be false) is: {IsValidNumber(new Password(123456))}");
            } else
            {
                var counter = 0;
                for (var i = minRange; i <= maxRange; i++)
                {
                    if (IsValidNumber(new Password(i)))
                    {
                        counter++;
                    }
                }
                Console.WriteLine($"PART1: {counter} number of valid digits found");
            }
            // Part 2
            if (test)
            {
                Console.WriteLine($"112233 (should be ok) is: {IsValidNumberPart2(new Password(112233))}");
                Console.WriteLine($"123444 (should be false) is: {IsValidNumberPart2(new Password(123444))}");
                Console.WriteLine($"111122 (should be ok)is: {IsValidNumberPart2(new Password(111122))}");
            }
            else
            {
                var counter = 0;
                for (var i = minRange; i <= maxRange; i++)
                {
                    if (IsValidNumberPart2(new Password(i)))
                    {
                        counter++;
                    }
                }
                Console.WriteLine($"PART2: {counter} number of valid digits found");
            }
        }


        private bool IsValidNumber(Password input)
        {
            var result = true;
            if (!IsSixDigits(input)) return false;
            if (!Is2AdjacentSame(input)) return false;
            if (!IsAscending(input)) return false;
            return result;
        }

        private bool IsValidNumberPart2(Password input)
        {
            var result = true;
            if (!IsSixDigits(input)) return false;
            if (!IsMax2AdjacentSame(input)) return false;
            if (!IsAscending(input)) return false;
            return result;
        }

        private bool IsSixDigits(Password input)
        {
            return input.Number >= 100000 && input.Number <= 999999;
        }

        private bool Is2AdjacentSame(Password input)
        {
            for (var i = 0; i < 5; i++)
            {
                if (input.NumArr[i] == input.NumArr[i + 1])
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsMax2AdjacentSame(Password input)
        {
            for (var i = 0; i < 5; i++)
            {
                if (input.NumArr[i] == input.NumArr[i + 1])
                {
                    var prevVal = i == 0 ? int.MinValue : input.NumArr[i - 1];
                    var nextVal = i == 4 ? int.MinValue : input.NumArr[i + 2];
                    if (input.NumArr[i] != prevVal && input.NumArr[i] != nextVal)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool IsAscending(Password input)
        {
            var currentVal = int.MinValue;
            foreach(var i in input.NumArr)
            {
                if (i < currentVal)
                {
                    return false;
                }
                currentVal = i;
            }
            return true;
        }

        private class Password {
            public Password(int number)
            {
                Number = number;
                NumStr = number.ToString();
                NumArr = new int[6];
                for (var i = 0; i < 6; i++)
                {
                    NumArr[i] = int.Parse(NumStr.Substring(i, 1));
                }
            }
            public int Number { get; }
            public string NumStr { get; }
            public int[] NumArr { get; }
        }
    }
}
using System;
using System.IO;
using System.Linq;
using AdventOfCode.Domain;

namespace AdventOfCode._2019
{
    public class Day02 : Solution
    {
        public override void Run(bool test = false)
        {
            var input = "1,9,10,3,2,3,11,0,99,30,40,50";
            if (!test)
            {
                input = File.ReadAllText("Data/2019/02.txt");
            }

            var arr = SetupInput(input, 12, 2);
            var result = CalculateIntCode(arr);

            Console.Write("Result is: ");
            Console.WriteLine(result);

            var noun = 0;
            var verb = 0;
            for ( var i = 0; i <= 99; i++)
            {
                for (var j = 0; j <= 99; j++)
                {
                    var newArr = SetupInput(input, i, j);
                    if (CalculateIntCode(newArr) == 19690720)
                    {
                        noun = i;
                        verb = j;
                        break;
                    }
                }
                if (noun + verb > 0)
                {
                    break;
                }
            }
            Console.WriteLine($"noun: {noun}, verb: {verb}");
            Console.WriteLine($"100*noun+verb= {100 * noun + verb}");
        }

        private int CalculateIntCode(int[] arr)
        {
            var done = false;
            var currentStep = 0;
            while (!done)
            {
                if (arr[currentStep] == 99)
                {
                    done = true;
                    break;
                }
                var a = arr[arr[currentStep + 1]];
                var b = arr[arr[currentStep + 2]];
                var result = arr[currentStep] == 1 ? a + b : a * b;
                arr[arr[currentStep + 3]] = result;
                currentStep += 4;
            }
            return arr[0];
        }

        private int[] SetupInput(string input, int a, int b)
        {
            var arr = input.Split(',').Select(int.Parse).ToArray();
            if (arr.Length > 20) // not for test code
            {
                arr[1] = a;
                arr[2] = b;
            }

            return arr;
        }
    }
}

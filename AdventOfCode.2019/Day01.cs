using System;
using System.IO;
using System.Linq;
using AdventOfCode.Domain;

namespace AdventOfCode._2019
{
    public class Day01 : Solution
    {
        public override void Run(bool test = false)
        {

            var input = File.ReadAllLines("Data/2019/01.txt")
                .Select(int.Parse).ToArray();

            Console.WriteLine($"Total fuel for mass: {GetFuel(input)}");
            Console.WriteLine($"Total fuel: {GetTotalFuelCost(input)}");
        }

        private int GetFuel(int[] input)
        {
            var sum = 0;
            foreach(var i in input)
            {
                sum += i / 3 - 2;
            }
            return sum;
        }

        private int GetTotalFuelCost(int[] input)
        {
            int CalcFuel(int input)
            {
                return input / 3 - 2;
            }

            var sum = 0;
            foreach (var i in input)
            {
                var fuel = CalcFuel(i);
                var remainingCost = fuel;
                while (remainingCost > 0)
                {
                    var fuelCost = CalcFuel(remainingCost);
                    if (fuelCost > 0)
                    {
                        fuel += fuelCost;
                        remainingCost = fuelCost;
                    } else
                    {
                        remainingCost = 0;
                    }
                }
                sum += fuel;
            }
            return sum;
        }
    }
}

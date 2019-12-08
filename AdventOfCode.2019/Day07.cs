using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCode._2019.Helpers;
using AdventOfCode.Domain;

namespace AdventOfCode._2019
{
    public class Day07 : Solution
    {
        // a bit stuck on this one, so learned from https://github.com/alexchro93/AdventOfCode to figure it out.

        public override void Run(bool test = false)
        {
            Console.WriteLine($"Result phase 1: {SolvePhase1(test)}");
            Console.WriteLine($"Result phase 2: {SolvePhase2(test)}");
        }

        private int SolvePhase1(bool test)
        {
            var data = GetData(test);
            var permutations = GetPermutations(new[] { 0, 1, 2, 3, 4 }, 5);
            var maxOutput = int.MinValue;

            foreach (var permutation in permutations)
            {
                var permArr = permutation.ToArray();
                var output = 0;

                for (var t = 0; t < 5; t++)
                {
                    var input = permArr[t];
                    var computer = new IntCodeComputer(data, input, output);
                    while (!computer.Complete)
                        computer.PerformNextInstruction();
                    output = computer.Output;
                }

                if (output > maxOutput)
                {
                    maxOutput = output;
                }
            }

            return maxOutput;

        }

        private int SolvePhase2(bool test)
        {
            var data = GetData(test);
            var permutations = GetPermutations(new[] { 5, 6, 7, 8, 9 }, 5);
            var maxOutput = int.MinValue;

            foreach (var permutation in permutations)
            {
                for (var t = 0; t < 5; t++)
                {
                    var computers = permutation.Select(c => new BlockingIntCodeComputer(data, c)).ToList();
                    computers[0].Input.Add(0);
                    var tasks = new Task[5];

                    for (var i = 0; i < 5; i++)
                    {
                        var c = computers[i];
                        var cNex = computers[(i + 1) % 5];
                        c.OutputProduced += o => cNex.Input.Add(o);
                        var run = Task.Run(() =>
                        {
                            while (!c.Complete)
                                c.PerformNextInstruction();
                        });
                        tasks[i] = run;
                    }

                    Task.WaitAll(tasks);

                    if (computers[4].Output > maxOutput)
                        maxOutput = computers[4].Output;
                }
            }

            return maxOutput;

        }

        private int[] GetData(bool test)
        {
            string data;
            if (test)
            {
                data = "3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0"; 
            }
            else
            {
                data = File.ReadAllText("Data/2019/07.txt");
            }
            
            return data.Split(',').Select(int.Parse).ToArray();
        }

        // Source: https://stackoverflow.com/a/10630026
        private static IEnumerable<IEnumerable<int>> GetPermutations(IEnumerable<int> list, int length)
        {
            if (length == 1) return list.Select(t => new int[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new int[] { t2 }));
        }

       
    }
}
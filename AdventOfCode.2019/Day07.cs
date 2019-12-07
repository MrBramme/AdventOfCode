using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Domain;

namespace AdventOfCode._2019
{
    public class Day07 : Solution
    {
        public override void Run(bool test = false)
        {
            SolvePhase1(test);
        }

        private void SolvePhase1(bool test)
        {
            var input = GetInput(test);
            var permutations = GetPermutations(new[] { 0, 1, 2, 3, 4 }, 5);
        }

        private string[] GetInput(bool test)
        {
            string data;
            if (test)
            {
                data = "3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0"; // expected: 43210
            }
            else
            {
                data = File.ReadAllText("Data/2019/07.txt");
            }
            
            return data.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        }

        // Source: https://stackoverflow.com/a/10630026
        private static IEnumerable<IEnumerable<int>> GetPermutations(IEnumerable<int> list, int length)
        {
            Console.WriteLine($"Perm with length {length}");
            if (length == 1) return list.Select(t => new int[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new int[] { t2 }));
        }

        #region IntCode Program of day 6
        private int CalcIntcode(int[] data, int inputValue)
        {
            var currentStep = 0;
            var done = false;
            var latest = int.MinValue;

            while (!done)
            {
                var opCode = GetOpCode(data[currentStep]);
                var (mode1, mode2, mode3) = GetParamModes(data[currentStep]);
                var a = 0;
                var b = 0;

                if (opCode == OpCode.Add
                    || opCode == OpCode.Multiply
                    || opCode == OpCode.JumpIfTrue
                    || opCode == OpCode.JumpIfFalse
                    || opCode == OpCode.LessThan
                    || opCode == OpCode.Equals)
                {
                    a = mode1 == Mode.Position ? data[data[currentStep + 1]] : data[currentStep + 1];
                    b = mode2 == Mode.Position ? data[data[currentStep + 2]] : data[currentStep + 2];
                }
                switch (opCode)
                {
                    case OpCode.Stop:
                        done = true;
                        break;
                    case OpCode.Add:
                        data[data[currentStep + 3]] = a + b;
                        currentStep += 4;
                        break;
                    case OpCode.Multiply:
                        data[data[currentStep + 3]] = a * b;
                        currentStep += 4;
                        break;
                    case OpCode.Input:
                        data[data[currentStep + 1]] = inputValue;
                        currentStep += 2;
                        break;
                    case OpCode.Output:
                        latest = data[data[currentStep + 1]];
                        currentStep += 2;
                        break;
                    case OpCode.JumpIfTrue:
                        currentStep = a == 0 ? currentStep + 3 : b;
                        break;
                    case OpCode.JumpIfFalse:
                        currentStep = a != 0 ? currentStep + 3 : b;
                        break;
                    case OpCode.LessThan:
                        data[data[currentStep + 3]] = a < b ? 1 : 0;
                        currentStep += 4;
                        break;
                    case OpCode.Equals:
                        data[data[currentStep + 3]] = a == b ? 1 : 0;
                        currentStep += 4;
                        break;
                }
            }

            return latest;
        }

        private (Mode, Mode, Mode) GetParamModes(int input)
        {
            var param1 = input % 1000 > 100 ? Mode.Immediate : Mode.Position;
            var param2 = input % 10000 > 1000 ? Mode.Immediate : Mode.Position;
            var param3 = input % 100000 > 10000 ? Mode.Immediate : Mode.Position;
            return (param1, param2, param3);
        }

        private OpCode GetOpCode(int input)
        {
            // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/arithmetic-operators#code-try-9
            return (OpCode)(input % 100);
        }

        enum OpCode
        {
            Add = 1,
            Multiply = 2,
            Input = 3,
            Output = 4,
            JumpIfTrue = 5,
            JumpIfFalse = 6,
            LessThan = 7,
            Equals = 8,
            Stop = 99,
        }

        enum Mode
        {
            Position = 0,
            Immediate = 1
        }

        #endregion IntCode Program of day 6
    }
}
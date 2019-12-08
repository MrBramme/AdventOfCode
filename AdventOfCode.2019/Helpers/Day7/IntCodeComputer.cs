using System;
namespace AdventOfCode._2019.Helpers
{
    public class IntCodeComputer
    {
        public IntCodeComputer(int[] data, params int[] input)
        {
            Data = new int[data.Length];
            Array.Copy(data, Data, data.Length);
            Input = input;
        }

        private int _inpLoc;
        private bool _complete;

        public int[] Data { get; }
        public int[] Input { get; }
        public int Output { get; private set; }
        public int currentStep { get; private set; }
        public bool Complete => _complete || currentStep >= Data.Length;

        public void PerformNextInstruction()
        {
            if (Complete) return;

            Output = CalcIntcode();
        }

        private int CalcIntcode()
        {
            var latest = int.MinValue;

            while (!_complete)
            {
                var opCode = GetOpCode(Data[currentStep]);
                var (mode1, mode2) = GetParamModes(Data[currentStep]);
                var a = 0;
                var b = 0;

                if (opCode == OpCode.Add
                    || opCode == OpCode.Multiply
                    || opCode == OpCode.JumpIfTrue
                    || opCode == OpCode.JumpIfFalse
                    || opCode == OpCode.LessThan
                    || opCode == OpCode.Equals)
                {
                    a = mode1 == Mode.Position ? Data[Data[currentStep + 1]] : Data[currentStep + 1];
                    b = mode2 == Mode.Position ? Data[Data[currentStep + 2]] : Data[currentStep + 2];
                }
                switch (opCode)
                {
                    case OpCode.Stop:
                        _complete = true;
                        break;
                    case OpCode.Add:
                        Data[Data[currentStep + 3]] = a + b;
                        currentStep += 4;
                        break;
                    case OpCode.Multiply:
                        Data[Data[currentStep + 3]] = a * b;
                        currentStep += 4;
                        break;
                    case OpCode.Input:
                        Data[Data[currentStep + 1]] = Input[_inpLoc++];
                        currentStep += 2;
                        break;
                    case OpCode.Output:
                        latest = Data[Data[currentStep + 1]];
                        currentStep += 2;
                        break;
                    case OpCode.JumpIfTrue:
                        currentStep = a == 0 ? currentStep + 3 : b;
                        break;
                    case OpCode.JumpIfFalse:
                        currentStep = a != 0 ? currentStep + 3 : b;
                        break;
                    case OpCode.LessThan:
                        Data[Data[currentStep + 3]] = a < b ? 1 : 0;
                        currentStep += 4;
                        break;
                    case OpCode.Equals:
                        Data[Data[currentStep + 3]] = a == b ? 1 : 0;
                        currentStep += 4;
                        break;
                }
            }

            return latest;
        }

        private (Mode, Mode) GetParamModes(int input)
        {
            var param1 = input % 1000 > 100 ? Mode.Immediate : Mode.Position;
            var param2 = input % 10000 > 1000 ? Mode.Immediate : Mode.Position;
            return (param1, param2);
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
    }
}

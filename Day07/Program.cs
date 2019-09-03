using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day07
{
    class Program
    {
        //private const string inputFile = @"../../../../input07_test.txt";
        private const string inputFile = @"../../../../input07.txt";
        private static Dictionary<string, Wire> wires = new Dictionary<string, Wire>();

        static void Main(string[] args)
        {
            Console.WriteLine("Day 7");
            Console.WriteLine("Star 1");
            Console.WriteLine();

            string[] lines = File.ReadAllLines(inputFile);
            HashSet<WireValue> operations = new HashSet<WireValue>();

            foreach (string line in lines)
            {
                int arrowIndex = line.IndexOf("->");
                string outputName = line.Substring(arrowIndex + 3);

                WireValue input = null;

                string[] splitInput = line.Substring(0, arrowIndex - 1).Split(' ');

                if (splitInput.Length == 1)
                {
                    //123 -> Y
                    //X -> Y
                    input = GetInput(splitInput[0]);
                }
                else if (splitInput.Length == 2)
                {
                    //NOT X -> Y
                    if (splitInput[0] != "NOT")
                    {
                        throw new Exception();
                    }

                    input = new NotOperation(GetInput(splitInput[1]));
                }
                else if (splitInput[1] == "AND")
                {
                    input = new AndOperation(GetInput(splitInput[0]), GetInput(splitInput[2]));
                }
                else if (splitInput[1] == "OR")
                {
                    input = new OrOperation(GetInput(splitInput[0]), GetInput(splitInput[2]));
                }
                else if (splitInput[1] == "RSHIFT")
                {
                    input = new RShiftOperation(GetInput(splitInput[0]), int.Parse(splitInput[2]));
                }
                else if (splitInput[1] == "LSHIFT")
                {
                    input = new LShiftOperation(GetInput(splitInput[0]), int.Parse(splitInput[2]));
                }
                else
                {
                    throw new Exception();
                }

                SafeGetWire(outputName).input = input;

                operations.Add(input);
            }

            Wire wireA = SafeGetWire("a");

            int output1 = wireA.GetValue();

            Console.WriteLine($"The answer is: {output1}");



            Console.WriteLine();
            Console.WriteLine("Star 2");
            Console.WriteLine();

            //Reset cached values
            foreach (WireValue wireValue in wires.Values)
            {
                wireValue.Reset();
            }

            foreach (WireValue wireValue in operations)
            {
                wireValue.Reset();
            }


            wires["b"].input = new FixedWireValue(output1);


            Console.WriteLine($"The answer is: {wireA.GetValue()}");

            Console.WriteLine();
            Console.ReadKey();
        }

        private static WireValue GetInput(string value)
        {
            if (char.IsDigit(value[0]))
            {
                return new FixedWireValue(int.Parse(value));
            }

            return SafeGetWire(value);
        }

        private static Wire SafeGetWire(string wireName)
        {
            if (!wires.ContainsKey(wireName))
            {
                wires[wireName] = new Wire(wireName);
            }

            return wires[wireName];
        }

        public abstract class WireValue
        {
            protected abstract int CalculateValue();

            private int cachedValue = -1;
            public int GetValue()
            {
                if (cachedValue == -1)
                {
                    cachedValue = CalculateValue();
                }

                return cachedValue;
            }

            public void Reset() => cachedValue = -1;
        }

        public class FixedWireValue : WireValue
        {
            public readonly int value;

            public FixedWireValue(int value)
            {
                this.value = value;
            }

            protected override int CalculateValue() => value;
        }

        public class RShiftOperation : WireValue
        {
            public readonly WireValue input;
            public readonly int rshift;

            public RShiftOperation(WireValue input, int rshift)
            {
                this.input = input;
                this.rshift = rshift;
            }

            protected override int CalculateValue() => input.GetValue() >> rshift;
        }

        public class LShiftOperation : WireValue
        {
            public readonly WireValue input;
            public readonly int lshift;

            public LShiftOperation(WireValue input, int lshift)
            {
                this.input = input;
                this.lshift = lshift;
            }

            protected override int CalculateValue() => (input.GetValue() << lshift) & 0xffff;
        }

        public class NotOperation : WireValue
        {
            public readonly WireValue input;

            public NotOperation(WireValue input)
            {
                this.input = input;
            }

            protected override int CalculateValue() => (~input.GetValue()) & 0xffff;
        }

        public class OrOperation : WireValue
        {
            public readonly WireValue inputA;
            public readonly WireValue inputB;

            public OrOperation(WireValue inputA, WireValue inputB)
            {
                this.inputA = inputA;
                this.inputB = inputB;
            }

            protected override int CalculateValue() => inputA.GetValue() | inputB.GetValue();
        }

        public class AndOperation : WireValue
        {
            public readonly WireValue inputA;
            public readonly WireValue inputB;

            public AndOperation(WireValue inputA, WireValue inputB)
            {
                this.inputA = inputA;
                this.inputB = inputB;
            }

            protected override int CalculateValue() => inputA.GetValue() & inputB.GetValue();
        }

        public class Wire : WireValue
        {
            public readonly string name;
            public WireValue input = null;

            public Wire(string name)
            {
                this.name = name;
            }

            protected override int CalculateValue() => input?.GetValue() ?? 0;
        }
    }
}

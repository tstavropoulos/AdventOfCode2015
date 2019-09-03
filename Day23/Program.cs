using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace Day23
{
    class Program
    {
        private const string inputFile = @"../../../../input23.txt";

        static void Main(string[] args)
        {
            Console.WriteLine("Day 23");
            Console.WriteLine("Star 1");
            Console.WriteLine();

            string[] lines = File.ReadAllLines(inputFile);

            List<Instr> instructions = lines.Select(Parse).ToList();

            BigInteger a = 0;
            BigInteger b = 0;
            int instr = 0;

            while (instr >= 0 && instr < instructions.Count)
            {
                instructions[instr].Execute(ref a, ref b, ref instr);
            }

            Console.WriteLine($"The answer is: {b}");

            Console.WriteLine();
            Console.WriteLine("Star 2");
            Console.WriteLine();


            a = 1;
            b = 0;
            instr = 0;

            while (instr >= 0 && instr < instructions.Count)
            {
                instructions[instr].Execute(ref a, ref b, ref instr);
            }

            Console.WriteLine($"The answer is: {b}");


            Console.WriteLine();
            Console.ReadKey();
        }

        static Instr Parse(string line)
        {
            if (line[0] == 'h')
            {
                return new HalfR(line);
            }

            if (line[0] == 't')
            {
                return new TplR(line);
            }

            if (line[0] == 'i')
            {
                return new IncR(line);
            }

            if (line[1] == 'm')
            {
                return new Jmp(line);
            }

            if (line[2] == 'e')
            {
                return new JieR(line);
            }

            return new JioR(line);
        }

        abstract class Instr
        {
            public abstract void Execute(ref BigInteger a, ref BigInteger b, ref int instr);
        }

        class HalfR : Instr
        {
            private readonly bool RegA;

            public HalfR(string line)
            {
                RegA = line.Split(' ')[1] == "a";
            }

            public override void Execute(ref BigInteger a, ref BigInteger b, ref int instr)
            {
                if (RegA)
                {
                    a /= 2;
                }
                else
                {
                    b /= 2;
                }

                instr++;
            }
        }

        class TplR : Instr
        {
            private readonly bool RegA;

            public TplR(string line)
            {
                RegA = line.Split(' ')[1] == "a";
            }

            public override void Execute(ref BigInteger a, ref BigInteger b, ref int instr)
            {
                if (RegA)
                {
                    a *= 3;
                }
                else
                {
                    b *= 3;
                }

                instr++;
            }
        }

        class IncR : Instr
        {
            private readonly bool RegA;

            public IncR(string line)
            {
                RegA = line.Split(' ')[1] == "a";
            }

            public override void Execute(ref BigInteger a, ref BigInteger b, ref int instr)
            {
                if (RegA)
                {
                    a++;
                }
                else
                {
                    b++;
                }

                instr++;
            }
        }

        class Jmp : Instr
        {
            private readonly int jump;

            public Jmp(string line)
            {
                jump = int.Parse(line.Split(' ')[1]);
            }

            public override void Execute(ref BigInteger a, ref BigInteger b, ref int instr)
            {
                instr += jump;
            }
        }

        class JieR : Instr
        {
            private readonly bool RegA;
            private readonly int jump;
            public JieR(string line)
            {
                string[] splitLine = line.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                RegA = splitLine[1] == "a";
                jump = int.Parse(splitLine[2]);
            }

            public override void Execute(ref BigInteger a, ref BigInteger b, ref int instr)
            {
                if (RegA && a%2 == 0)
                {
                    instr += jump;
                }
                else if (!RegA && b%2 == 0)
                {
                    instr += jump;
                }
                else
                {
                    instr++;
                }
            }
        }

        class JioR : Instr
        {
            private readonly bool RegA;
            private readonly int jump;
            public JioR(string line)
            {
                string[] splitLine = line.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                RegA = splitLine[1] == "a";
                jump = int.Parse(splitLine[2]);
            }

            public override void Execute(ref BigInteger a, ref BigInteger b, ref int instr)
            {
                if (RegA && a == 1)
                {
                    instr += jump;
                }
                else if (!RegA && b == 1)
                {
                    instr += jump;
                }
                else
                {
                    instr++;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day01
{
    class Program
    {
        private const string inputFile = @"../../../../input01.txt";

        static void Main(string[] args)
        {
            Console.WriteLine("Day 1");
            Console.WriteLine("Star 1");
            Console.WriteLine();


            string input = File.ReadAllText(inputFile);

            int floor = 0;
            int firstBasement = int.MaxValue;
            for (int i = 0; i < input.Length; i++)
            {
                switch (input[i])
                {
                    case '(':
                        floor++;
                        break;

                    case ')':
                        floor--;
                        break;

                    default:
                        throw new Exception();
                }


                if (floor == -1 && i < firstBasement)
                {
                    firstBasement = i + 1;
                }
            }

            Console.WriteLine($"Ending Floor: {floor}");

            Console.WriteLine();
            Console.WriteLine("Star 2");
            Console.WriteLine();

            Console.WriteLine($"First Basement: {firstBasement}");

            Console.WriteLine();
            Console.ReadKey();
        }
    }
}

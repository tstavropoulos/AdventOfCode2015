using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day10
{
    class Program
    {
        private const string inputFile = @"../../../../input10.txt";

        static void Main(string[] args)
        {
            Console.WriteLine("Day 10");
            Console.WriteLine("Star 1");
            Console.WriteLine();

            string input = File.ReadAllText(inputFile);

            //This puzzle is the see and speak thing...

            for (int i = 0; i < 40; i++)
            {
                input = Speak(Read(input));
            }

            int output1 = input.Length;



            Console.WriteLine($"The answer is: {output1}");

            Console.WriteLine();
            Console.WriteLine("Star 2");
            Console.WriteLine();


            for (int i = 0; i < 10; i++)
            {
                input = Speak(Read(input));
            }

            int output2 = input.Length;

            Console.WriteLine($"The answer is: {output2}");

            Console.WriteLine();
            Console.ReadKey();
        }

        private static List<SeriesElement> Read(string input)
        {
            List<SeriesElement> elements = new List<SeriesElement>();
            char initialDigit;
            int count;

            int index = 0;

            while (index < input.Length)
            {
                initialDigit = input[index];
                count = 1;

                while (++index < input.Length && initialDigit == input[index])
                {
                    count++;
                }

                elements.Add(new SeriesElement(int.Parse(initialDigit.ToString()), count));
            }

            return elements;
        }

        private static string Speak(IEnumerable<SeriesElement> elements)
        {
            StringBuilder builder = new StringBuilder();

            foreach(SeriesElement element in elements)
            {
                builder.Append(element.repetitions);
                builder.Append(element.digit);
            }

            return builder.ToString();
        }

        public readonly struct SeriesElement
        {
            public readonly int digit;
            public readonly int repetitions;

            public SeriesElement(int digit, int repetitions)
            {
                this.digit = digit;
                this.repetitions = repetitions;
            }
        }
    }
}

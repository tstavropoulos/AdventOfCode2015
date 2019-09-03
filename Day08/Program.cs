using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day08
{
    class Program
    {
        private const string inputFile = @"../../../../input08.txt";

        static void Main(string[] args)
        {
            Console.WriteLine("Day 8");
            Console.WriteLine("Star 1");
            Console.WriteLine();

            string[] lines = File.ReadAllLines(inputFile);

            //How many wasted characters on each line?

            string[] parsedLines = lines.Select(Parse).ToArray();

            int originalLengths = lines.Select(x => x.Length).Sum();
            int parsedLengths = parsedLines.Select(x => x.Length).Sum();

            int output1 = originalLengths - parsedLengths;

            Console.WriteLine($"The answer is: {output1}");

            Console.WriteLine();
            Console.WriteLine("Star 2");
            Console.WriteLine();



            string[] encodedLines = lines.Select(Encode).ToArray();

            int encodedLengths = encodedLines.Select(x => x.Length).Sum();
            int output2 = encodedLengths - originalLengths;

            Console.WriteLine($"The answer is: {output2}");


            Console.WriteLine();
            Console.ReadKey();
        }

        public static string Parse(string input)
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 1; i < input.Length - 1; i++)
            {
                if (input[i] == '\\')
                {
                    if (input[++i] == 'x')
                    {
                        string code = input.Substring(++i, 2);
                        stringBuilder.Append((char)Convert.ToByte(code, 16));
                        i++;
                    }
                    else
                    {
                        stringBuilder.Append(input[i]);
                    }
                }
                else
                {
                    stringBuilder.Append(input[i]);
                }
            }

            return stringBuilder.ToString();
        }

        public static string Encode(string input)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append('"');

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '\\')
                {
                    stringBuilder.Append("\\\\");
                }
                else if (input[i] == '"')
                {
                    stringBuilder.Append("\\\"");
                }
                else if (char.IsLetterOrDigit(input[i]))
                {
                    stringBuilder.Append(input[i]);
                }
                else
                {
                    stringBuilder.Append("\\x##");
                }
            }

            stringBuilder.Append('"');

            return stringBuilder.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day19
{
    class Program
    {
        private const string inputFile = @"../../../../input19.txt";

        static void Main(string[] args)
        {
            Console.WriteLine("Day 19");
            Console.WriteLine("Star 1");
            Console.WriteLine();

            HashSet<string> outputs = new HashSet<string>();

            string[] lines = File.ReadAllLines(inputFile);

            List<Replacement> replacements = lines.Where(x => x.Contains(" => ")).Select(x => new Replacement(x)).ToList();

            string input = lines.Last();

            foreach (Replacement rep in replacements)
            {
                foreach (string output in rep.GetOutputs(input))
                {
                    outputs.Add(output);
                }
            }

            Console.WriteLine($"Number of unique outputs: {outputs.Count}");

            Console.WriteLine();
            Console.WriteLine("Star 2");
            Console.WriteLine();

            //Calculation in "input19_Cleaned.txt"

            ////Drop the Electron ones, they have to be last:
            //replacements.RemoveRange(replacements.Count - 3, 3);

            ////Lets actually just reverse the building process.
            ////Every (reverse) step actually shortens the sequence

            //Dictionary<string, int> stepDict = new Dictionary<string, int>();

            //stepDict[input] = 0;

            //int steps = 0;
            //bool done = false;

            //while(true)
            //{
            //    string[] patterns = stepDict.Where(x => x.Value == steps).Select(x=>x.Key).ToArray();

            //    steps++;

            //    Console.WriteLine($"Step: {steps}  Patterns: {patterns.Length}");
            //    foreach (string pattern in patterns)
            //    {
            //        foreach (Replacement replacement in replacements)
            //        {
            //            foreach(string newPattern in replacement.GetReverseInputs(pattern))
            //            {
            //                if (!stepDict.ContainsKey(newPattern))
            //                {
            //                    stepDict[newPattern] = steps;

            //                    if (newPattern == "HF" || newPattern == "ND" || newPattern == "OG")
            //                    {
            //                        stepDict["e"] = steps + 1;
            //                        done = true;
            //                        break;
            //                    }
            //                }
            //            }

            //            if (done)
            //            {
            //                break;
            //            }
            //        }

            //        if (done)
            //        {
            //            break;
            //        }
            //    }

            //    if (done)
            //    {
            //        break;
            //    }
            //}

            //Console.WriteLine($"The answer is: {stepDict["e"]}");


            Console.WriteLine();
            Console.ReadKey();
        }

        readonly struct Replacement
        {
            private readonly string input;
            private readonly string output;

            private readonly Regex regex;
            private readonly Regex reverseRegex;

            public Replacement(string line)
            {
                string[] splitLines = line.Split(" => ");

                input = splitLines[0];
                output = splitLines[1];

                regex = new Regex($@"({input})");
                reverseRegex = new Regex($@"({output})");
            }

            public IEnumerable<string> GetOutputs(string input)
            {
                MatchCollection matches = regex.Matches(input);

                foreach (Match m in matches)
                {
                    yield return regex.Replace(input, output, 1, m.Index);
                }
            }

            public IEnumerable<string> GetReverseInputs(string output)
            {
                MatchCollection matches = reverseRegex.Matches(output);

                foreach (Match m in matches)
                {
                    yield return reverseRegex.Replace(output, input, 1, m.Index);
                }
            }
        }
    }

}

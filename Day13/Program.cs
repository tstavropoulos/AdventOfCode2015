using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day13
{
    class Program
    {
        private const string inputFile = @"../../../../input13.txt";

        private const string YOU = "you";

        private static List<string> peopleList;
        private static readonly Dictionary<(string, string), int> mapping = new Dictionary<(string, string), int>();
        static void Main(string[] args)
        {
            Console.WriteLine("Day 13");
            Console.WriteLine("Star 1");
            Console.WriteLine();

            string[] lines = File.ReadAllLines(inputFile);
            HashSet<string> peopleSet = new HashSet<string>();

            foreach (string line in lines)
            {
                string[] splitLine = line.Split(' ');
                string target = splitLine[0];
                string neighbor = splitLine[10].Substring(0, splitLine[10].Length - 1);
                int value = int.Parse(splitLine[3]);

                if (splitLine[2] == "lose")
                {
                    value = -value;
                }

                peopleSet.Add(target);
                mapping[(target, neighbor)] = value;
            }

            peopleList = new List<string>(peopleSet);

            int maxHappiness = GetHappiness(peopleList.First(), peopleList.First(), peopleList.Skip(1)).Max();

            Console.WriteLine($"The maximum happiness is: {maxHappiness}");

            Console.WriteLine();
            Console.WriteLine("Star 2");
            Console.WriteLine();


            foreach(string person in peopleList)
            {
                mapping[(person, YOU)] = 0;
                mapping[(YOU, person)] = 0;
            }

            peopleList.Add(YOU);

            maxHappiness = GetHappiness(peopleList.First(), peopleList.First(), peopleList.Skip(1)).Max();

            Console.WriteLine($"The answer is: {maxHappiness}");

            Console.WriteLine();
            Console.ReadKey();
        }

        //This solution is fast and easy, and just increases our work by a factor of 2 over the
        //theoretical minimum of unique comparisons (it doesn't omit reversals)
        private static IEnumerable<int> GetHappiness(string firstSeated, string lastSeated, IEnumerable<string> remaining)
        {
            int remainingCount = remaining.Count();

            if (remainingCount == 1)
            {
                string finalSeated = remaining.First();

                yield return mapping[(lastSeated, finalSeated)] +
                    mapping[(finalSeated, lastSeated)] +
                    mapping[(finalSeated, firstSeated)] +
                    mapping[(firstSeated, finalSeated)];
                yield break;
            }

            for (int i = 0; i < remainingCount; i++)
            {
                string guest = remaining.ElementAt(i);
                int seatingValue = mapping[(lastSeated, guest)] + mapping[(guest, lastSeated)];

                foreach (int happinessValue in GetHappiness(firstSeated, guest, remaining.Where((_, j) => j != i)))
                {
                    yield return seatingValue + happinessValue;
                }
            }
        }
    }
}

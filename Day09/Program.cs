using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day09
{
    class Program
    {
        private const string inputFile = @"../../../../input09.txt";
        private static Dictionary<(string, string), int> distanceMap;

        static void Main(string[] args)
        {
            Console.WriteLine("Day 9");
            Console.WriteLine("Star 1");
            Console.WriteLine();

            //LOL
            //Traveling salesman problem

            HashSet<string> cities = new HashSet<string>();
            distanceMap = new Dictionary<(string, string), int>();

            string[] lines = File.ReadAllLines(inputFile);

            foreach (string line in lines)
            {
                string[] splitLine = line.Split(' ');
                string city1 = splitLine[0];
                string city2 = splitLine[2];
                int distance = int.Parse(splitLine[4]);

                cities.Add(city1);
                cities.Add(city2);

                distanceMap[(city1, city2)] = distance;
                distanceMap[(city2, city1)] = distance;
            }

            int minDistance = FindMinPath(cities);

            Console.WriteLine($"The answer is: {minDistance}");

            Console.WriteLine();
            Console.WriteLine("Star 2");
            Console.WriteLine();

            int maxDistance = FindMaxPath(cities);

            Console.WriteLine($"The answer is: {maxDistance}");


            Console.WriteLine();
            Console.ReadKey();
        }

        public static int FindMinPath(IEnumerable<string> cities)
        {
            string[] citiesCopy = cities.ToArray();

            int minDistance = int.MaxValue;

            //The list is reversible.
            //That means I can cut **1** element off the initial element check
            for (int i = 0; i < citiesCopy.Length - 1; i++)
            {
                minDistance = Math.Min(minDistance, FindMinPath(
                    lastCity: citiesCopy[i],
                    remainingCities: citiesCopy.Take(i).Concat(citiesCopy.Skip(i + 1))));
            }

            return minDistance;
        }

        public static int FindMinPath(string lastCity, IEnumerable<string> remainingCities)
        {
            if (remainingCities.Count() == 1)
            {
                return distanceMap[(lastCity, remainingCities.First())];
            }

            int minDistance = int.MaxValue;

            foreach (string city in remainingCities)
            {
                minDistance = Math.Min(minDistance, distanceMap[(lastCity, city)] + FindMinPath(
                    lastCity: city,
                    remainingCities: remainingCities.Where(x => x != city)));
            }

            return minDistance;
        }

        public static int FindMaxPath(IEnumerable<string> cities)
        {
            string[] citiesCopy = cities.ToArray();

            int maxDistance = 0;

            //The list is reversible.
            //That means I can cut **1** element off the initial element check
            for (int i = 0; i < citiesCopy.Length - 1; i++)
            {
                maxDistance = Math.Max(maxDistance, FindMaxPath(
                    lastCity: citiesCopy[i],
                    remainingCities: citiesCopy.Take(i).Concat(citiesCopy.Skip(i + 1))));
            }

            return maxDistance;
        }

        public static int FindMaxPath(string lastCity, IEnumerable<string> remainingCities)
        {
            if (remainingCities.Count() == 1)
            {
                return distanceMap[(lastCity, remainingCities.First())];
            }

            int maxDistance = 0;

            foreach (string city in remainingCities)
            {
                maxDistance = Math.Max(maxDistance, distanceMap[(lastCity, city)] + FindMaxPath(
                    lastCity: city,
                    remainingCities: remainingCities.Where(x => x != city)));
            }

            return maxDistance;
        }
    }
}

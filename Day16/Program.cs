using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day16
{
    class Program
    {
        private const string inputFile = @"../../../../input16.txt";

        private static readonly char[] separators = new char[] { ' ', ':', ',' };

        static void Main(string[] args)
        {
            Console.WriteLine("Day 16");
            Console.WriteLine("Star 1");
            Console.WriteLine();

            string[] lines = File.ReadAllLines(inputFile);

            List<Sue> sues = lines.Select(x => new Sue(x)).ToList();

            int output1 = 0;

            HashSet<string> features = new HashSet<string>()
            {
                "children: 3",
                "cats: 7",
                "samoyeds: 2",
                "pomeranians: 3",
                "akitas: 0",
                "vizslas: 0",
                "goldfish: 5",
                "trees: 3",
                "cars: 2",
                "perfumes: 1"
            };

            foreach (Sue sue in sues)
            {
                if (features.Contains(sue.featureA) &&
                    features.Contains(sue.featureB) &&
                    features.Contains(sue.featureC))
                {
                    Console.WriteLine($"Candidate Sue: {sue.number}");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Star 2");
            Console.WriteLine();



            List<Sue2> newSues = lines.Select(x => new Sue2(x)).ToList();

            List<(Feature, int)> measurements = new List<(Feature, int)>()
            {
                (Feature.Children, 3),
                (Feature.Cats, 7),
                (Feature.Samoyeds, 2),
                (Feature.Pomeranians, 3),
                (Feature.Akitas, 0),
                (Feature.Vizslas, 0),
                (Feature.Goldfish, 5),
                (Feature.Trees, 3),
                (Feature.Cars, 2),
                (Feature.Perfumes, 1)
            };

            foreach(Sue2 sue in newSues)
            {
                if (measurements.Select(x=>sue.IsConsistentWith(x.Item1, x.Item2)).All(x=>x))
                {
                    Console.WriteLine($"Candidate Sue: {sue.number}");
                }
            }

            Console.WriteLine();
            Console.ReadKey();
        }

        readonly struct Sue
        {
            public readonly int number;
            public readonly string featureA;
            public readonly string featureB;
            public readonly string featureC;

            public Sue(string line)
            {
                string[] splitLine = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                number = int.Parse(splitLine[1]);
                featureA = $"{splitLine[2]}: {splitLine[3]}";
                featureB = $"{splitLine[4]}: {splitLine[5]}";
                featureC = $"{splitLine[6]}: {splitLine[7]}";
            }
        }

        enum Feature
        {
            Children,
            Cats,
            Samoyeds,
            Pomeranians,
            Akitas,
            Vizslas,
            Goldfish,
            Trees,
            Cars,
            Perfumes
        }

        readonly struct Sue2
        {
            public readonly int number;
            public readonly Feature featureA;
            public readonly int countA;
            public readonly Feature featureB;
            public readonly int countB;
            public readonly Feature featureC;
            public readonly int countC;

            public Sue2(string line)
            {
                string[] splitLine = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                number = int.Parse(splitLine[1]);
                featureA = ParseFeature(splitLine[2]);
                countA = int.Parse(splitLine[3]);
                featureB = ParseFeature(splitLine[4]);
                countB = int.Parse(splitLine[5]);
                featureC = ParseFeature(splitLine[6]);
                countC = int.Parse(splitLine[7]);
            }

            public bool IsConsistentWith(Feature feature, int count)
            {
                int sueCount;
                if (feature == featureA)
                {
                    sueCount = countA;
                }
                else if (feature == featureB)
                {
                    sueCount = countB;
                }
                else if (feature == featureC)
                {
                    sueCount = countC;
                }
                else
                {
                    return true;
                }

                switch (feature)
                {
                    case Feature.Cats:
                    case Feature.Trees:
                        return sueCount > count;

                    case Feature.Pomeranians:
                    case Feature.Goldfish:
                        return sueCount < count;

                    case Feature.Children:
                    case Feature.Samoyeds:
                    case Feature.Akitas:
                    case Feature.Vizslas:
                    case Feature.Cars:
                    case Feature.Perfumes:
                        return sueCount == count;

                    default:
                        throw new Exception();
                }

            }
        }

        private static Feature ParseFeature(string feature)
        {
            switch (feature)
            {
                case "children": return Feature.Children;
                case "cats": return Feature.Cats;
                case "samoyeds": return Feature.Samoyeds;
                case "pomeranians": return Feature.Pomeranians;
                case "akitas": return Feature.Akitas;
                case "vizslas": return Feature.Vizslas;
                case "goldfish": return Feature.Goldfish;
                case "trees": return Feature.Trees;
                case "cars": return Feature.Cars;
                case "perfumes": return Feature.Perfumes;

                default: throw new Exception();
            }
        }
    }
}

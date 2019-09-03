using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day02
{
    class Program
    {
        private const string inputFile = @"../../../../input02.txt";

        static void Main(string[] args)
        {
            Console.WriteLine("Day 2");
            Console.WriteLine("Star 1");
            Console.WriteLine();

            List<(long x, long y, long z)> dimensions = File.ReadAllLines(inputFile).Select(Parse).ToList();

            long totalSize = dimensions.Select(num => 3 * num.x * num.y + 2 * num.z * (num.x + num.y)).Sum();

            Console.WriteLine($"The total size is: {totalSize}");

            Console.WriteLine();
            Console.WriteLine("Star 2");
            Console.WriteLine();

            long totalRibbon = dimensions.Select(num => 2 * (num.x + num.y) + num.x * num.y * num.z).Sum();

            Console.WriteLine($"The total amount of ribbon is: {totalRibbon}");

            Console.WriteLine();
            Console.ReadKey();
        }

        static (long x, long y, long z) Parse(string line)
        {
            long[] splitLine = line.Split('x').Select(long.Parse).OrderBy(x=>x).ToArray();

            return (splitLine[0], splitLine[1], splitLine[2]);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day24
{
    class Program
    {
        private const string inputFile = @"../../../../input24.txt";

        static void Main(string[] args)
        {
            Console.WriteLine("Day 24");
            Console.WriteLine("Star 1");
            Console.WriteLine();

            int[] weights = File.ReadAllLines(inputFile).Select(int.Parse).OrderByDescending(x => x).ToArray();

            //Split packages into three groups of equal weight
            //Find the arrangement with the fewest possible packages in group 1
            //Further, select packages by minimizing the product of their weights

            int partialWeight = weights.Sum() / 3;

            IEnumerable<int[]> arrangements = GetArrangements(
                selections: new Stack<int>(),
                remainingWeight: partialWeight,
                weights: weights,
                index: 0).ToArray();

            int shortestLength = arrangements.Min(x => x.Length);

            long output1 = arrangements
                .Where(x => x.Length == shortestLength)
                .Min(x => x.Aggregate(1L, (y, z) => y * (long)z));

            Console.WriteLine($"The answer is: {output1}");

            Console.WriteLine();
            Console.WriteLine("Star 2");
            Console.WriteLine();


            partialWeight = weights.Sum() / 4;

            arrangements = GetArrangements(
                selections: new Stack<int>(),
                remainingWeight: partialWeight,
                weights: weights,
                index: 0).ToArray();

            shortestLength = arrangements.Min(x => x.Length);

            long output2 = arrangements
                .Where(x => x.Length == shortestLength)
                .Min(x => x.Aggregate(1L, (y, z) => y * (long)z));



            Console.WriteLine($"The answer is: {output2}");


            Console.WriteLine();
            Console.ReadKey();
        }

        static IEnumerable<int[]> GetArrangements(
            Stack<int> selections,
            int remainingWeight,
            int[] weights,
            int index)
        {
            if (remainingWeight == 0)
            {
                yield return selections.ToArray();
                yield break;
            }

            for (int i = index; i < weights.Length; i++)
            {
                if (weights[i] <= remainingWeight)
                {
                    selections.Push(weights[i]);

                    foreach (int[] result in GetArrangements(
                        selections: selections,
                        remainingWeight: remainingWeight - weights[i],
                        weights: weights,
                        index: i + 1))
                    {
                        yield return result;
                    }

                    selections.Pop();
                }
            }

        }

    }
}

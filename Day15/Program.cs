using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day15
{
    class Program
    {
        private const string inputFile = @"../../../../input15.txt";

        private static readonly char[] splitChars = new char[] { ' ', ',', ':' };

        static void Main(string[] args)
        {
            Console.WriteLine("Day 15");
            Console.WriteLine("Star 1");
            Console.WriteLine();

            string[] lines = File.ReadAllLines(inputFile);

            Ingredient[] ingredients = lines.Select(x => new Ingredient(x)).ToArray();

            //Total of 100 ingredients
            //Score is the product of the clamped total Capacity, Durability, Flavor, and Texture

            //Lets try a naive maximum search approach.
            //Start with split everything, take a step in the direction that yields the biggest increase

            int[] counts = new int[ingredients.Length];

            for (int i = 0; i < counts.Length; i++)
            {
                counts[i] = 100 / counts.Length;
            }

            //Put remainder in the last one
            counts[counts.Length - 1] += 100 % counts.Length;

            long currentScore = GetScore(ingredients, counts);
            (int up, int down, long score) bestMove;

            while (true)
            {
                bestMove = (-1, -1, currentScore);

                for (int up = 0; up < counts.Length; up++)
                {
                    for (int down = 0; down < counts.Length; down++)
                    {
                        if (up == down)
                        {
                            continue;
                        }

                        if (counts[down] == 0)
                        {
                            //can't go lower
                            continue;
                        }

                        //Make change
                        counts[up]++;
                        counts[down]--;

                        long testScore = GetScore(ingredients, counts);

                        if (testScore > bestMove.score)
                        {
                            bestMove = (up, down, testScore);
                        }

                        //Reverse change
                        counts[up]--;
                        counts[down]++;
                    }
                }

                if (bestMove.up == -1)
                {
                    break;
                }

                counts[bestMove.up]++;
                counts[bestMove.down]--;

                currentScore = bestMove.score;
            }

            Console.WriteLine($"The best score is: {currentScore}");

            Console.WriteLine();
            Console.WriteLine("Star 2");
            Console.WriteLine();

            //Now i need the best score with only 500 calories...
            //There might be very limited solutions...  just test them

            //1.         5a + 8b + 6c + d = 500
            //2.            a + b + c + d = 100
            //2.          100 - a - b - c = d
            //1. <- 2.       4a + 7b + 5c = 400

            int a1 = 400 / 4;
            int b1 = 400 / 7;
            int c1 = 400 / 5;

            long bestScore = 0;

            for (int a = 0; a <= a1; a++)
            {
                for (int b = 0; b <= b1; b++)
                {
                    for (int c = 0; c <= c1; c++)
                    {
                        if (a + b + c > 100)
                        {
                            // negative Sugar
                            break;
                        }

                        int total = 4 * a + 7 * b + 5 * c;

                        if (total == 400)
                        {
                            counts[0] = a;
                            counts[1] = b;
                            counts[2] = c;
                            counts[3] = 100 - (a + b + c);

                            long newScore = GetScore(ingredients, counts);

                            if (newScore > bestScore)
                            {
                                bestScore = newScore;
                            }
                        }
                        else if (total > 400)
                        {
                            //past threshold
                            break;
                        }
                    }
                }
            }

            Console.WriteLine($"The best diet cookie is: {bestScore}");

            Console.WriteLine();
            Console.ReadKey();
        }

        private static long GetScore(Ingredient[] ingredients, int[] counts)
        {
            long capacity = Math.Max(0, ingredients.Zip(counts, (x, y) => x.capacity * y).Sum());
            long durability = Math.Max(0, ingredients.Zip(counts, (x, y) => x.durability * y).Sum());
            long flavor = Math.Max(0, ingredients.Zip(counts, (x, y) => x.flavor * y).Sum());
            long texture = Math.Max(0, ingredients.Zip(counts, (x, y) => x.texture * y).Sum());

            return capacity * durability * flavor * texture;
        }

        readonly struct Ingredient
        {
            public readonly string name;
            public readonly long capacity;
            public readonly long durability;
            public readonly long flavor;
            public readonly long texture;
            public readonly long calories;

            public Ingredient(string line)
            {
                string[] splitLine = line.Split(splitChars, StringSplitOptions.RemoveEmptyEntries);

                name = splitLine[0];
                capacity = long.Parse(splitLine[2]);
                durability = long.Parse(splitLine[4]);
                flavor = long.Parse(splitLine[6]);
                texture = long.Parse(splitLine[8]);
                calories = long.Parse(splitLine[10]);
            }
        }
    }
}

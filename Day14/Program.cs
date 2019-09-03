using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day14
{
    class Program
    {
        private const string inputFile = @"../../../../input14.txt";

        static void Main(string[] args)
        {
            Console.WriteLine("Day 14");
            Console.WriteLine("Star 1");
            Console.WriteLine();

            string[] lines = File.ReadAllLines(inputFile);

            List<Reindeer> reindeer = lines.Select(x => new Reindeer(x)).ToList();

            int farthestReindeer = reindeer.Select(x => x.GetDistance(2503)).Max();

            Console.WriteLine($"The farthest reindeer is: {farthestReindeer}");

            Console.WriteLine();
            Console.WriteLine("Star 2");
            Console.WriteLine();


            for (int i = 1; i <= 2503; i++)
            {
                reindeer.OrderByDescending(x => x.GetDistance(i)).First().score++;
            }

            Console.WriteLine($"The answer is: {reindeer.OrderByDescending(x => x.score).First().score}");


            Console.WriteLine();
            Console.ReadKey();
        }

        class Reindeer
        {
            public readonly string name;
            public readonly int speed;
            public readonly int run;
            public readonly int rest;

            public int score;

            public Reindeer(string line)
            {
                string[] splitLine = line.Split(' ');

                name = splitLine[0];
                speed = int.Parse(splitLine[3]);
                run = int.Parse(splitLine[6]);
                rest = int.Parse(splitLine[13]);

                score = 0;
            }

            public int GetDistance(int time)
            {
                int periods = time / (run + rest);

                int remainder = Math.Min(time % (run + rest), run);

                return (periods * run + remainder) * speed;
            }
        }
    }
}

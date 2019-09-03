using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day18
{
    class Program
    {
        private const string inputFile = @"../../../../input18.txt";

        static void Main(string[] args)
        {
            Console.WriteLine("Day 18");
            Console.WriteLine("Star 1");
            Console.WriteLine();

            // 100 x 100 grid of lights
            // # is on
            // . is off

            // This is exactly the game of life

            //A light which is on stays on when 2 or 3 neighbors are on, and turns off otherwise.
            //A light which is off turns on if exactly 3 neighbors are on, and stays off otherwise.

            //How many lights are on after 100 steps?

            //Let's do page flipping...

            bool[,] currentState = new bool[100, 100];
            bool[,] nextState = new bool[100, 100];


            string[] lines = File.ReadAllLines(inputFile);

            for (int y = 0; y < 100; y++)
            {
                for (int x = 0; x < 100; x++)
                {
                    currentState[x, y] = lines[x][y] != '.';
                }
            }

            for (int gen = 0; gen < 100; gen++)
            {
                for (int y = 0; y < 100; y++)
                {
                    for (int x = 0; x < 100; x++)
                    {
                        nextState[x, y] = IsAlive(currentState, x, y);
                    }
                }

                (currentState, nextState) = (nextState, currentState);
            }

            int lightCount = 0;

            for (int y = 0; y < 100; y++)
            {
                for (int x = 0; x < 100; x++)
                {
                    if (currentState[x, y])
                    {
                        lightCount++;
                    }
                }
            }

            Console.WriteLine($"The number of lights on is: {lightCount}");

            Console.WriteLine();
            Console.WriteLine("Star 2");
            Console.WriteLine();

            //Corner lights stuck on



            for (int y = 0; y < 100; y++)
            {
                for (int x = 0; x < 100; x++)
                {
                    currentState[x, y] = lines[x][y] != '.';
                }
            }

            currentState[0, 0] = true;
            currentState[0, 99] = true;
            currentState[99, 0] = true;
            currentState[99, 99] = true;

            for (int gen = 0; gen < 100; gen++)
            {
                for (int y = 0; y < 100; y++)
                {
                    for (int x = 0; x < 100; x++)
                    {
                        nextState[x, y] = IsAlive(currentState, x, y);
                    }
                }

                (currentState, nextState) = (nextState, currentState);

                currentState[0, 0] = true;
                currentState[0, 99] = true;
                currentState[99, 0] = true;
                currentState[99, 99] = true;
            }

            int lightCount2 = 0;

            for (int y = 0; y < 100; y++)
            {
                for (int x = 0; x < 100; x++)
                {
                    if (currentState[x, y])
                    {
                        lightCount2++;
                    }
                }
            }


            Console.WriteLine($"The number of lights on is: {lightCount2}");


            Console.WriteLine();
            Console.ReadKey();
        }

        private static bool IsAlive(bool[,] currentState, int x, int y)
        {
            int neighborCount = CountNeighbors(currentState, x, y);

            return neighborCount == 3 || (currentState[x, y] && neighborCount == 2);
        }

        private static int CountNeighbors(bool[,] currentState, int x, int y)
        {
            int x0 = Math.Max(0, x - 1);
            int y0 = Math.Max(0, y - 1);

            int x1 = Math.Min(100, x + 2);
            int y1 = Math.Min(100, y + 2);

            int neighborCount = 0;

            for (int i = x0; i < x1; i++)
            {
                for (int j = y0; j < y1; j++)
                {
                    if (i == x && j == y)
                    {
                        continue;
                    }

                    if (currentState[i, j])
                    {
                        neighborCount++;
                    }
                }
            }


            return neighborCount;
        }
    }
}

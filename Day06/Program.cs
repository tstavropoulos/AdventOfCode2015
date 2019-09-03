using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day06
{
    class Program
    {
        private const string inputFile = @"../../../../input06.txt";

        static void Main(string[] args)
        {
            Console.WriteLine("Day 6");
            Console.WriteLine("Star 1");
            Console.WriteLine();

            bool[,] grid = new bool[1000, 1000];
            int[,] grid2 = new int[1000, 1000];
            char[] splitChars = new char[] { ',', ' ' };

            string[] lines = File.ReadAllLines(inputFile);

            foreach (string line in lines)
            {
                string[] splitLine = line.Split(splitChars);

                if (splitLine[0] == "toggle")
                {
                    //Toggle
                    int x0 = int.Parse(splitLine[1]);
                    int y0 = int.Parse(splitLine[2]);

                    int x1 = int.Parse(splitLine[4]);
                    int y1 = int.Parse(splitLine[5]);

                    for (int y = y0; y <= y1; y++)
                    {
                        for (int x = x0; x <= x1; x++)
                        {
                            grid[x, y] = !grid[x, y];
                            grid2[x, y] += 2;
                        }
                    }

                }
                else
                {
                    //Turn On/Off
                    bool on = (splitLine[1] == "on");
                    int on2 = on ? 1 : -1;
                    int x0 = int.Parse(splitLine[2]);
                    int y0 = int.Parse(splitLine[3]);

                    int x1 = int.Parse(splitLine[5]);
                    int y1 = int.Parse(splitLine[6]);

                    for (int y = y0; y <= y1; y++)
                    {
                        for (int x = x0; x <= x1; x++)
                        {
                            grid[x, y] = on;
                            grid2[x, y] = Math.Max(0, grid2[x, y] + on2);
                        }
                    }

                }
            }

            int output1 = 0;
            long output2 = 0;
            for (int y = 0; y < 1000; y++)
            {
                for (int x = 0; x < 1000; x++)
                {
                    if (grid[x, y])
                    {
                        output1++;
                    }

                    output2 += grid2[x, y];
                }
            }




            Console.WriteLine($"The answer is: {output1}");

            Console.WriteLine();
            Console.WriteLine("Star 2");
            Console.WriteLine();

            Console.WriteLine($"The answer is: {output2}");


            Console.WriteLine();
            Console.ReadKey();
        }
    }
}

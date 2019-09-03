using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AoCTools;

namespace Day03
{
    class Program
    {
        private const string inputFile = @"../../../../input03.txt";

        static void Main(string[] args)
        {
            Console.WriteLine("Day 3");
            Console.WriteLine("Star 1");
            Console.WriteLine();

            {
                Dictionary<Point, int> visitDict = new Dictionary<Point, int>();

                int x = 0;
                int y = 0;

                string file = File.ReadAllText(inputFile);
                visitDict[(x, y)] = 1;

                foreach (char c in file)
                {
                    switch (c)
                    {
                        case '^':
                            y++;
                            break;

                        case 'v':
                            y--;
                            break;

                        case '<':
                            x--;
                            break;

                        case '>':
                            x++;
                            break;

                        default:
                            throw new Exception();
                    }

                    Point newPoint = new Point(x, y);
                    if (visitDict.ContainsKey(newPoint))
                    {
                        visitDict[newPoint]++;
                    }
                    else
                    {
                        visitDict[newPoint] = 1;
                    }
                }

                Console.WriteLine($"The number of houses that receive at least one present is: {visitDict.Count}");
            }

            Console.WriteLine();
            Console.WriteLine("Star 2");
            Console.WriteLine();

            {
                Dictionary<Point, int> visitDict = new Dictionary<Point, int>();

                (int x, int y) santaPos = (0, 0);
                (int x, int y) robotPos = (0, 0);

                string file = File.ReadAllText(inputFile);
                visitDict[santaPos] = 2;

                bool santaMove = true;

                foreach (char c in file)
                {
                    switch (c)
                    {
                        case '^':
                            if (santaMove)
                            {
                                santaPos.y++;
                            }
                            else
                            {
                                robotPos.y++;
                            }
                            break;

                        case 'v':
                            if (santaMove)
                            {
                                santaPos.y--;
                            }
                            else
                            {
                                robotPos.y--;
                            }
                            break;

                        case '<':
                            if (santaMove)
                            {
                                santaPos.x--;
                            }
                            else
                            {
                                robotPos.x--;
                            }
                            break;

                        case '>':
                            if (santaMove)
                            {
                                santaPos.x++;
                            }
                            else
                            {
                                robotPos.x++;
                            }
                            break;

                        default:
                            throw new Exception();
                    }

                    Point newPoint = santaMove ? new Point(santaPos.x, santaPos.y) : new Point(robotPos.x, robotPos.y);
                    if (visitDict.ContainsKey(newPoint))
                    {
                        visitDict[newPoint]++;
                    }
                    else
                    {
                        visitDict[newPoint] = 1;
                    }

                    santaMove = !santaMove;
                }

                Console.WriteLine($"The number of houses that receive at least one present is: {visitDict.Count}");
            }


            Console.WriteLine();
            Console.ReadKey();
        }
    }
}

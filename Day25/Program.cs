using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day25
{
    class Program
    {
        private const string inputFile = @"../../../../input25.txt";

        static void Main(string[] args)
        {
            Console.WriteLine("Day 25");
            Console.WriteLine();

            string[] lines = File.ReadAllLines(inputFile);

            //Enter the code at row 2978, column 3083.

            //   |    1         2         3         4         5         6
            //---+---------+---------+---------+---------+---------+---------+
            // 1 | 20151125  18749137  17289845  30943339  10071777  33511524
            // 2 | 31916031  21629792  16929656   7726640  15514188   4041754
            // 3 | 16080970   8057251   1601130   7981243  11661866  16474243
            // 4 | 24592653  32451966  21345942   9380097  10600672  31527494
            // 5 |    77061  17552253  28094349   6899651   9250759  31663883
            // 6 | 33071741   6796745  25397450  24659492   1534922  27995004

            //First code is 20151125
            // x[n] = ( x[n-1] * 252533 ) % 33554393

            int output = 0;

            // grid traversal:
            //
            // The number is on the Xth diagonal, Yth position, where 
            //   X = row + column - 1
            //   Y = column
            //

            //How do I go from the coordinates to the number of times to perform this operation?

            // Row 1:  0
            // Row 2:  1
            // Row 3:  3
            // Row 4:  6
            // Row N:  Row N-1 + N - 1 = Sum of 1 to N-1


            //Total operations are :  Sum of 1 to (2978 + 3083 - 2) + 3083 - 1


            //long diagonalNum = 2978 + 3083 - 2;
            //long diagonalNum = 6059;
            //Sum of 0 to 6059 is 6060 terms, or 3030*6059
            long diagonalNum = 3030 * 6059;
            long positionNum = 3083 - 1;

            long operationNum = diagonalNum + positionNum;

            long value = 20151125;

            for (long i = 0; i < operationNum; i++)
            {
                value = (value * 252533) % 33554393;
            }

            Console.WriteLine($"The answer is: {value}");
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}

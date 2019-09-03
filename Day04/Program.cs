using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Day04
{
    class Program
    {
        private const string inputFile = @"../../../../input04.txt";

        private static MD5 md5;

        static void Main(string[] args)
        {
            Console.WriteLine("Day 4");
            Console.WriteLine("Star 1");
            Console.WriteLine();

            string input = File.ReadAllText(inputFile);

            md5 = MD5.Create();


            long modifier = 1;
            while(true)
            {
                string hash = GetMd5Hash(md5, $"{input}{modifier.ToString()}");
                if (hash.StartsWith("00000"))
                {
                    break;
                }
                modifier++;
            }

            Console.WriteLine($"The answer is: {modifier}");

            Console.WriteLine();
            Console.WriteLine("Star 2");
            Console.WriteLine();

            while (true)
            {
                string hash = GetMd5Hash(md5, $"{input}{modifier.ToString()}");
                if (hash.StartsWith("000000"))
                {
                    break;
                }
                modifier++;
            }

            Console.WriteLine($"The answer is: {modifier}");


            Console.WriteLine();
            Console.ReadKey();
        }

        /// <summary>
        /// Stolen from MS Docs on MD5
        /// </summary>
        private static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}

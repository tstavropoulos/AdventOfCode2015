using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day11
{
    class Program
    {
        private const string inputFile = @"../../../../input11.txt";

        static void Main(string[] args)
        {
            Console.WriteLine("Day 11");
            Console.WriteLine("Star 1");
            Console.WriteLine();

            List<char> password = new List<char>(File.ReadAllText(inputFile));

            do
            {
                Increment(password);
            }
            while (!IsValid(password));


            Console.WriteLine($"The new password is: {string.Join("", password)}");

            Console.WriteLine();
            Console.WriteLine("Star 2");
            Console.WriteLine();

            do
            {
                Increment(password);
            }
            while (!IsValid(password));

            Console.WriteLine($"The new password is: {string.Join("", password)}");

            Console.WriteLine();
            Console.ReadKey();
        }

        private static void Increment(List<char> password)
        {
            int index = password.Count - 1;

            while (index >= 0 && password[index] == 'z')
            {
                password[index] = 'a';
                index--;
            }

            if (index == -1)
            {
                password.Insert(0, 'a');
            }
            else
            {
                password[index]++;
            }
        }

        private static bool IsValid(List<char> password)
        {
            //has i, o, or l
            for (int i = 0; i < password.Count; i++)
            {
                if (password[i] == 'i' || password[i] == 'o' || password[i] == 'l')
                {
                    return false;
                }
            }

            bool foundRun = false;

            for (int i = 0; i < password.Count - 2; i++)
            {
                if (password[i + 1] == password[i] + 1 && password[i + 2] == password[i] + 2)
                {
                    foundRun = true;
                    break;
                }
            }

            if (!foundRun)
            {
                return false;
            }

            for (int i = 0; i < password.Count - 3; i++)
            {
                if (password[i] == password[i + 1])
                {
                    for (int j = i + 2; j < password.Count - 1; j++)
                    {
                        if (password[j] == password[j + 1] && password[i] != password[j])
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}

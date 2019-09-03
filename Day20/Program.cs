using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day20
{
    class Program
    {
        private const string inputFile = @"../../../../input20.txt";

        static void Main(string[] args)
        {
            Console.WriteLine("Day 20");
            Console.WriteLine("Star 1");
            Console.WriteLine();

            int initialInput = int.Parse(File.ReadAllText(inputFile));

            int inputA = initialInput / 10;

            //Each elf (numbered N) delivers (10*) N presents to every house divisible by N 

            //What is the lowest numbered house to receive >= 2,900,000 presents?

            //For each house: Factorize, accumulate (10*) every combination of factors

            //Generate max prime list
            int[] primeList = PrimesUpTo((int)Math.Sqrt(inputA)).ToArray();

            int target = 2;

            while (true)
            {
                int sum = SumUniqueFactors(target, primeList);

                if (sum > inputA)
                {
                    break;
                }

                target++;
            }


            Console.WriteLine($"The answer is: {target}");

            Console.WriteLine();
            Console.WriteLine("Star 2");
            Console.WriteLine();

            int inputB = initialInput / 11;

            target = 2;

            while (true)
            {
                int sum = SumUniqueFactors2(target, primeList);

                if (sum > inputB)
                {
                    break;
                }

                target++;
            }


            int output2 = 0;



            Console.WriteLine($"The answer is: {target}");


            Console.WriteLine();
            Console.ReadKey();
        }

        public static int SumUniqueFactors(int number, IEnumerable<int> primes)
        {
            IEnumerable<int> factors = Factorize(number, primes).ToArray();

            return GenerateFactors(factors).Distinct().Sum();
        }

        public static int SumUniqueFactors2(int number, IEnumerable<int> primes)
        {
            IEnumerable<int> factors = Factorize(number, primes).ToArray();

            return GenerateFactors(factors).Distinct().Where(x => x * 50 >= number).Sum();
        }

        public static IEnumerable<int> GenerateFactors(IEnumerable<int> factors)
        {
            yield return 1;

            for (int i = 0; i < factors.Count(); i++)
            {
                int element = factors.ElementAt(i);
                foreach (int factor in GenerateFactors(factors.Skip(i + 1)))
                {
                    yield return element * factor;
                }
            }
        }

        public static IEnumerable<int> Factorize(int number, IEnumerable<int> primes)
        {
            if (number < 1)
            {
                yield break;
            }

            foreach (int prime in primes)
            {
                while (number % prime == 0)
                {
                    yield return prime;
                    number /= prime;
                }

                if (number == 1)
                {
                    break;
                }
            }

            if (number > 1)
            {
                yield return number;
            }
        }

        public static IEnumerable<int> PrimesUpTo(int number)
        {
            if (number < 2)
            {
                yield break;
            }

            //Include the boundary
            number++;

            BitArray primeField = new BitArray(number, true);
            primeField.Set(0, false);
            primeField.Set(1, false);
            yield return 2;

            //We don't bother setting the multiples of 2 because we don't bother checking them.

            int i;
            for (i = 3; i * i < number; i += 2)
            {
                if (primeField.Get(i))
                {
                    //i Is Prime
                    yield return i;

                    //Clear new odd factors
                    //All our primes are now odd, as are our primes Squared.
                    //This maens the numbers we need to clear start at i*i, and advance by 2*i
                    //For example j=3:  9 is the first odd composite, 15 is the next odd composite 
                    //  that's a factor of 3
                    for (int j = i * i; j < number; j += 2 * i)
                    {
                        primeField.Set(j, false);
                    }
                }
            }

            //Grab remainder of identified primes
            for (; i < number; i += 2)
            {
                if (primeField.Get(i))
                {
                    yield return i;
                }
            }
        }
    }
}

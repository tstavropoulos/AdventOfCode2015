using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day21
{
    class Program
    {
        const int bossHealth = 100;
        const int bossDamage = 8;
        const int bossArmor = 2;

        static void Main(string[] args)
        {
            Console.WriteLine("Day 21");
            Console.WriteLine("Star 1");
            Console.WriteLine();

            //Your Health is 100
            //Your Damage is 0
            //Your Armor is 0

            //Weapons:     Cost  Damage  Armor
            //Dagger         8     4       0
            //Shortsword    10     5       0
            //Warhammer     25     6       0
            //Longsword     40     7       0
            //Greataxe      74     8       0
            //
            //Armor:       Cost  Damage  Armor
            //Leather       13     0       1
            //Chainmail     31     0       2
            //Splintmail    53     0       3
            //Bandedmail    75     0       4
            //Platemail    102     0       5
            //
            //Rings:       Cost  Damage  Armor
            //Damage + 1    25     1       0
            //Damage + 2    50     2       0
            //Damage + 3   100     3       0
            //Defense + 1   20     0       1
            //Defense + 2   40     0       2
            //Defense + 3   80     0       3

            //You must buy exactly one weapon; no dual-wielding. 
            //Armor is optional, but you can't use more than one.
            //You can buy 0-2 rings (at most one for each hand).
            //You must use any items you buy.
            //The shop only has one of each item, so you can't buy, for example, 
            //  two rings of Damage +3.

            //Player always goes first
            //Damage is equal to Damage stat minus armor stat, min 1

            //What is the least amount of gold you can spend and still win the fight?

            Item[] weapons = new Item[]
            {
                new Item (8, 4, 0),
                new Item (10, 5, 0),
                new Item (25, 6, 0),
                new Item (40, 7, 0),
                new Item (74, 8, 0)
            };

            Item[] armors = new Item[]
            {
                new Item (0, 0, 0),
                new Item (13, 0, 1),
                new Item (31, 0, 2),
                new Item (53, 0, 3),
                new Item (75, 0, 4),
                new Item (102, 0, 5)
            };

            Item[] rings = new Item[]
            {
                new Item (25, 1, 0),
                new Item (50, 2, 0),
                new Item (100, 3, 0),
                new Item (20, 0, 1),
                new Item (40, 0, 2),
                new Item (80, 0, 3)
            };

            //Step 1: Figure out the exact winning min combos:

            Dictionary<int, int> winningCombos = new Dictionary<int, int>();

            for (int atk = 4; atk <= 13; atk++)
            {
                for (int def = 0; def <= 10; def++)
                {
                    int damagePerRound = Math.Max(1, atk - bossArmor);
                    int healthPerRound = Math.Max(1, bossDamage - def);
                    int roundsToWin = (bossHealth + damagePerRound - 1) / damagePerRound;
                    int roundsSurvived = (100 + healthPerRound - 1) / healthPerRound;

                    if (roundsSurvived >= roundsToWin)
                    {
                        winningCombos[atk] = def;
                        break;
                    }
                }
            }

            int minCost = int.MaxValue;
            foreach (var kvp in winningCombos)
            {
                minCost = Math.Min(minCost, GetMinGoldCost(kvp.Key, kvp.Value, weapons, armors, rings));
            }



            Console.WriteLine($"The answer is: {minCost}");

            Console.WriteLine();
            Console.WriteLine("Star 2");
            Console.WriteLine();


            Dictionary<int, int> losingCombos = new Dictionary<int, int>();

            for (int atk = 4; atk <= 13; atk++)
            {
                for (int def = 0; def <= 10; def++)
                {
                    int damagePerRound = Math.Max(1, atk - bossArmor);
                    int healthPerRound = Math.Max(1, bossDamage - def);
                    int roundsToWin = (bossHealth + damagePerRound - 1) / damagePerRound;
                    int roundsSurvived = (100 + healthPerRound - 1) / healthPerRound;

                    if (roundsSurvived < roundsToWin)
                    {
                        losingCombos[atk] = def;
                        break;
                    }
                }
            }

            int maxCost = 0;
            foreach (var kvp in losingCombos)
            {
                maxCost = Math.Max(maxCost, GetMaxGoldCost(kvp.Key, kvp.Value, weapons, armors, rings));
            }

            Console.WriteLine($"The answer is: {maxCost}");


            Console.WriteLine();
            Console.ReadKey();
        }

        private static int GetMinGoldCost(int atk, int def, Item[] weapons, Item[] armors, Item[] rings)
        {
            int minGoldCost = int.MaxValue;

            foreach (Item weapon in weapons)
            {
                if (weapon.damage > atk)
                {
                    break;
                }

                foreach (Item armor in armors)
                {
                    if (armor.armor > def)
                    {
                        break;
                    }

                    int cost = weapon.cost + armor.cost + GetMinGoldCost(
                        atk: atk - weapon.damage,
                        def: def - armor.armor,
                        ringCount: 0,
                        ringIndex: 0,
                        rings: rings);

                    minGoldCost = Math.Min(minGoldCost, cost);
                }
            }

            return minGoldCost;
        }

        private static int GetMinGoldCost(int atk, int def, int ringCount, int ringIndex, Item[] rings)
        {
            if (atk == 0 && def == 0)
            {
                return 0;
            }

            if (ringCount == 2)
            {
                return int.MaxValue / 16;
            }

            int minCost = int.MaxValue / 16;

            for (int i = ringIndex; i < rings.Length; i++)
            {
                if (atk - rings[i].damage >= 0 && def - rings[i].armor >= 0)
                {
                    int cost = rings[i].cost + GetMinGoldCost(
                        atk: atk - rings[i].damage,
                        def: def - rings[i].armor,
                        ringCount: ringCount + 1,
                        ringIndex: i + 1,
                        rings: rings);
                    minCost = Math.Min(minCost, cost);
                }
            }

            return minCost;
        }

        private static int GetMaxGoldCost(int atk, int def, Item[] weapons, Item[] armors, Item[] rings)
        {
            int maxGoldCost = 0;

            foreach (Item weapon in weapons)
            {
                if (weapon.damage > atk)
                {
                    break;
                }

                foreach (Item armor in armors)
                {
                    if (armor.armor > def)
                    {
                        break;
                    }

                    int cost = weapon.cost + armor.cost + GetMaxGoldCost(
                        atk: atk - weapon.damage,
                        def: def - armor.armor,
                        ringCount: 0,
                        ringIndex: 0,
                        rings: rings);

                    maxGoldCost = Math.Max(maxGoldCost, cost);
                }
            }

            return maxGoldCost;
        }

        private static int GetMaxGoldCost(int atk, int def, int ringCount, int ringIndex, Item[] rings)
        {
            if (atk == 0 && def == 0)
            {
                return 0;
            }

            if (ringCount == 2)
            {
                return -int.MaxValue / 16;
            }

            int maxCost = -int.MaxValue / 16;

            for (int i = ringIndex; i < rings.Length; i++)
            {
                if (atk - rings[i].damage >= 0 && def - rings[i].armor >= 0)
                {
                    int cost = rings[i].cost + GetMaxGoldCost(
                        atk: atk - rings[i].damage,
                        def: def - rings[i].armor,
                        ringCount: ringCount + 1,
                        ringIndex: i + 1,
                        rings: rings);
                    maxCost = Math.Max(maxCost, cost);
                }
            }

            return maxCost;
        }


        readonly struct Item
        {
            public readonly int cost;
            public readonly int damage;
            public readonly int armor;

            public Item(int cost, int damage, int armor)
            {
                this.cost = cost;
                this.damage = damage;
                this.armor = armor;
            }
        }
    }
}

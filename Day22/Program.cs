using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day22
{
    class Program
    {
        const int bossHealth = 55;
        const int BOSS_DAMAGE = 8;

        static int MinManaSpent = int.MaxValue;
        static int BossDamage;

        static void Main(string[] args)
        {
            Console.WriteLine("Day 22");
            Console.WriteLine("Star 1");
            Console.WriteLine();

            //50 HP
            //500 Mana

            //Spells:
            // Magic Missile costs 53 mana. It instantly does 4 damage.
            // Drain costs 73 mana. It instantly does 2 damage and heals you for 2 hit points.
            // Shield costs 113 mana. It starts an effect that lasts for 6 turns.  
            //   While it is active, your armor is increased by 7.
            // Poison costs 173 mana. It starts an effect that lasts for 6 turns.
            //   At the start of each turn while it is active, it deals the boss 3 damage.
            // Recharge costs 229 mana. It starts an effect that lasts for 5 turns.
            //   At the start of each turn while it is active, it gives you 101 new mana.

            //You cannot cast a spell that would start an effect which is already active.

            BossDamage = BOSS_DAMAGE;
            KillBoss(true, 0, 500, 50, bossHealth, 0, 0, 0);



            Console.WriteLine($"The answer is: {MinManaSpent}");

            Console.WriteLine();
            Console.WriteLine("Star 2");
            Console.WriteLine();

            MinManaSpent = int.MaxValue;

            BossDamage = BOSS_DAMAGE + 1;
            KillBoss(true, 0, 500, 49, bossHealth, 0, 0, 0);


            Console.WriteLine($"The answer is: {MinManaSpent}");


            Console.WriteLine();
            Console.ReadKey();
        }

        private static void KillBoss(
            bool playerTurn,
            int manaSpent,
            int playerMana,
            int playerHP,
            int bossHP,
            int remainingShield,
            int remainingPoison,
            int remainingRecharge)
        {
            bool playerArmor = false;
            if (remainingShield > 0)
            {
                remainingShield--;
                playerArmor = true;
            }

            if (remainingPoison > 0)
            {
                remainingPoison--;
                bossHP -= 3;
            }

            if (remainingRecharge > 0)
            {
                remainingRecharge--;
                playerMana += 101;
            }

            if (bossHP <= 0)
            {
                if (MinManaSpent > manaSpent)
                {
                    MinManaSpent = manaSpent;
                }
                return;
            }


            if (playerTurn)
            {
                if (playerMana >= 53 && manaSpent + 53 < MinManaSpent)
                {
                    KillBoss(
                        playerTurn: false,
                        manaSpent: manaSpent + 53,
                        playerMana: playerMana - 53,
                        playerHP: playerHP,
                        bossHP: bossHP - 4,
                        remainingShield: remainingShield,
                        remainingPoison: remainingPoison,
                        remainingRecharge: remainingRecharge);
                }

                if (playerMana >= 73 && manaSpent + 73 < MinManaSpent)
                {
                    KillBoss(
                        playerTurn: false,
                        manaSpent: manaSpent + 73,
                        playerMana: playerMana - 73,
                        playerHP: Math.Min(playerHP + 2, 50),
                        bossHP: bossHP - 2,
                        remainingShield: remainingShield,
                        remainingPoison: remainingPoison,
                        remainingRecharge: remainingRecharge);
                }

                if (playerMana >= 113 && remainingShield == 0 && manaSpent + 113 < MinManaSpent)
                {
                    //Cast Shield
                    KillBoss(
                        playerTurn: false,
                        manaSpent: manaSpent + 113,
                        playerMana: playerMana - 113,
                        playerHP: playerHP,
                        bossHP: bossHP,
                        remainingShield: 6,
                        remainingPoison: remainingPoison,
                        remainingRecharge: remainingRecharge);
                }

                if (playerMana >= 173 && remainingPoison == 0 && manaSpent + 173 < MinManaSpent)
                {
                    //Cast Poison
                    KillBoss(
                        playerTurn: false,
                        manaSpent: manaSpent + 173,
                        playerMana: playerMana - 173,
                        playerHP: playerHP,
                        bossHP: bossHP,
                        remainingShield: remainingShield,
                        remainingPoison: 6,
                        remainingRecharge: remainingRecharge);
                }

                if (playerMana >= 229 && remainingRecharge == 0 && manaSpent + 229 < MinManaSpent)
                {
                    //Cast Recharge
                    KillBoss(
                        playerTurn: false,
                        manaSpent: manaSpent + 229,
                        playerMana: playerMana - 229,
                        playerHP: playerHP,
                        bossHP: bossHP,
                        remainingShield: remainingShield,
                        remainingPoison: remainingPoison,
                        remainingRecharge: 5);
                }
            }
            else
            {
                int damage = playerArmor ? BossDamage - 7 : BossDamage;

                if (damage >= playerHP)
                {
                    return;
                }

                KillBoss(
                    playerTurn: true,
                    manaSpent: manaSpent,
                    playerMana: playerMana,
                    playerHP: playerHP - damage,
                    bossHP: bossHP,
                    remainingShield: remainingShield,
                    remainingPoison: remainingPoison,
                    remainingRecharge: remainingRecharge);
            }
        }
    }
}

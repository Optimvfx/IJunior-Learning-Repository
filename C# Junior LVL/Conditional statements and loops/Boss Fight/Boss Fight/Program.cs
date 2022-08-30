using System;


namespace Boss_Fight
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region player
            float currentPlayerHealth = 300;
            bool isPLayerDead = false; 
            
            float playerEnergy = 100;
            float playerEnergyRegenerationPerStep = 10;

            #region playerSpells
            string spellNameBaharot = "BAHAROT";
            string spellNameSuckHealth = "SUCK";
            string spellNameBustEnergy = "BUST";
            string spellNameCastBall = "CASTBALL";

            float spellBaharotEnergyUsePerCast = 10;
            float spellSuckHealthEnergyUsePerCast = 20;
            float spellBustEnergyUsePerCast = 10;
            float spellCastBallEnergyUsePerCast = 50;

            float spellBaharotBossDamageNegativeEffect = 10;
            float spellBaharotBossPasiveHealthNegativeEffect = 10;

            float spellCastBallBossHealthNegativeEffect = 125;

            float spellBustEnergyPlayerBustEnergyPositiveEffect = 10;

            float spellSuckHealthBossHealthNegativeEffect = 50;
            float spellSuckHealthPlayerHealthPositiveEffect = spellSuckHealthBossHealthNegativeEffect / 1.5f;

            float invalidCastMaximumDamage = 30;
            #endregion playerSpells
            #endregion player

            #region Boss
            float bossHealth = 1000;
            bool isBossDead = false;

            float bossDamage = 50;
            float bossMinumalDamage = 5;

            float bossHealthPasiveEffect = 5;
            #endregion Boss
 
            var random = new Random();

            while(isBossDead == false && isPLayerDead == false)
            {
                #region gameInfoDebug
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\nPlayer: HP {currentPlayerHealth}, ENERGY {playerEnergy}, ENERGYREGENERATION {playerEnergyRegenerationPerStep}." +
                    $"\nBoss: HP {bossHealth},Damage {bossDamage}, PASIVE HP EFFECT {bossHealthPasiveEffect}.");
                #endregion gameInfoDebug

                #region playerCast

                #region playerInput
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nPosible Spells: " +
                    $"\n{spellNameBaharot} - debuf boss." +
                    $"\n{spellNameBustEnergy} - bust player energy regeneration." +
                    $"\n{spellNameCastBall} -  cast a magic ball thet damage boss." +
                    $"\n{spellNameSuckHealth} - suck health from boss to player.");

                Console.ForegroundColor = ConsoleColor.Cyan;
                var sellectedSpell = Console.ReadLine().ToUpper();
                #endregion playerInput
                //Не пользуюсь Switch так как делаю с заскриптоваными именами
                if (sellectedSpell == spellNameBaharot && playerEnergy >= spellBaharotEnergyUsePerCast)
                {
                    playerEnergy -= spellBaharotEnergyUsePerCast;

                    bossDamage -= spellBaharotBossDamageNegativeEffect;
                    bossHealthPasiveEffect -= spellBaharotBossPasiveHealthNegativeEffect;
                }
                else if (sellectedSpell == spellNameCastBall && playerEnergy >= spellCastBallEnergyUsePerCast)
                {
                    playerEnergy -= spellCastBallEnergyUsePerCast;

                    bossHealth -= spellCastBallBossHealthNegativeEffect;
                }
                else if (sellectedSpell == spellNameBustEnergy && playerEnergy >= spellBustEnergyUsePerCast)
                {
                    playerEnergy -= spellBustEnergyUsePerCast;

                    playerEnergyRegenerationPerStep += spellBustEnergyPlayerBustEnergyPositiveEffect;
                }
                else if (sellectedSpell == spellNameSuckHealth && playerEnergy >= spellSuckHealthEnergyUsePerCast)
                {
                    playerEnergy -= spellSuckHealthEnergyUsePerCast;

                    bossHealth -= spellSuckHealthBossHealthNegativeEffect;
                    currentPlayerHealth += spellSuckHealthPlayerHealthPositiveEffect;
                }
                else
                {
                    var invalidCastDamage = (float)random.NextDouble() * invalidCastMaximumDamage;
                    currentPlayerHealth -= invalidCastDamage;
                    Console.WriteLine($"You casted Invalid Speel and damage self by {invalidCastDamage}.");
                }

                playerEnergy += playerEnergyRegenerationPerStep;
                #endregion playerCast

                #region boss
                
                #region bossDamage
                bossDamage = Math.Max(bossDamage, bossMinumalDamage);

                currentPlayerHealth -= bossDamage;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nBoss damaged player by {bossDamage}.");
                #endregion bossDamage

                bossHealth += bossHealthPasiveEffect;
                #endregion boss

                #region checkGameState
                isBossDead = bossHealth <= 0;
                isPLayerDead =  currentPlayerHealth <= 0;
                #endregion checkGameState
            }

            #region figthEndInfoDebug
            Console.ForegroundColor = ConsoleColor.Yellow;

            if(isBossDead && isPLayerDead)
            {
                Console.WriteLine("\n All dead!");
            }
            if(isBossDead)
            {
                Console.WriteLine("\n Boss dead!");
            }
            else
            {
                Console.WriteLine("\n Player dead!");
            }

            Console.ReadKey();
            #endregion fightEndInfoDebug 
        }
    }
}

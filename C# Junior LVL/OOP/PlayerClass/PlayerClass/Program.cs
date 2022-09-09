﻿using System;

namespace PlayerClass
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var player = new Player("Joo", 100, 50);

            player.ShowInfo();

            Console.ReadKey();
        }

        private class Player
        {
            public string Name { get; private set; }

            public int MaximalHealth { get; private set; }
            public int CurrentHealth { get; private set; }

            public int MaximalEnergy { get; private set; }
            public int CurrentEnergy { get; private set; }

            public Player(string name, int maximalHealth, int maximalEnergy)
            {
                Name = name;

                MaximalHealth = maximalHealth;
                CurrentHealth = maximalHealth;

                MaximalEnergy = maximalEnergy;
                CurrentEnergy = maximalEnergy;
            }

            public void ShowInfo()
            {
                Console.WriteLine($"Player:" +
                    $"\nName: {Name}" +
                    $"\nHPMAX: {MaximalHealth}, HP: {CurrentHealth}" +
                    $"\nENGMAX: {MaximalEnergy}, ENG: {CurrentEnergy}");
            }
        }
    }
}

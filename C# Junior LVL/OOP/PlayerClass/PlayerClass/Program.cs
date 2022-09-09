using System;

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
            private string Name;

            private int MaximalHealth;
            private int CurrentHealth;

            private int MaximalEnergy;
            private int CurrentEnergy;

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

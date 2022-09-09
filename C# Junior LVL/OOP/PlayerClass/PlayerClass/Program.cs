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
    }

    public class Player
    {
        private string _name;

        private int _maximalHealth;
        private int _currentHealth;

        private int _maximalEnergy;
        private int _currentEnergy;

        public Player(string name, int maximalHealth, int maximalEnergy)
        {
            _name = name;

            _maximalHealth = maximalHealth;
            _currentHealth = maximalHealth;

            _maximalEnergy = maximalEnergy;
            _currentEnergy = maximalEnergy;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Player:" +
                $"\nName: {_name}" +
                $"\nHPMAX: {_maximalHealth}, HP: {_currentHealth}" +
                $"\nENGMAX: {_maximalEnergy}, ENG: {_currentEnergy}");
        }
    }
}

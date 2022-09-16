using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerTop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int countPlayersToShow = 3;

            var players = new List<Player>();

            players.Add(new Player("Joo",20,15));
            players.Add(new Player("DeadInside", 61, 17));
            players.Add(new Player("Akaraw", 31, 13));
            players.Add(new Player("Duga", 75, 8));
            players.Add(new Player("Blast", 5, 6));
            players.Add(new Player("Kivi", 20, 3));
            players.Add(new Player("Kesh", 52, 5));
            players.Add(new Player("Antosha200", 15, 215));
            players.Add(new Player("Popadanez", 52, 165));
            players.Add(new Player("Rapira", 120, 205));

            var playerDatabase = new PlayerDatabase(players);

            Console.WriteLine("Top by level:");
            playerDatabase.ShowPlayerTopByLevel(countPlayersToShow);

            Console.WriteLine("Top by power:");
            playerDatabase.ShowPlayerTopByPower(countPlayersToShow);

            Console.ReadKey();
        }
    }

    public class PlayerDatabase
    {
        private readonly IEnumerable<Player> _players;

        public PlayerDatabase(IEnumerable<Player> players)
        {
            _players = players;
        }

        public void ShowPlayerTopByLevel(int count)
        {
            int boundedCount = Math.Max(count, 0);

            var playerTopByLevel = _players.OrderByDescending(player => player.Level)
                                           .Take(boundedCount);

            ShowPlayers(playerTopByLevel);
        }

        public void ShowPlayerTopByPower(int count)
        {
            int boundedCount = Math.Max(count, 0);

            var playerTopByLevel = _players.OrderByDescending(player => player.Power)
                                           .Take(boundedCount);

            ShowPlayers(playerTopByLevel);
        }

        private void ShowPlayers(IEnumerable<Player> players)
        {
            int playerIndex = 1;

            foreach(var player in players)
            {
                Console.WriteLine($"{playerIndex} : {player.GetInfo()}");
                playerIndex++;
            }
        }
    }

    public struct Player
    {
        public readonly string Name;

        public readonly int Level;
        public readonly int Power;

        public Player(string name, int level, int power)
        {
            Name = name;

            Level = Math.Max(level, 0);
            Power = Math.Max(power, 0);
        }

        public string GetInfo()
        {
            return $"{Name}, Level {Level}, Power {Power}.";
        }
    }
}

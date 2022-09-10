using System;
using System.Collections.Generic;

namespace PlayerDataBase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string ShowAllCommand = "SHOW";
            const string AddCommand = "ADD";
            const string RemoveCommand = "REMOVE";
            const string BanCommand = "BAN";
            const string UnBanCommand = "UNBAN";
            const string ExitCommand = "EXIT";

            var playersDataBase = new PlayersDataBase();

            bool isOpen = true;

            while(isOpen)
            {
                Console.WriteLine($"\nSellect command:" +
                    $"\n{ShowAllCommand}" +
                    $"\n{AddCommand}" +
                    $"\n{RemoveCommand}" +
                    $"\n{BanCommand}" +
                    $"\n{UnBanCommand}" +
                    $"\n{ExitCommand}");

                var userInput = Console.ReadLine().ToUpper();

                switch(userInput)
                {
                    case ShowAllCommand:
                        playersDataBase.ShowAllPlayers();
                        break;
                    case AddCommand:
                        playersDataBase.AddPlayer();
                        break;
                    case RemoveCommand:
                        playersDataBase.RemovePlayer();
                        break;
                    case BanCommand:
                        playersDataBase.BanPlayer();
                        break;
                    case UnBanCommand:
                        playersDataBase.UnbanPlayer();
                        break;
                    case ExitCommand:
                        isOpen = false;
                        break;
                    default:
                        ConsoleExtention.WriteLine("Uncnown command!", ConsoleColor.Red);
                        break;
                }
            }
        }
    }

    public class PlayersDataBase
    {
        private List<Player> _players;

        public PlayersDataBase()
        {
            _players = new List<Player>();
        }
    
        public void ShowAllPlayers()
        {
            const ConsoleColor BanedColor = ConsoleColor.Red;
            const ConsoleColor UnbanedColor = ConsoleColor.Green;

            foreach(var player in _players)
            {
                if (player.IsBaned)
                {
                    ConsoleExtention.WriteLine($"User {player.Name} lvl {player.Level}.\nUniqueKey {{{player.UniqueKey}}}", BanedColor);
                }
                else
                {
                    ConsoleExtention.WriteLine($"User {player.Name} lvl {player.Level}.\nUniqueKey {{{player.UniqueKey}}}", UnbanedColor);
                }
            }
        }

        public void AddPlayer()
        {
            Console.Write("New player name: ");
            var newPlayerName = Console.ReadLine();

            Console.Write("New player level: ");
            var newPlayerLevel = Convert.ToInt32(Console.ReadLine());

            var newPlayer = new Player(newPlayerName, newPlayerLevel);
            _players.Add(newPlayer);
        }

        public void RemovePlayer()
        {
            Console.Write("Enter removing player unique key: ");
            var removingPlayerUniqueKey = Convert.ToInt32(Console.ReadLine());

            if(TryGetPlayerIndexByUniqueKey(removingPlayerUniqueKey, out int removingPlayerIndex))
            {
                _players.RemoveAt(removingPlayerIndex);
            }
            else
            {
                ConsoleExtention.WriteLine($"Cant remove player witch unique key {removingPlayerUniqueKey}!", ConsoleColor.Red);
            }
        }

        public void BanPlayer()
        {
            Console.Write("Enter baning player unique key: ");
            var baningPlayerUniqueKey = Convert.ToInt32(Console.ReadLine());

            if (TryGetPlayerIndexByUniqueKey(baningPlayerUniqueKey, out int baningPlayerIndex) ==  false || _players[baningPlayerIndex].TryBan() == false)
            {
                ConsoleExtention.WriteLine($"Cant ban player witch unique key {baningPlayerUniqueKey}!", ConsoleColor.Red);
            }
        }

        public void UnbanPlayer()
        {
            Console.Write("Enter unbaning player unique key: ");
            var unBaningPlayerUniqueKey = Convert.ToInt32(Console.ReadLine());

            if (TryGetPlayerIndexByUniqueKey(unBaningPlayerUniqueKey, out int unBaningPlayerIndex) == false || _players[unBaningPlayerIndex].TryUnBan() == false)
            { 
                ConsoleExtention.WriteLine($"Cant unban player witch unique key {unBaningPlayerUniqueKey}!", ConsoleColor.Red);
            }
        }

        private bool TryGetPlayerIndexByUniqueKey(int serchingUniqueKey, out int index)
        {
            index = 0;

            for (int i = 0; i < _players.Count; i++)
            {
                if (_players[i].UniqueKey == serchingUniqueKey)
                {
                    index = i;
                    return true;
                }
            }

            return false;
        }
    }

    public class Player
    {
        public string Name { get; private set; }
        public int Level { get; private set; }

        public bool IsBaned { get; private set; }

        public int UniqueKey { get; private set; }

        public Player(string name, int level)
        {
            Name = name;
            Level = level;

            IsBaned = false;

            var random = new Random();

            UniqueKey = random.Next(int.MinValue, int.MaxValue);
        }

        //Делаю методы для большей понятности.
        public bool TryBan()
        {
            if (IsBaned)
                return false;

            IsBaned = true;
            return true;
        }
        
        public bool TryUnBan()
        {
            if (IsBaned == false)
                return false;

            IsBaned = false;
            return true;
        }
    }

    public static class ConsoleExtention
    {
        public static void WriteLine(string text, ConsoleColor color)
        {
            var prewiusColor = Console.ForegroundColor;

            Console.ForegroundColor = color;
            Console.WriteLine(text);

            Console.ForegroundColor = prewiusColor;
        }
    }
}

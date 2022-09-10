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

            var database = new Database();

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
                        database.ShowAllPlayers();
                        break;
                    case AddCommand:
                        database.AddPlayer();
                        break;
                    case RemoveCommand:
                        database.RemovePlayer();
                        break;
                    case BanCommand:
                        database.BanPlayer();
                        break;
                    case UnBanCommand:
                        database.UnbanPlayer();
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

    public class Database
    {
        private List<Player> _players;

        public Database()
        {
            _players = new List<Player>();
        }
    
        public void ShowAllPlayers()
        {
            foreach (var player in _players)
            {
                var playerInfoColor = ConsoleColor.Green;

                if (player.IsBaned)
                {
                    playerInfoColor = ConsoleColor.Red;
                }

                ConsoleExtention.WriteLine($"User {player.Name} lvl {player.Level}.\nUniqueKey {{{player.UniqueKey}}}", playerInfoColor);
            }
        }

        public void AddPlayer()
        {
            Console.Write("New player name: ");
            var newPlayerName = Console.ReadLine();

            Console.Write("New player level: ");

            if (int.TryParse(Console.ReadLine(), out int newPlayerLevel))
            {
                var newPlayer = new Player(newPlayerName, newPlayerLevel);
                _players.Add(newPlayer);
            }
            else
            {
                ConsoleExtention.WriteLine("Cant convert to int player level input!", ConsoleColor.Red);
            }    
        }

        public void RemovePlayer()
        {
            Console.Write("Enter removing player unique key: ");

            if (int.TryParse(Console.ReadLine(), out int removingPlayerUniqueKey))
            {
                if (TryGetPlayerIndexByUniqueKey(removingPlayerUniqueKey, out int removingPlayerIndex))
                {
                    _players.RemoveAt(removingPlayerIndex);
                }
                else
                {
                    ConsoleExtention.WriteLine($"Cant remove player witch unique key {removingPlayerUniqueKey}!", ConsoleColor.Red);
                }
            }
            else
            {
                ConsoleExtention.WriteLine("Cant convert to int  removing player unique key input!", ConsoleColor.Red);
            }
        }

        public void BanPlayer()
        {
            Console.Write("Enter baning player unique key: ");

            if (int.TryParse(Console.ReadLine(), out int baningPlayerUniqueKey))
            {
                if (TryGetPlayerIndexByUniqueKey(baningPlayerUniqueKey, out int baningPlayerIndex) == false || _players[baningPlayerIndex].TryBan() == false)
                {
                    ConsoleExtention.WriteLine($"Cant ban player witch unique key {baningPlayerUniqueKey}!", ConsoleColor.Red);
                }
            }
            else
            {
                ConsoleExtention.WriteLine("Cant convert to int baning player unique key input!", ConsoleColor.Red);
            }
        }

        public void UnbanPlayer()
        {
            Console.Write("Enter unbaning player unique key: ");

            if (int.TryParse(Console.ReadLine(), out int unBaningPlayerUniqueKey))
            {
                if (TryGetPlayerIndexByUniqueKey(unBaningPlayerUniqueKey, out int unBaningPlayerIndex) == false || _players[unBaningPlayerIndex].TryUnBan() == false)
                {
                    ConsoleExtention.WriteLine($"Cant unban player witch unique key {unBaningPlayerUniqueKey}!", ConsoleColor.Red);
                }
            }
            else
            {
                ConsoleExtention.WriteLine("Cant convert to int unbaning player unique key input!", ConsoleColor.Red);
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

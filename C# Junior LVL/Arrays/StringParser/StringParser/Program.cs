using System;

namespace StringParser
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var userInputString = Console.ReadLine();

            var splitChar = ' ';

            var userInputWorlds = userInputString.Split(new[] { splitChar },StringSplitOptions.RemoveEmptyEntries);

            foreach(var world in userInputWorlds)
                Console.WriteLine(world + " ");

            Console.ReadKey();
        }
    }
}

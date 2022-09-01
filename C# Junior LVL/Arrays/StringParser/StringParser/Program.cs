using System;
using System.Linq;

namespace StringParser
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var userInputString = Console.ReadLine();

            var splitChar = ' ';

            var userInputWorlds = userInputString.Split(splitChar).Where(world => world != "");

            foreach(var world in userInputWorlds)
                Console.WriteLine(world + " ");

            Console.ReadKey();
        }
    }
}

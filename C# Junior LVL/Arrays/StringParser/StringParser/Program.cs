using System;

namespace StringParser
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var userInputString = Console.ReadLine();

            var splitChar = ' ';

            var userInputWords = userInputString.Split(new[] { splitChar },StringSplitOptions.RemoveEmptyEntries);

            foreach(var word in userInputWords)
                Console.WriteLine(word + " ");

            Console.ReadKey();
        }
    }
}

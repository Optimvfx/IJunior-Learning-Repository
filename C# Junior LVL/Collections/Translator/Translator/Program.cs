using System;
using System.Collections.Generic;

namespace Translator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> wordToLinguistiFrequency = new Dictionary<string, int>();

            wordToLinguistiFrequency["see"] = 5;
            wordToLinguistiFrequency["convert"] = 20;
            wordToLinguistiFrequency["car"] = 4;
            wordToLinguistiFrequency["animal"] = 15;

            bool isOpen = true;

            while(isOpen)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Word to convert to linguisti frequency: ");

                var wordToConvert = Console.ReadLine();

                if(TryWordLinguisticFrequency(wordToLinguistiFrequency,wordToConvert, out int linguistiFcrequency))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Linguisti frequency of {wordToConvert} is {linguistiFcrequency}.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Cant find {wordToConvert} word in dictionary!");
                }
            }
        }

        private static bool TryWordLinguisticFrequency(Dictionary<string, int> wordToLinguistiFrequency,string word, out int linguisticFrequency)
        {
            if(wordToLinguistiFrequency.ContainsKey(word))
            {
                linguisticFrequency = wordToLinguistiFrequency[word];
                return true;
            }

            linguisticFrequency = 0;
            return false;   
        }
    }
}

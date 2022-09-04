using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleCalculatorList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string ExitCommand = "EXIT";
            const string SumCommand = "SUM";

            var numbers = new List<int>();

            var isOpen = true;

            while (isOpen)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nEneter command, commands:" +
                    $"\nenter number(write a number)." +
                    $"\n{SumCommand} - summar all numbers." +
                    $"\n{ExitCommand} - exit a program.");

                string userInput = Console.ReadLine().ToUpper();

                switch (userInput)
                {
                    case SumCommand:
                        var sumOfAllNumbers = numbers.Sum();
                        Console.WriteLine($"Sum of all user input is {sumOfAllNumbers}.");
                        break;
                    case ExitCommand:
                        isOpen = false;
                        break;
                    default:
                        if (int.TryParse(userInput, out int userInputNumber))
                        {
                            numbers.Add(userInputNumber);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Cant sellect a command type!");
                        }
                        break;
                }
            }
        }
    }
}

using System;
using System.Linq;

namespace ConsoleCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string ExitCommand = "EXIT";
            const string SumCommand = "SUM";

            var numbers = new int[0];

            var isOpen = true;

            while(isOpen)
            {
                Console.WriteLine($"\nEneter command, commands:" +
                    $"\nenter number(write a number)." +
                    $"\n{SumCommand} - summar all numbers." +
                    $"\n{ExitCommand} - exit a program.");

                string userInput = Console.ReadLine().ToUpper();

                switch (userInput)
                {
                    case ExitCommand:
                        isOpen = false;
                        break;
                    case SumCommand:
                        var sumOfAllNumbers = 0;
                        foreach (var number in numbers)
                            sumOfAllNumbers += number;
                        Console.WriteLine($"Sum of all user input is {sumOfAllNumbers}.");
                        break;
                    //Add a new element.
                    default:
                        var userInputNumber = Convert.ToInt32(userInput);

                        #region extentArray
                        var newUserInputArray = new int[numbers.Length + 1];

                        for(int i = 0; i < numbers.Length; i++)
                        {
                            newUserInputArray[i] = numbers[i];
                        }

                        numbers = newUserInputArray;
                        #endregion extentArray

                        numbers[numbers.Length - 1] = userInputNumber;
                        break;
                }
            }
        }
    }
}

using System;
using System.Linq;

namespace ConsoleCalculator
{
    internal class Program
    {
        const string ExitCommand = "EXIT";
        const string SumCommand = "SUM";

        static void Main(string[] args)
        {
            var userInputArray = new int[0];

            var isOpen = true;

            while(isOpen)
            {
                Console.WriteLine($"\nEneter command, commands:" +
                    $"\nenter number(write a number)." +
                    $"\n{SumCommand} - summar all numbers." +
                    $"\n{ExitCommand} - exit a program.");

                string userInput;

                switch (userInput = Console.ReadLine().ToUpper())
                {
                    case ExitCommand:
                        isOpen = false;
                        break;
                    case SumCommand:
                        var sumOfAllUserInput = userInputArray.Sum();
                        Console.WriteLine($"Sum of all user input is {sumOfAllUserInput}.");
                        break;
                    //Add a new element.
                    default:
                        var userInputNumber = Convert.ToInt32(userInput);

                        #region extentArray
                        var newUserInputArray = new int[userInputArray.Length + 1];

                        for(int i = 0; i < userInputArray.Length; i++)
                        {
                            newUserInputArray[i] = userInputArray[i];
                        }

                        userInputArray = newUserInputArray;
                        #endregion extentArray

                        userInputArray[userInputArray.Length - 1] = userInputNumber;
                        break;
                }
            }
        }
    }
}

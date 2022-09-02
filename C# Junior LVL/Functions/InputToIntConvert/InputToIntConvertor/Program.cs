using System;

namespace InputToIntConvertor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GetUserInputInt();

            Console.ReadKey();
        }

        private static int GetUserInputInt()
        {
            int userInput;
            Console.Write("Enter a number:");
            
            while(int.TryParse(Console.ReadLine(), out userInput) == false)
            {
                Console.Write("Invalid Input, enter a number:");
            }

            return userInput;
        }
    }
}

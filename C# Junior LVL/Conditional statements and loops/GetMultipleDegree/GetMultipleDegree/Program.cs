using System;

namespace GetMultipleDegree
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int multiplyingPower = 2;

            Console.Write("Enter a number: ");
            var userInput = Convert.ToInt32(Console.ReadLine());

            int currentmultiplying = 1; 

            while(Math.Pow(multiplyingPower, currentmultiplying) < userInput)
            {
                currentmultiplying++;
            }

            Console.WriteLine($"Minimal multiplying of {multiplyingPower} thet larger then {userInput} is " +
                $"{Math.Pow(multiplyingPower, currentmultiplying)} ({multiplyingPower} ^ {currentmultiplying}).");
            Console.ReadKey();
        }
    }
}

using System;

namespace SecretMesage
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string secretMessage = $"47°41'32.2\"N 31°30'10.8\"E";
            string passworld = "helix";

            uint leftTryCount = 3;

            Console.Write("Enter passworld: ");
            var userInput = Console.ReadLine();
            leftTryCount--;

            while (userInput != passworld && leftTryCount > 0)
            {
                Console.Write($"Wrong passworld,left try count {leftTryCount},enter passworld: ");
                userInput = Console.ReadLine();

                leftTryCount--;
            }

            if(leftTryCount > 0)
            {
                Console.WriteLine($"Secret message: {secretMessage}.");
            }
            else
            {
                Console.WriteLine($"You passed all trys to enter passworld!");
            }

            Console.ReadKey();
        }
    }
}

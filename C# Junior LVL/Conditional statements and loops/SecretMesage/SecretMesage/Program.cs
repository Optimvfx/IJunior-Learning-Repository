using System;

namespace SecretMesage
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string secretMessage = $"47°41'32.2\"N 31°30'10.8\"E";
            string passworld = "helix";

            uint tryCount = 3;

            for(int currentTry = 1; currentTry <= tryCount; currentTry++)
            {
                Console.Write("Enter passworld: ");
                var userInput = Console.ReadLine(); 
                if(userInput == passworld)
                {
                    Console.WriteLine($"SecretMessage is {secretMessage}");
                    break;
                }
                else
                {
                    Console.WriteLine($"Wrong passworld, left try count {tryCount - currentTry}");
                }
            }

            Console.ReadKey();
        }
    }
}

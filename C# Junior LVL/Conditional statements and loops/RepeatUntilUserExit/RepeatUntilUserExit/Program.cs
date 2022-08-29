using System;

//Task of https://lk.ijunior.ru/Homework/Detail/122
namespace RepeatUntilUserExit
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool isExit = false;

            while (isExit == false)
            {
                Console.WriteLine("Enter Command:");
                var userCommand = Console.ReadLine().ToLower();

                switch (userCommand)
                {
                    case "exit":
                        isExit = true;
                        break;
                    default:
                        Console.WriteLine("Unknown Command.");
                        break;
                }
            }
        }
    }
}

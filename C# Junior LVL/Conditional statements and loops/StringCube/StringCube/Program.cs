﻿using System;

namespace StringCube
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string userInput = Console.ReadLine();
            char strokeChar = Console.ReadLine()[0];

            for (int y = -1; y <= 1; y++)
            {
                for (int x = -1; x <= userInput.Length; x++)
                {
                    if (x >= 0 && x < userInput.Length && y == 0)
                    {
                        Console.Write(userInput[x]);
                    }
                    else
                    {
                        Console.Write(strokeChar);
                    }
                }
                Console.Write("\n");
            }

            Console.ReadKey();
        }
    }
}

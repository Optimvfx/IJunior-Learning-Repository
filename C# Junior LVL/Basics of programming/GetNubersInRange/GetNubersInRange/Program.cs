﻿using System;

namespace GetNubersInRange
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int linealGrow = 7;
            int startNuber = 5;
            int maximalNumber = 100;

            for (var currentNuber = startNuber; currentNuber < maximalNumber; currentNuber += linealGrow)
            {
                Console.WriteLine(currentNuber);
            }

            Console.ReadKey();
        }
    }
}

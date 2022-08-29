using System;

//Task of https://lk.ijunior.ru/Homework/Detail/35
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

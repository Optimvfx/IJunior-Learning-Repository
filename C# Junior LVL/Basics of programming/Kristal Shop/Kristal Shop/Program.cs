using System;

namespace Kristal_Shop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int crystalPrice = 5;

            int crystalCount = 0;
            Console.Write("У вас монет: ");
            int goldCount = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"Сколько вы хотите купить крисаталов по цене {crystalPrice}?");
            int buyedCristalCount = Convert.ToInt32(Console.ReadLine());
             
            goldCount -= buyedCristalCount * crystalPrice;
            crystalCount += buyedCristalCount;

            Console.WriteLine($"У вас: {goldCount} золота, {crystalCount} кристалов.");
            Console.ReadKey();
        }
    }
}

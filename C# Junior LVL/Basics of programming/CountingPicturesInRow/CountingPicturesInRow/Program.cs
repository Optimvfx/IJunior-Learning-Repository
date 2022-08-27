//Task of https://lk.ijunior.ru/Homework/Detail/28
using System;

namespace CountingPicturesInRow
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var picturesRowLength = 3;

            var userPicturesCount = 52;

            var filledPicturesRowsCount = userPicturesCount / picturesRowLength;
            var overRowPicturesCount = userPicturesCount % picturesRowLength;

            Console.WriteLine($"Количество картинок игрока - {userPicturesCount}, картинки смогли полностью заполнить {filledPicturesRowsCount}, с остатков в размере {overRowPicturesCount}.");
            Console.ReadKey();
        }
    }
}

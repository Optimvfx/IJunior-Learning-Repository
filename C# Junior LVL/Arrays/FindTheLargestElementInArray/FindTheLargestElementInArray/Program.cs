using System;

namespace FindTheLargestElementInArray
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var array = new float[10, 10];

            #region randomFillArray
            var random = new Random();

            var minimalRandomNumber = 0;
            var maximalRandomNumber = 100;

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = random.Next(minimalRandomNumber, maximalRandomNumber);
                }
            }
            #endregion randomFillArray

            var maximalElement = float.MinValue;

            foreach(var element in array)
            {
                if (element >= maximalElement)
                    maximalElement = element;
            }

            var newValueOfMaximalElements = 0;

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write(array[i, j] + "\t");
                }
                Console.WriteLine();
            }

            Console.WriteLine($"\nMaximal element is {maximalElement}.\n");

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for(int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] >= maximalElement)
                        array[i, j] = newValueOfMaximalElements;

                    Console.Write(array[i, j] + "\t");
                }
                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}

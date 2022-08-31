using System;

namespace ArrayTests
{
    internal class Program
    {
        static void Main(string[] args)
        {
            float[,] array = new float[5, 5];

            //В задачи не написано но я решил сделать радом ввод в масив. 
            #region randomFillArray
            var random = new Random();

            var minimalRandomNumber = 1;
            var maximalRandomNumber = 9;

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = random.Next(minimalRandomNumber, maximalRandomNumber);
                }
            }
            #endregion randomFillArray

            var lineIndex = 2;
            var columnIndex = 1;

            var sumOfLine = 0f;
            var multiplyOfColumn = 1f;

            for (int j = 0; j < array.GetLength(1); j++)
            {
                sumOfLine += array[lineIndex, j];
            }

            for (int i = 0; i < array.GetLength(0); i++)
            {
                    multiplyOfColumn *= array[i, columnIndex];
            }

            Console.WriteLine($"Sum of all number in line {lineIndex} is {sumOfLine}." +
                $"\nMultiply of all number in column {columnIndex} is {multiplyOfColumn}.\n");

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write(array[i, j] + " ");
                }
                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}

using System;
using System.Text;

namespace ArrayQuickSort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var array = new float[10000];

            #region randomFillArray
            var random = new Random();

            var maximalRandomNumber = 100;

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = (float)random.NextDouble() * maximalRandomNumber;
            }
            #endregion randomFillArray

            var arrayOutput = new StringBuilder();

            foreach (var value in array)
                arrayOutput.AppendLine(value.ToString());

            //Перенес свой Java алгоритм, на c#, для самого выгодного быстродействия(n - O(n * log2(n)) - n^2).
            QuicSortArray(array);

            Console.WriteLine(arrayOutput.ToString() + "\nPrewius array!");

            arrayOutput = new StringBuilder();

            foreach (var value in array)
                arrayOutput.AppendLine(value.ToString());

            Console.WriteLine(arrayOutput.ToString() + "\nArray sorted!");

            Console.ReadKey();
        }

        public static void QuicSortArray(float[] array)
        {
            QuicSortArray(array, 0, array.Length - 1);
        }
        private static void QuicSortArray(float[] array, int leftBorderIndex, int rightBorderIndex)
        {
            if (leftBorderIndex >= rightBorderIndex)
                return;

            var midellIndex = QuicSortPartition(array, leftBorderIndex, rightBorderIndex);

            QuicSortArray(array,
                     leftBorderIndex,
                     midellIndex - 1);
            QuicSortArray(array,
                     midellIndex + 1,
                     rightBorderIndex);
        }
        private static int QuicSortPartition(float[] array, int leftBorderIndex, int rightBorderIndex)
        {
            var midellIndex = leftBorderIndex;
            float temp;

            for (int i = leftBorderIndex; i < rightBorderIndex; i++)
            {
                if (array[i] <= array[rightBorderIndex])
                {
                    temp = array[midellIndex];
                    array[midellIndex] = array[i];
                    array[i] = temp;
                    midellIndex++;
                }
            }

            temp = array[midellIndex];
            array[midellIndex] = array[rightBorderIndex];
            array[rightBorderIndex] = temp;

            return midellIndex;
        }
    }
}

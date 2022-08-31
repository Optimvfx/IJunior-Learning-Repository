using System;


namespace FindLockalMaximum
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var array = new float[30];

            #region randomFillArray
            var random = new Random();

            var maximalRandomNumber = 100;

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = (float)random.NextDouble() * maximalRandomNumber;
            }
            #endregion randomFillArray

            var localMaximumColor = ConsoleColor.Green;
            var notLocalMaximumColor = ConsoleColor.Red;

            for (int i = 0; i < array.Length; i++)
            {
                bool elementIsLocalMaximum;

                if (i <= 0 && i >= array.Length)
                {
                    elementIsLocalMaximum = true;
                }
                else if (i <= 0)
                {
                    elementIsLocalMaximum = array[i] > array[i + 1];
                }
                else if (i >= array.Length - 1)
                {
                    elementIsLocalMaximum = array[i] > array[i - 1];
                } 
                else
                {
                    elementIsLocalMaximum = array[i] > array[i + 1] && array[i] > array[i - 1];
                }

                if (elementIsLocalMaximum)
                    Console.ForegroundColor = localMaximumColor;
                else
                    Console.ForegroundColor = notLocalMaximumColor;

                Console.WriteLine(array[i]);
            }

            Console.ReadKey();
        }
    }
}

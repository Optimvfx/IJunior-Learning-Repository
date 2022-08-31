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

            #region firstElement
            if (array[0] > array[1])
                Console.ForegroundColor = localMaximumColor;
            else
                Console.ForegroundColor = notLocalMaximumColor;

            Console.WriteLine(array[0]);
            #endregion firstElement

            for (int i = 1; i < array.Length - 1; i++)
            {
                if (array[i] > array[i + 1] && array[i] > array[i - 1])
                    Console.ForegroundColor = localMaximumColor;
                else
                    Console.ForegroundColor = notLocalMaximumColor;

                Console.WriteLine(array[i]);
            }

            #region lastElement
            if (array[array.Length - 1] > array[array.Length - 2])
                Console.ForegroundColor = localMaximumColor;
            else
                Console.ForegroundColor = notLocalMaximumColor;

            Console.WriteLine(array[array.Length - 1]);
            #endregion lastElement

            Console.ReadKey();
        }
    }
}

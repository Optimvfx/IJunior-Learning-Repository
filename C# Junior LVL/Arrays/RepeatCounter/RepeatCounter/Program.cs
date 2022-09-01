using System;

namespace RepeatCounter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var array = new int[10];

            #region randomFillArray
            var random = new Random();

            var maximalRandomNumber = 5;

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(maximalRandomNumber);
            }
            #endregion randomFillArray

            int mostRepeatedNumber = 0;
            int mostRepeatedNumberRepeatCount = int.MinValue;

            var currentNumberRepeatCount = 0;

            for (int i = 0; i < array.Length - 1; i++)
            {
                Console.WriteLine(array[i]);

                if (array[i] == array[i + 1])
                {
                    currentNumberRepeatCount++;
                }
                else
                {
                    currentNumberRepeatCount = 0;
                }
   
                if (mostRepeatedNumberRepeatCount < currentNumberRepeatCount)
                {
                    mostRepeatedNumber = array[i];
                    mostRepeatedNumberRepeatCount = currentNumberRepeatCount + 1;
                }
            }

            Console.WriteLine(array[array.Length - 1]);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"The largest list of repeat was with {mostRepeatedNumber} it was {mostRepeatedNumberRepeatCount} repeats.");

            Console.ReadKey();
        }
    }
}

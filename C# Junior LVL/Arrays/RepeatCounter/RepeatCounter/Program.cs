using System;

namespace RepeatCounter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var array = new int[30];

            #region randomFillArray
            var random = new Random();

            var maximalRandomNumber = 5;

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(maximalRandomNumber);
            }
            #endregion randomFillArray

            var repeatColor = ConsoleColor.Green;
            var notrepeatColor = ConsoleColor.Red;

            int mostRepeatedNumber = 0;
            int mostRepeatedNumberRepeatCount = int.MinValue;

            for (int i = 0; i < array.Length; i++)
            {
                var repeatCount = 0;

                if (i < array.Length - 1 && array[i] == array[i + 1])
                {
                    Console.ForegroundColor = repeatColor;         

                    while (i < array.Length - 1 && array[i] == array[i + 1])
                    {
                        Console.WriteLine(array[i]);

                        i++;
                        repeatCount++;
                    }

                    Console.WriteLine(array[i]);
                    repeatCount++;
                }
                else
                {
                    Console.ForegroundColor = notrepeatColor;
                    Console.WriteLine(array[i]);
                }

                if (mostRepeatedNumberRepeatCount < repeatCount)
                {
                    mostRepeatedNumber = array[i];
                    mostRepeatedNumberRepeatCount = repeatCount;
                }
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"The largest list of repeat was with {mostRepeatedNumber} it was {mostRepeatedNumberRepeatCount} repeats.");

            Console.ReadKey();
        }
    }
}

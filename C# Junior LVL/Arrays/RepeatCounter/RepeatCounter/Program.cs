using System;

namespace RepeatCounter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var array = new int[] {4,4,4,5,1,1,3,5,1,5,7,4,6,3,8,3,6,27,73,87,16,2,6,17,1,7,8,2,8,2,7,2,8,1,8,2,8,3,7,4,6,7,8,2,6,7,8,2,6,7};

            int mostRepeatedNumber = 0;
            int mostRepeatedNumberRepeatCount = int.MinValue;

            int currentNumberRepeatCount = 1;

            for (int i = 0; i < array.Length - 1; i++)
            {
                Console.WriteLine(array[i]);

                if (array[i] == array[i + 1])
                {
                    currentNumberRepeatCount++;
                }
                else
                {
                    currentNumberRepeatCount = 1;
                }
   
                if (mostRepeatedNumberRepeatCount < currentNumberRepeatCount)
                {
                    mostRepeatedNumber = array[i];
                    mostRepeatedNumberRepeatCount = currentNumberRepeatCount;
                }
            }

            Console.WriteLine(array[array.Length - 1]);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"The largest list of repeat was with {mostRepeatedNumber} it was {mostRepeatedNumberRepeatCount} repeats.");

            Console.ReadKey();
        }
    }
}

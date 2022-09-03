using System;

namespace ShuffleArray
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var array = new int[]{1,2,3,4,5,6,7,8,9,10};

            Shuffle(array,new Random());

            foreach (var element in array)
                Console.WriteLine(element);

            Console.ReadKey();
        }

        private static int[] Shuffle(int[] array, Random random)
        {
            for (int i = 0; i < array.Length; i++)
            {
                int randomIndex = random.Next(array.Length);
                int temp = array[i];
                array[i] = array[randomIndex];
                array[randomIndex] = temp;
            }

            return array;
        }
    }
}

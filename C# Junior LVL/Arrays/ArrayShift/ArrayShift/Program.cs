using System;

namespace ArrayShift
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var array = new float[] {1,2,3,4};

            foreach (var value in array)
                Console.WriteLine(value + " ");

            Console.Write("Enter array shift: ");
            int arrayShift = Convert.ToInt32(Console.ReadLine());

            for (int shift = 0; shift < arrayShift; shift++)
            {
                var prewiusNumber = array[array.Length - 1];
                array[array.Length - 1] = array[0];

                for (int i = array.Length - 2; i >= 0; i--)
                {
                    var temp = prewiusNumber;
                    prewiusNumber = array[i];
                    array[i] = temp;
                }
            }
            foreach (var value in array)
                Console.WriteLine(value);

            Console.ReadKey();
        }
    }
}

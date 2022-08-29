using System;

//Task of https://lk.ijunior.ru/Homework/Detail/202
namespace RandomMultiples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            uint multiplicityIndexFirst = 3;
            uint multiplicityIndexSecond = 5;

            uint maximalRandomNumber = 100;

            int rangeOfNumbersLength = random.Next((int)maximalRandomNumber);

            var sumOfAllMultiplicityNumbers = 0;

            for (int i = 0; i < rangeOfNumbersLength; i++)
            {
                if (i % multiplicityIndexFirst <= 0 || i % multiplicityIndexSecond <= 0)
                {
                    sumOfAllMultiplicityNumbers += i;
                    Console.WriteLine(i);
                }
            }

            Console.WriteLine($"Sum of all numbers which multiplicity {multiplicityIndexFirst} and {multiplicityIndexSecond} in range from 0 to {rangeOfNumbersLength} is {sumOfAllMultiplicityNumbers}.");
            Console.ReadKey();
        }
    }
}

using System;

//Task of https://lk.ijunior.ru/Homework/Detail/202
namespace RandomMultiples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            uint multiplicityIndex = 3;
            uint multiplicityMaximalRange = 0;

            uint maximalRandomNumber = 100;

            int rangeOfNumbersLength = random.Next((int)maximalRandomNumber);

            var sumOfAllMultiplicityNumbers = 0;

            for (int i = 0; i < rangeOfNumbersLength; i++)
            {
                if (i % multiplicityIndex > multiplicityMaximalRange)
                {
                    sumOfAllMultiplicityNumbers += i;
                    Console.WriteLine(i);
                }
            }

            Console.WriteLine($"Sum of all numbers which multiplicity {multiplicityIndex} in range from 0 to {rangeOfNumbersLength} is {sumOfAllMultiplicityNumbers}.");
            Console.ReadKey();
        }
    }
}

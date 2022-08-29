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

            uint minimalRandomNumber = 0, maximalRandomNumber = 100;

            int rangeOfNumbersLength = random.Next((int)minimalRandomNumber, (int)maximalRandomNumber);

            var sumOfAllMultiplicityNumbers = 0;

            for (int i = (int)minimalRandomNumber; i < rangeOfNumbersLength; i++)
            {
                if (i % multiplicityIndex > multiplicityMaximalRange)
                    continue;

                sumOfAllMultiplicityNumbers += i;
                Console.WriteLine(i);
            }

            Console.WriteLine($"Sum of all numbers which multiplicity {multiplicityIndex} in range from {minimalRandomNumber} to {rangeOfNumbersLength} is {sumOfAllMultiplicityNumbers}.");
            Console.ReadKey();
        }
    }
}

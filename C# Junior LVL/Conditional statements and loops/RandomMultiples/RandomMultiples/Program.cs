using System;

//Task of https://lk.ijunior.ru/Homework/Detail/202
namespace RandomMultiples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            uint minimalMultiplicityIndex = 1;
            uint maximalMultiplicityIndex = 27;

            var multiplicityIndex = random.Next((int)minimalMultiplicityIndex,(int)maximalMultiplicityIndex);

            var naturalNumbersThetMultiplicityCount = 0;

            for (int i = 0; i < 1000; i += multiplicityIndex) 
            {
               if(i >= 100)
                {
                    Console.WriteLine(i);
                    naturalNumbersThetMultiplicityCount++;
                }
            }

            Console.WriteLine($"Natural numbers thet multiplicity {multiplicityIndex} count {naturalNumbersThetMultiplicityCount}");
            Console.ReadKey();
        }
    }
}

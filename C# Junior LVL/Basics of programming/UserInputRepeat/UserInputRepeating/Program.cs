using System;

namespace UserInputRepeating
{
    internal class Program
    {
        static void Main(string[] args)
        {
            uint minimalRepeatCount = uint.MinValue;

            Console.Write("Введите строку: ");
            var userInputString = Console.ReadLine();
            Console.Write("Введите количество повторов: ");
            var repeatCount = Convert.ToInt32(Console.ReadLine());
            
            if(repeatCount < minimalRepeatCount)
            {
                Console.WriteLine($"Вы ввели {repeatCount}, что меньше чем минимально допустимое значение({minimalRepeatCount})");
                Console.ReadKey();
                return;
            }

            for(int repeat = 1; repeat <= repeatCount; repeat++)
            {
                Console.WriteLine($"{userInputString} || Повтор номер {repeat}");
                Console.ReadKey();
            }
        }
    }
}

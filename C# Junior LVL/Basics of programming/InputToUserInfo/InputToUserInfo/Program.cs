using System;

//Task of https://lk.ijunior.ru/Homework/Detail/58
namespace InputToUserInfo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите свое имя: ");
            var userName = Console.ReadLine();

            Console.WriteLine("Введите свою должность: ");
            var userPost = Console.ReadLine();

            Console.WriteLine("Введите свою зарплату: ");
            var userSalaryInUSD = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"Вас зовут {userName},Вы работаете на посту {userPost} и получаете зарплату в размере {userSalaryInUSD} USD.");
            Console.ReadKey();
        }
    }
}

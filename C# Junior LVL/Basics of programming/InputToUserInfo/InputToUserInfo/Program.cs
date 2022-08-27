using System;

//Task of https://lk.ijunior.ru/Homework/Detail/58
namespace InputToUserInfo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var userName = Console.ReadLine();
           
            var userPost = Console.ReadLine();

            var userSalary = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"Вас зовут {userName},Вы работаете на посту {userPost} и получаете зарплату в размере {userSalary}.");
        }
    }
}

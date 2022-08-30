using System;
using System.Text;

namespace StringCube
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter string: ");
            string userInput = Console.ReadLine();
            Console.Write("Enter stroke: ");
            char strokeChar = Console.ReadLine()[0];

            StringBuilder stroke = new StringBuilder();

            for (int i = 0; i < userInput.Length + 2; i++)
            {
               stroke.Append(strokeChar);
            }

            Console.WriteLine(stroke.ToString());
            Console.WriteLine(strokeChar + userInput + strokeChar);
            Console.WriteLine(stroke.ToString());

            Console.ReadKey();
        }
    }
}

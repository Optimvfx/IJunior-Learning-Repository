using System;
using System.Collections.Generic;

namespace Shop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Queue<uint> shoppingQueue = new Queue<uint>();

            shoppingQueue.Enqueue(141);
            shoppingQueue.Enqueue(22);
            shoppingQueue.Enqueue(613);
            shoppingQueue.Enqueue(4424);
            shoppingQueue.Enqueue(155);

            uint storeCheckoutMoney = 0;

            int timeBetweenBreaks = 1000;

            bool shoppingQueueEmpty = false;

            while (shoppingQueueEmpty == false)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;

                Console.WriteLine($"At the store checkout {storeCheckoutMoney} UAH.");
                Console.WriteLine($"At the checkout stay a client client with a purchase for {shoppingQueue.Peek()} UAH, to break through the purchase press any key.");

                Console.ReadKey();

                Console.WriteLine($"You have punched the goods for the amount {shoppingQueue.Peek()} UAH.");
                storeCheckoutMoney += shoppingQueue.Dequeue();

                shoppingQueueEmpty = shoppingQueue.Count == 0;

                System.Threading.Thread.Sleep(timeBetweenBreaks);
            }

            Console.Clear();
            Console.WriteLine("You have completed all purchases, go home.");
            Console.ReadKey();
        }
    }
}

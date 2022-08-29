using System;
using System.Linq;

//Task of https://lk.ijunior.ru/Homework/Detail/34
namespace ConsoleOfShop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Posible Commands:
            //Esc
            //Enter a userMessage.
            //Write userMessage.
            //encrypt with a Caesar cipher.
            //decipher with the Caesar cipher.
            //Set confusion with a Caesar cipher.
            //to higher register.
            //to lower register.

            var escCommandSended = false;

            var userMessage = "";

            var encryptConfusion = 0;

            while(escCommandSended == false)
            {

                Console.WriteLine($"\nPosible commands:" +
                    $"\nESC - esc program" +
                    $"\nENT - enter a userMessage" +
                    $"\nWRT - write a userMessage" +
                    $"\nENC - encrypt userMessage with a Caesar cipher." +
                    $"\nUNENC - unencrypt userMessage with a Caesar cipher." +
                    $"\nSETCONF - set confusion with a Caesar cipher." +
                    $"\nTOHIG - to higher register userMessage." +
                    $"\nTOLOW - to lower register userMessage.");

                Console.Write("\nCommand: ");
                var command = Console.ReadLine().ToUpper();

                switch(command)
                {
                    case "ESC":
                        escCommandSended = true;
                        break;
                    case "ENT":
                        Console.Write("Enter message: ");
                        userMessage = Console.ReadLine();
                        break;
                    case "WRT":
                        Console.WriteLine($"Message: {userMessage}");
                        break;
                    case "ENC":
                        //Использую Linq чтоб не засорять код
                        var encryptedMessageChars = userMessage.Select(chr => (byte)chr)
                                         .Select(charByteCode => charByteCode + encryptConfusion)
                                         .Select(confusionedByteCode => (char) confusionedByteCode)
                                         .ToArray();

                        userMessage = new string(encryptedMessageChars);
                        break;
                    case "UNENC":
                        //Использую Linq чтоб не засорять код
                        var unencryptedMessageChars = userMessage.Select(chr => (byte)chr)
                                         .Select(charByteCode => charByteCode - encryptConfusion)
                                         .Select(confusionedByteCode => (char)confusionedByteCode)
                                         .ToArray();

                        userMessage = new string(unencryptedMessageChars);
                        break;
                    case "SETCONF":
                        Console.Write("Encrtypt Confusion: ");
                        encryptConfusion = Convert.ToInt32(Console.ReadLine());
                        break;

                    case "TOHIG":
                        userMessage = userMessage.ToUpper();
                        break;

                    case "TOLOW":
                        userMessage = userMessage.ToUpper();
                        break;

                    default:
                        Console.WriteLine("Uncnown command!");
                        break;
                }
            }
        }
    }
}

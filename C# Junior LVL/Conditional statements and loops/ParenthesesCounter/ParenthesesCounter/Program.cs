using System;

namespace ParenthesesCounter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var leftbracket = '(';
            var rightbracket = ')';

            Console.Write("Write brackets: ");
            var userInputBrackets = Console.ReadLine();

            var offsetWeightBrackets = 0;
            var dephtMaximum = 0;
            var userInputIsInvalid = false;

            foreach (var chr in userInputBrackets)
            {
                if (chr == leftbracket)
                {
                    offsetWeightBrackets++;
                }
                else if (chr == rightbracket)
                {
                    offsetWeightBrackets--;
                }

                dephtMaximum = Math.Max(dephtMaximum, offsetWeightBrackets);

                #region inVariant
                if (offsetWeightBrackets < 0)
                {
                    userInputIsInvalid = true;
                }
                #endregion inVariant
            }

            if(offsetWeightBrackets != 0)
            {
                userInputIsInvalid = true;
            }

            #region output
            Console.Write($"User input is ");
            if (userInputIsInvalid)
            {
                Console.WriteLine("Invalid");
            }
            else
            {
                Console.WriteLine($"Valid, maximal depth is {dephtMaximum}");
            }

            Console.ReadKey();
            #endregion output
        }
    }
}

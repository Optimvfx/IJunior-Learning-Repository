using System;

namespace DrowBar
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DrawBar(56f, 20, ConsoleColor.Red);
            DrawBar(80f, 30, ConsoleColor.Blue, PositionY: 1);

            Console.ReadKey();
        }

        private static void DrawBar(float fillProcent,uint barLength, ConsoleColor fillColor, ConsoleColor unFillColor = ConsoleColor.Black,int positionX = 0,int PositionY = 0, char fillChar = '#',char unFillChar = '_')
        {
            float minimalFillProcent = 0f;
            float maximalFillProcent = 100f;

            fillProcent = Math.Max(Math.Min(fillProcent, maximalFillProcent), minimalFillProcent);
            int relativeFillProcent = (int)(fillProcent / 100 * barLength);

            Console.SetCursorPosition(positionX, PositionY);

            var prewiusColor = Console.BackgroundColor;
            Console.Write("[");
            
            for(int i = 0; i < barLength; i++)
            {
                if(relativeFillProcent > i)
                {
                    Console.BackgroundColor = fillColor;
                    Console.Write(fillChar);
                }
                else
                {
                    Console.BackgroundColor = unFillColor;
                    Console.Write(unFillChar);
                }
            }

            Console.BackgroundColor = prewiusColor;
            Console.Write("]");
        }
    }
}

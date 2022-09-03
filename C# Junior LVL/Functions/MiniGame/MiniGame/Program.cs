using System;
using System.IO;
using System.Linq;

namespace MiniGame
{
    internal class Program
    {
        const char filePlayerChar = '@';
        const char mapPlayerChar = '@';

        const char fileEmptyChar = '0';
        const char mapEmptyChar = ' ';

        const char fileWallChar = '#';
        const char mapWallChar = '#';

        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            int frameScrolSpeed = 50;

            int playerPositionX;
            int playerPositionY;

            var map = ReadMapFromFile("Map", out playerPositionX, out playerPositionY);

            bool isOpen = true;
            
            while(isOpen)
            {
                DoMapMovments(ref map, ref playerPositionX, ref playerPositionY);
                DrawMap(map, playerPositionX, playerPositionY);

                System.Threading.Thread.Sleep(frameScrolSpeed);
            }

        }

        #region read
        private static char[,] ReadMapFromFile(string mapFileName,out int playerPositionX,out int playerPositionY)
        {
            playerPositionX = 0;
            playerPositionY = 0;

            string[] mapPrintFromFile = File.ReadAllLines($"Maps/{mapFileName}.txt");

            var map = new char[mapPrintFromFile.Length, mapPrintFromFile[0].Length];

            for(int i = 0; i < mapPrintFromFile.Length; i++)
            {
                for(int j = 0; j < mapPrintFromFile[i].Length; j++)
                {
                    if (mapPrintFromFile[i][j] == fileEmptyChar)
                    {
                        map[i, j] = mapEmptyChar;
                    }
                    else if (mapPrintFromFile[i][j] == fileWallChar)
                    {
                        map[i, j] = mapWallChar;
                    }
                    else if (mapPrintFromFile[i][j] == filePlayerChar)
                    {
                        playerPositionX = i;
                        playerPositionY = j;
                    }
                }
            }

            return map;
        }
        #endregion read

        #region draw
        private static void DrawMap(char[,] map, int playerPositionX, int playerPositionY)
        {
            for(int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    Console.SetCursorPosition(y, x);

                    if (x == playerPositionX && y == playerPositionY)
                    {
                        Console.Write(mapPlayerChar);
                    }
                    else
                    {
                        Console.Write(map[x, y]);
                    }
                }
            }
        }
        #endregion draw

        #region move
        private static void DoMapMovments(ref char[,] map,ref int playerPositionX,ref int playerPositionY)
        {
            DoPlayerMovments(map, ref playerPositionX, ref playerPositionY);
        }

        private static void DoPlayerMovments(char[,] map,ref int playerPositionX,ref int playerPositionY)
        {
            GetPlayerMoveDirection(out int playerMoveDirectionY, out int playerMoveDirectionX);

            if(IsWall(map,playerPositionX + playerMoveDirectionX,playerPositionY + playerMoveDirectionY) == false)
            {
                playerPositionX += playerMoveDirectionX;
                playerPositionY += playerMoveDirectionY;
            }
        }

        private static void GetPlayerMoveDirection(out int playerMoveDirectionVertical, out int playerMoveDirectionHorizontal)
        {
            const ConsoleKey directionKeyUp = ConsoleKey.W;
            const ConsoleKey directionKeyDown = ConsoleKey.S;
            const ConsoleKey directionKeyLeft = ConsoleKey.A;
            const ConsoleKey directionKeyRight = ConsoleKey.D;

            playerMoveDirectionVertical = 0;
            playerMoveDirectionHorizontal = 0;

            if(Console.KeyAvailable)
            {
                var inputedKey = Console.ReadKey(true).Key;

                switch(inputedKey)
                {
                    case directionKeyDown:
                        playerMoveDirectionHorizontal++;
                        break;
                    case directionKeyUp:
                        playerMoveDirectionHorizontal--;
                        break;
                    case directionKeyLeft:
                        playerMoveDirectionVertical--;
                        break;
                    case directionKeyRight:
                        playerMoveDirectionVertical++;
                        break;
                }
            }
        }
        #endregion move

        #region helping
        private static bool IsWall(char[,] map, int x, int y)
        {
            if (x < 0 || y < 0 || x >= map.GetLength(0) || y >= map.GetLength(1))
                return true;

            return map[x, y] == mapWallChar;
        }
        #endregion helping
    }
}

﻿using System;
using System.IO;

namespace MiniGame
{
    internal class Program
    {
        private const char FilePlayerChar = '@';
        private const char MapPlayerChar = '@';

        private const char FileEmptyChar = '0';
        private const char MapEmptyChar = ' ';

        private const char FileWallChar = '#';
        private const char MapWallChar = '#';

        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            int frameScrolSpeed = 50;

            int playerPositionX;
            int playerPositionY;

            var map = ReadMapFromFile("Map", out playerPositionX, out playerPositionY);

            bool isOpen = true;

            while (isOpen)
            {
                MoveMapEntitys(map, ref playerPositionX, ref playerPositionY);
                DrawGame(map, playerPositionX, playerPositionY);

                System.Threading.Thread.Sleep(frameScrolSpeed);
            }

        }

        #region read
        private static char[,] ReadMapFromFile(string mapFileName, out int playerPositionX, out int playerPositionY)
        {
            playerPositionX = 0;
            playerPositionY = 0;

            string[] mapPrintFromFile = File.ReadAllLines($"Maps/{mapFileName}.txt");

            var map = new char[mapPrintFromFile.Length, mapPrintFromFile[0].Length];

            for (int i = 0; i < mapPrintFromFile.Length; i++)
            {
                for (int j = 0; j < mapPrintFromFile[i].Length; j++)
                {
                    if (mapPrintFromFile[i][j] == FileEmptyChar)
                    {
                        map[i, j] = MapEmptyChar;
                    }
                    else if (mapPrintFromFile[i][j] == FileWallChar)
                    {
                        map[i, j] = MapWallChar;
                    }
                    else if (mapPrintFromFile[i][j] == FilePlayerChar)
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
        private static void DrawGame(char[,] map, int playerPositionX, int playerPositionY)
        {
            DrawMap(map, playerPositionX, playerPositionY);
            DrawPlayer(playerPositionX, playerPositionY); 
        }

        private static void DrawMap(char[,] map, int playerPositionX, int playerPositionY)
        {
            for (int positionX = 0; positionX < map.GetLength(0); positionX++)
            {
                for (int positionY = 0; positionY < map.GetLength(1); positionY++)
                {
                    if (playerPositionX != positionX || playerPositionY != positionY)
                    {
                        Console.SetCursorPosition(positionY, positionX);
                        Console.Write(map[positionX, positionY]);
                    }
                }
            }
        }
        private static void DrawPlayer(int playerPositionX, int playerPositionY)
        {
            Console.SetCursorPosition(playerPositionY, playerPositionX);
            Console.Write(MapPlayerChar);
        }

        #endregion draw

        #region move
        private static void MoveMapEntitys(char[,] map,ref int playerPositionX,ref int playerPositionY)
        {
            MovePlayer(map, ref playerPositionX, ref playerPositionY);
        }

        private static void MovePlayer(char[,] map,ref int playerPositionX,ref int playerPositionY)
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

            return map[x, y] == MapWallChar;
        }
        #endregion helping
    }
}

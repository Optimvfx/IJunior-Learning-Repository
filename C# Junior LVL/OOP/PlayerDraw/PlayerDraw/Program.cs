using System;

namespace PlayerDraw
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var player = new Player('@',new Vector2Int(2, 9), "Joo");

            var gameFieldDrawer = new GameFieldDrawer(0, 10);

            gameFieldDrawer.DrawPlayer(player);

            Console.ReadKey();
        }
    }

    public class GameFieldDrawer
    {
        private readonly static int _horizontalPositionOfPlayerInfo = 0;
        private readonly static int _offsetGameFieldToPlayerInfo = 2;

        private readonly uint _minimalPosibleDrawPosition;
        private readonly uint _maximalPosibleDrawPosition;

        public GameFieldDrawer(uint minimalDrawPosition = 0, uint maximalDrawPosition = 100)
        {
            _minimalPosibleDrawPosition = minimalDrawPosition;
            _maximalPosibleDrawPosition = Math.Max(maximalDrawPosition, minimalDrawPosition);
        }

        public void DrawPlayer(Player player)
        {
            if (IsPlayerOutOfBounds(player))
            {
                Console.WriteLine("Cant draw player!");
            }
            else
            {
                Console.SetCursorPosition(player.Position.X, player.Position.Y);
                Console.Write(player.DrawChar);

                Console.SetCursorPosition(_horizontalPositionOfPlayerInfo, (int)_maximalPosibleDrawPosition + _offsetGameFieldToPlayerInfo);
                Console.WriteLine(player.GetStats());
            }
        }

        private bool IsPlayerOutOfBounds(Player player)
        {
            return player.Position.X < _minimalPosibleDrawPosition || player.Position.Y < _minimalPosibleDrawPosition ||
                   player.Position.X > _maximalPosibleDrawPosition || player.Position.Y > _maximalPosibleDrawPosition;
        }
    }

    public class Player
    {
        public readonly char DrawChar;

        private readonly string _name;

        public Vector2Int Position { get; private set; }

        public Player(char drawChar, Vector2Int position, string name)
        { 
            DrawChar = drawChar;
            Position = position;

            _name = name;
        }

        public string GetStats()
        {
            return $"Player: Name - {_name}.";
        }
    }

    public struct Vector2Int
    {
        public readonly int X;
        public readonly int Y;
    
        public Vector2Int(int x,int y)
        {
            X = x;
            Y = y;
        }
    }
}

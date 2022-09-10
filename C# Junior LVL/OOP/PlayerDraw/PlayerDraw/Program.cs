using System;

namespace PlayerDraw
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var player = new Player("Joo", new Vector2Int(2, 9));

            var gameFieldDrawer = new GameFieldDrawer('@', 0, 10);

            gameFieldDrawer.DrawPlayer(player);

            Console.ReadKey();
        }
    }

    public class GameFieldDrawer
    {
        private readonly char _playerDrawImage;

        private readonly uint _minimalPosibleDrawPosition;
        private readonly uint _maximalPosibleDrawPosition;

        public GameFieldDrawer(char playerDrawImage = '@', uint minimalDrawPosition = 0, uint maximalDrawPosition = 100)
        {
            _playerDrawImage = playerDrawImage;

            _minimalPosibleDrawPosition = minimalDrawPosition;
            _maximalPosibleDrawPosition = Math.Max(maximalDrawPosition, minimalDrawPosition);
        }

        public void DrawPlayer(Player player)
        {
            const int HorizontalPositionOfPlayerInfo = 0;
            const int OffsetGameFieldToPlayerInfo = 2;

            if (IsPlayerOutOfBounds(player))
            {
                Console.WriteLine("Cant draw player!");
            }
            else
            {
                Console.SetCursorPosition(player.Position.X, player.Position.Y);
                Console.Write(_playerDrawImage);

                Console.SetCursorPosition(HorizontalPositionOfPlayerInfo, (int)_maximalPosibleDrawPosition + OffsetGameFieldToPlayerInfo);
                Console.WriteLine(player.GetPlayerStats());
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
        private readonly string _name;

        public Vector2Int Position { get; private set; }

        public Player(string name, Vector2Int position)
        {
            _name = name;
            Position = position;
        }

        public string GetPlayerStats()
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

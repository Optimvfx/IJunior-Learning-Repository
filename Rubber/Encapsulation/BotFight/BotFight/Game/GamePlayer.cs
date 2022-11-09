using System;
using BotFight.Map;
using BotFight.Map.Entity;
using System.Threading;
using Extenstions;

namespace BotFight.Game
{
    public class GamePlayer
    {
        private readonly GameMap _map;

        private readonly int _updateTime;

        public GamePlayer(Array2D<IUpdatableEntity> mapCells, int gameUpdateTimeInMiliseconds)
        {
            if (gameUpdateTimeInMiliseconds <= 0)
                throw new ArgumentException();

            _updateTime = gameUpdateTimeInMiliseconds;

            _map = new GameMap(mapCells);
        }

        public void Play()
        {
            bool gameIsOpened = true;

            while(gameIsOpened)
            {
                _map.Update();

                Thread.Sleep(_updateTime);
            }
        }
    }
}

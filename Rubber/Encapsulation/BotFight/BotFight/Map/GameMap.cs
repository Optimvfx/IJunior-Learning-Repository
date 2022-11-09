using System;
using BotFight.Map.Entity;
using Extenstions;

namespace BotFight.Map
{
    public class GameMap : IReadOnlyMap
    {
        private readonly Array2D<IUpdatableEntity> _mapCells;

        public int Widht => _mapCells.Widht;

        public int Height => _mapCells.Height;

        public GameMap(Array2D<IUpdatableEntity> mapCells)
        {
            _mapCells = mapCells.Clone();
        }

        public void Update()
        {
            foreach (var entity in _mapCells.GetArray())
                entity.Update();
        }

        public bool IsSollidCell(int x, int y)
        {
            if (_mapCells.OutOfBounds(x, y))
                return true;

            return _mapCells[x, y].IsSolid;
        }
    }
}

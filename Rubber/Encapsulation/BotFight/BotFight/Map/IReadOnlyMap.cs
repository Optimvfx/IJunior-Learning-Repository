using System;

namespace BotFight.Map
{
    public interface IReadOnlyMap
    {
        int Widht { get;}
        int Height { get; }

        bool IsSollidCell(int x, int y);
    }
}

using System;

namespace BotFight.Map.Entity
{
    public interface IReadOnlyEntity
    {
        bool IsSolid { get; }
    }
}

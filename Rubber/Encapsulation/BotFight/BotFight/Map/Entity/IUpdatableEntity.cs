using System;

namespace BotFight.Map.Entity
{
    public interface IUpdatableEntity : IReadOnlyEntity
    {
        void Update();
    }
}

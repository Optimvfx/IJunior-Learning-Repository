using BotFight.Map.Entity;
using Extenstions;
using System; 

namespace BotFight.Map.Generator
{
    public interface IGameMapGenerator<T>
    {
        Array2D<IUpdatableEntity> Generate(T input);
    }
}

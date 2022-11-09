using BotFight.Map.Entity;
using Extenstions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BotFight.Map.Generator
{
    public class GameMapGeneratorByStrings : IGameMapGenerator<string[]>
    {
        public delegate IUpdatableEntity GetEntityDelegate();

        private readonly Dictionary<char, GetEntityDelegate> _charToEntityConvertor;

        private readonly GetEntityDelegate _standartCharEntiyCreateDelegate;

        public GameMapGeneratorByStrings(Dictionary<char, GetEntityDelegate> charToEntityConvertor, GetEntityDelegate standartCharDelegate)
        {
            _charToEntityConvertor = charToEntityConvertor.ToDictionary(keyValue => keyValue.Key, keyValue => keyValue.Value);

            _standartCharEntiyCreateDelegate = standartCharDelegate;
        }

        public Array2D<IUpdatableEntity> Generate(string[] input)
        {
            if (input.Length == 0)
                throw new ArgumentException();

            var inputVerticalLength = input[0].Length;

            if(input.Any(str => str.Length != inputVerticalLength))
                throw new ArgumentException();

            var result = new Array2D<IUpdatableEntity>(input.Length, input[0].Length);

            for(int x = 0; x < result.Widht; x++)
            {
                for(int y = 0; y < result.Height; y++)
                {
                    result[x, y] = SellectEntity(input[y][x]);
                }
            }

            return result;
        }

        public IUpdatableEntity SellectEntity(char key)
        {
            if (_charToEntityConvertor.ContainsKey(key))
                return _charToEntityConvertor[key]();

            return _standartCharEntiyCreateDelegate();
        }
    }
}

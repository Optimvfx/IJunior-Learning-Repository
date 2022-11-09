using BotFight.Game;
using BotFight.Map;
using BotFight.Map.Entity;
using BotFight.Map.Generator;
using System;
using System.Collections.Generic;

namespace BotFight
{
    class Program
    {
          private static readonly string[] _gameMap = new string[]
          {"0000000000000000000",
           "0000000000000000000",
           "0000000000000000000",
           "0000000000000000000",
           "0000000000000000000",
           "0000000000000000000",
           "0000000000000000000",
           "0000000000000000000"};

        static void Main(string[] args)
        {
            var inputToEntityConvertor = new Dictionary<char, GameMapGeneratorByStrings.GetEntityDelegate>();

            var mapGenerator = new GameMapGeneratorByStrings(inputToEntityConvertor, null);

            var game = new GamePlayer(mapGenerator.Generate(_gameMap), 15);

            game.Play();
        }
    }
}

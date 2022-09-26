using System;
using System.Collections.Generic;

namespace Extensions
{
    public static class CollectionExtention
    {
        private static Random _random;

        static CollectionExtention()
        {
            _random = new Random();
        }

        public static T GetRandomElement<T>(this List<T> collection)
        {
            var randomIndex = _random.Next(collection.Count);

            return collection[randomIndex];
        }
    }
}

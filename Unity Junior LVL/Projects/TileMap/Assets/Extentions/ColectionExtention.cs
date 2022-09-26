using System.Collections;
using System.Collections.Generic;
using System;

public static class ColectionExtention
{
    private static readonly Random _random;

    static ColectionExtention()
    {
        _random = new Random();
    }

    public static T GetRandomElement<T>(this IReadOnlyList<T> collection, Random random = null)
    {
        if (random == null)
            random = _random;

        if (collection.Count == 0)
            throw new IndexOutOfRangeException();

        var randomIndex = random.Next(collection.Count);

        return collection[randomIndex];
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Extensions;

public class SpawnerRandomPrefabSellector : SpawnerNextSpawnableSellector<Enemy, StandartSpawner.StandartSpawnerArguments>
{
    [SerializeField] private List<Enemy> _prefabs;

    public override Enemy GetNextSpawnablePrefab(StandartSpawner.StandartSpawnerArguments spawnArguments)
    {
        var notEmptyPrefabs = _prefabs.Where(spawnPoint => spawnPoint != null).ToList();

        if (notEmptyPrefabs.Count == 0)
            throw new NullReferenceException();

        return notEmptyPrefabs.GetRandomElement();
    }
}

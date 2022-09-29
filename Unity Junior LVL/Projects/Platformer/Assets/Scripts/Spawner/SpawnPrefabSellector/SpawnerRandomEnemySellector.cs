using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Extensions;

public class SpawnerRandomEnemySellector : SpawnerNextSpawnableSellector<StandartSpawner.StandartSpawnerArguments>
{
    [SerializeField] private List<Spawnable> _prefabs;

    public override Spawnable GetNextSpawnablePrefab(StandartSpawner.StandartSpawnerArguments spawnArguments)
    {
        var notEmptyPrefabs = _prefabs.Where(spawnPoint => spawnPoint != null).ToList();

        if (notEmptyPrefabs.Count == 0)
            throw new NullReferenceException();

        return notEmptyPrefabs.GetRandomElement();
    }
}

using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Extensions;

public class SpawnerRandomPointSellector : SpawnerSpawnPositionSellector<StandartSpawner.StandartSpawnerArguments>
{
    [SerializeField] private List<SpawnPoint> _spawnPoints;
    
    private SpawnPoint _previous;

    private void Reset()
    {
        _spawnPoints = transform.GetComponentsInChildren<SpawnPoint>().ToList();
    }

    public override bool TryGetNextSpawnPosition(StandartSpawner.StandartSpawnerArguments spawnArguments, out Vector3 position)
    {
        position = default(Vector3);

        var notEmptySpawnPoints = _spawnPoints.Where(spawnPoint => spawnPoint != null && spawnPoint != _previous).ToList();

        if (notEmptySpawnPoints.Count == 0)
            return false;

        _previous = notEmptySpawnPoints.GetRandomElement();

        position = _previous.Position;

        return true;
    }
}

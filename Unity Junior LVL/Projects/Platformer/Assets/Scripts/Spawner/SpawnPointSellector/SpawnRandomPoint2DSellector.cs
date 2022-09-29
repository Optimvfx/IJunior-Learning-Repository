using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Extensions;

public class SpawnRandomPoint2DSellector : SpawnerSpawnPositionSellector<StandartSpawner.StandartSpawnerArguments>
{
    [SerializeField] private List<SpawnPoint> _spawnPoints;

    private List<SpawnPoint> _spawned;

    private void Reset()
    {
        _spawnPoints = transform.GetComponentsInChildren<SpawnPoint>().ToList();
    }


    public override bool TryGetNextSpawnPosition(StandartSpawner.StandartSpawnerArguments spawnArguments, out Vector3 position)
    {
        position = default(Vector3);

        var notEmptySpawnPoints = _spawnPoints.Where(spawnPoint => spawnPoint != null).ToList();

        if (notEmptySpawnPoints.Count == 0)
            return false;

        var randomSpawnPoint = notEmptySpawnPoints.GetRandomElement();

        _spawned.Add(randomSpawnPoint);

        position = (Vector2)randomSpawnPoint.Position;

        return true;
    }
}

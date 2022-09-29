using System.Collections.Generic;
using UnityEngine;
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

    private void Awake()
    {
        _spawned = new List<SpawnPoint>();
    }

    public override bool TryGetNextSpawnPosition(StandartSpawner.StandartSpawnerArguments spawnArguments, out Vector3 position)
    {
        position = default(Vector3);

        var notEmptySpawnPoints = _spawnPoints.Where(spawnPoint => spawnPoint != null).Except(_spawned).ToList();

        if (notEmptySpawnPoints.Count == 0)
            return false;

        var randomSpawnPoint = notEmptySpawnPoints.GetRandomElement();

        _spawned.Add(randomSpawnPoint);

        position = (Vector2)randomSpawnPoint.Position;

        return true;
    }
}

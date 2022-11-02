using System;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _container;

    [SerializeField] private SpawnerNextSpawnableSellector _nextSpawnableSellector;
    [SerializeField] private SpawnerSpawnPositionSellector _spawnerSpawnPositionSellector;

    public void Init(SpawnerNextSpawnableSellector nextSpawnableSellector, SpawnerSpawnPositionSellector spawnerSpawnPositionSellector)
    {
        _nextSpawnableSellector = nextSpawnableSellector;
        _spawnerSpawnPositionSellector = spawnerSpawnPositionSellector;
    }

    public void Spawn()
    {
        if (_nextSpawnableSellector == null || _spawnerSpawnPositionSellector == null)
            throw new NullReferenceException();

        var prefab = _nextSpawnableSellector.GetNextSpawnablePrefab();

        var position = _spawnerSpawnPositionSellector.GetNextSpawnPosition();

        Instantiate(prefab, position, Quaternion.identity, _container);
    }
}

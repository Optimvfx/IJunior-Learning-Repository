using System;
using UnityEngine;

public abstract class Spawner<SpawnableType, Arguments> : MonoBehaviour
    where SpawnableType : Spawnable
    where Arguments : ISpawnArguments
{
    [SerializeField] private Transform _container;

    private SpawnerNextSpawnableSellector<SpawnableType, Arguments> _nextSpawnableSellector;
    private SpawnerSpawnPositionSellector<Arguments> _spawnerSpawnPositionSellector;

    public void Init(SpawnerNextSpawnableSellector<SpawnableType, Arguments> nextSpawnableSellector, SpawnerSpawnPositionSellector<Arguments> spawnerSpawnPositionSellector)
    {
        _nextSpawnableSellector = nextSpawnableSellector;
        _spawnerSpawnPositionSellector = spawnerSpawnPositionSellector;
    }

    public void Spawn()
    {
        if (_nextSpawnableSellector == null || _spawnerSpawnPositionSellector == null)
            throw new NullReferenceException();

        var nextArguments = GetNextSpawnArguments();

        var prefab = _nextSpawnableSellector.GetNextSpawnablePrefab(nextArguments);

        var position = _spawnerSpawnPositionSellector.GetNextSpawnPosition(nextArguments);

        Instantiate(prefab, position, Quaternion.identity, _container);
    }

    protected abstract Arguments GetNextSpawnArguments();
}

public interface ISpawnArguments
{

}

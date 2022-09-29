using System;
using UnityEngine;

public abstract class Spawner<Arguments> : MonoBehaviour
    where Arguments : ISpawnArguments
{
    [SerializeField] private Transform _container;

    private SpawnerNextSpawnableSellector<Arguments> _nextSpawnableSellector;
    private SpawnerSpawnPositionSellector<Arguments> _spawnerSpawnPositionSellector;

    public void Init(SpawnerNextSpawnableSellector<Arguments> nextSpawnableSellector, SpawnerSpawnPositionSellector<Arguments> spawnerSpawnPositionSellector)
    {
        _nextSpawnableSellector = nextSpawnableSellector;
        _spawnerSpawnPositionSellector = spawnerSpawnPositionSellector;
    }

    public bool TrySpawn()
    {
        if (_nextSpawnableSellector == null || _spawnerSpawnPositionSellector == null)
            throw new NullReferenceException();

        var nextArguments = GetNextSpawnArguments();

        var prefab = _nextSpawnableSellector.GetNextSpawnablePrefab(nextArguments);

        if(_spawnerSpawnPositionSellector.TryGetNextSpawnPosition(nextArguments, out Vector3 position) == false)
            return false;
            
        Instantiate(prefab, position, Quaternion.identity, _container);

        return true;
    }

    protected abstract Arguments GetNextSpawnArguments();
}

public interface ISpawnArguments
{

}

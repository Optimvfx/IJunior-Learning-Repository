using UnityEngine;

public class SpawnerCoinSellector : SpawnerNextSpawnableSellector<StandartSpawner.StandartSpawnerArguments>
{
    [SerializeField] private Coin _prefab;

    public override Spawnable GetNextSpawnablePrefab(StandartSpawner.StandartSpawnerArguments spawnArguments)
    {
        return _prefab;
    }
}

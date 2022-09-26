using UnityEngine;

[RequireComponent(typeof(SpawnerNextSpawnableSellector<Enemy, StandartSpawner.StandartSpawnerArguments>))]
[RequireComponent(typeof(SpawnerSpawnPositionSellector<StandartSpawner.StandartSpawnerArguments>))]
public class StandartSpawner : Spawner<Enemy, StandartSpawner.StandartSpawnerArguments>
{
    private void Awake()
    {
        var nextSpawnableSellector = GetComponent<SpawnerNextSpawnableSellector<Enemy, StandartSpawner.StandartSpawnerArguments>>();
        var spawnPositionSellector = GetComponent<SpawnerSpawnPositionSellector<StandartSpawner.StandartSpawnerArguments>>();

        Init(nextSpawnableSellector, spawnPositionSellector);
    }

    protected override StandartSpawnerArguments GetNextSpawnArguments()
    {
        return new StandartSpawnerArguments();
    }


    public class StandartSpawnerArguments : ISpawnArguments
    {

    }
}

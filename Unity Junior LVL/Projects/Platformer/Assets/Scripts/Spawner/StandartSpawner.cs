using UnityEngine;

[RequireComponent(typeof(SpawnerNextSpawnableSellector<StandartSpawner.StandartSpawnerArguments>))]
[RequireComponent(typeof(SpawnerSpawnPositionSellector<StandartSpawner.StandartSpawnerArguments>))]
public class StandartSpawner : Spawner<StandartSpawner.StandartSpawnerArguments>
{
    private void Awake()
    {
        var nextSpawnableSellector = GetComponent<SpawnerNextSpawnableSellector<StandartSpawner.StandartSpawnerArguments>>();
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

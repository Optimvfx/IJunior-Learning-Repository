using UnityEngine;

[RequireComponent(typeof(SpawnerNextSpawnableSellector))]
[RequireComponent(typeof(SpawnerSpawnPositionSellector))]
public class StandartSpawner : Spawner
{
    private void Awake()
    {
        var nextSpawnableSellector = GetComponent<SpawnerNextSpawnableSellector>();
        var spawnPositionSellector = GetComponent<SpawnerSpawnPositionSellector>();

        Init(nextSpawnableSellector, spawnPositionSellector);
    }
}

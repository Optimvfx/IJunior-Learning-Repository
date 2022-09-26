using UnityEngine;

public abstract class SpawnerNextSpawnableSellector<SpawnableType, Arguments> : MonoBehaviour
    where SpawnableType : Spawnable
    where Arguments : ISpawnArguments
{ 
    public abstract SpawnableType GetNextSpawnablePrefab(Arguments spawnArguments);
}

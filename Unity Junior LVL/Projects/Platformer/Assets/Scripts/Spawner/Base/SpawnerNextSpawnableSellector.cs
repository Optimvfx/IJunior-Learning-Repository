using UnityEngine;

public abstract class SpawnerNextSpawnableSellector<Arguments> : MonoBehaviour
    where Arguments : ISpawnArguments
{ 
    public abstract Spawnable GetNextSpawnablePrefab(Arguments spawnArguments);
}

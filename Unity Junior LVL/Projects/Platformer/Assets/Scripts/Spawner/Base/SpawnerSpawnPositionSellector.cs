using UnityEngine;

public abstract class SpawnerSpawnPositionSellector<Arguments> : MonoBehaviour
    where Arguments : ISpawnArguments
{
    public abstract bool TryGetNextSpawnPosition(Arguments spawnArguments, out Vector3 position);
}

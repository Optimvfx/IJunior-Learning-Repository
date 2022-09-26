using UnityEngine;

public abstract class SpawnerSpawnPositionSellector<Arguments> : MonoBehaviour
    where Arguments : ISpawnArguments
{
    public abstract Vector3 GetNextSpawnPosition(Arguments spawnArguments);
}

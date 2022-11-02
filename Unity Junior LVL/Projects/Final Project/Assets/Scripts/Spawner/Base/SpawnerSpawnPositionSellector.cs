using UnityEngine;

public abstract class SpawnerSpawnPositionSellector : MonoBehaviour
{
    public abstract Vector3 GetNextSpawnPosition();
}

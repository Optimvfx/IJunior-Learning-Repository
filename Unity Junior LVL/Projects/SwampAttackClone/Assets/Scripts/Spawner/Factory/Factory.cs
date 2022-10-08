using UnityEngine;

public abstract class Factory : MonoBehaviour
{
    public ReadOnlyEnemy Spawn(Vector3 spawnPoint, Transform container)
    {
        ReadOnlyEnemy enemy = GetEnemy();

        enemy.transform.position = spawnPoint;
        enemy.transform.parent = container;

        return enemy;
    }

    protected abstract ReadOnlyEnemy GetEnemy();
}

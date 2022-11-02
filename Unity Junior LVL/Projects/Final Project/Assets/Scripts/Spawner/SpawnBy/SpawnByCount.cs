using UnityEngine;

[RequireComponent(typeof(Spawner))]
public class SpawnByCount : MonoBehaviour
{
    [SerializeField] private uint _spawnCount;

    private Spawner _spawner;

    private void Start()
    {
        _spawner = GetComponent<Spawner>();

        for (int i = 0; i < _spawnCount; i++)
            _spawner.Spawn();
    }
}

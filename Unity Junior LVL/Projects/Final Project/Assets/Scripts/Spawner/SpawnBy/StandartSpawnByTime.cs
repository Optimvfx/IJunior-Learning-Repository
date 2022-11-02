using UnityEngine;

[RequireComponent(typeof(Spawner))]
public class StandartSpawnByTime : SpawnByTime
{
    [SerializeField] private float _timeBetweenSpawn;

    private void OnValidate()
    {
        _timeBetweenSpawn = Mathf.Max(_timeBetweenSpawn, 0);
    }

    public void Start()
    {
        Init(GetComponent<Spawner>());

        StartCoroutine(StartSpawning(_timeBetweenSpawn));
    }
}

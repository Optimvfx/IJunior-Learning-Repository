using UnityEngine;

[RequireComponent(typeof(StandartSpawner))]
public class StandartSpawnByTime : SpawnByTime<Enemy, StandartSpawner.StandartSpawnerArguments>
{
    [SerializeField] private float _timeBetweenSpawn;

    private void OnValidate()
    {
        _timeBetweenSpawn = Mathf.Max(_timeBetweenSpawn, 0);
    }

    public void Start()
    {
        Init(GetComponent<StandartSpawner>());

        StartCoroutine(StartSpawning(_timeBetweenSpawn));
    }
}

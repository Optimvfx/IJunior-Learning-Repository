using UnityEngine;
using System;
using System.Collections;

public class SpawnByTime<Arguments> : MonoBehaviour
    where Arguments : ISpawnArguments
{
    private Spawner<Arguments> _spawner;

    public void Init(Spawner<Arguments> spawner)
    {
        _spawner = spawner;
    }

    public IEnumerator StartSpawning(float secondsBreakBetweenSpawning)
    {
        if (_spawner == null)
            throw new NullReferenceException();

        if (secondsBreakBetweenSpawning <= 0)
            throw new ArgumentException();

        var waitBetweenSpawns = new WaitForSeconds(secondsBreakBetweenSpawning);

        while (true)
        {
            _spawner.TrySpawn();

            yield return waitBetweenSpawns;
        }
    }
}


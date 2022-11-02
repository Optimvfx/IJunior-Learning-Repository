using UnityEngine;
using System;
using System.Collections;

public class SpawnByTime : MonoBehaviour
{
    private Spawner _spawner;

    public void Init(Spawner spawner)
    {
        _spawner = spawner;
    }

    public IEnumerator StartSpawning(float secondsBreakBetweenSpawning)
    {
        if (_spawner == null)
            throw new NullReferenceException();

        if (secondsBreakBetweenSpawning <= 0)
            throw new ArgumentException();

        while(true)
        {
            _spawner.Spawn();

            yield return new WaitForSeconds(secondsBreakBetweenSpawning);
        }
    }
}


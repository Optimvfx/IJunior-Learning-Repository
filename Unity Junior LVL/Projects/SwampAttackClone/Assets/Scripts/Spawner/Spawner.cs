using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Spawner : MonoBehaviour
{
    [Header("Spawning")]
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _container;

    [Header("Extra")]
    [SerializeField] private PlayerInventory _inventory;

    private int _currentWaveNumber = 0;

    private Coroutine _spawnEnemysCorutione;

    public event UnityAction AllWavesEnded;
    public event UnityAction AllEnemySpawned;
    public event UnityAction<int,int> EnemyCountChanged;

    private void Start()
    {
        SetWave(_currentWaveNumber);
    }

    public void NextWave()
    {
        if(_currentWaveNumber + 1 >= _waves.Count)
        {
            AllWavesEnded?.Invoke();
            return;
        }

        SetWave(++_currentWaveNumber);
    }

    private void SetWave(int index)
    {
        if (index < 0 || index >= _waves.Count)
            throw new ArgumentException();

        EnemyCountChanged?.Invoke(0, 1);

        if (_spawnEnemysCorutione != null)
            StopCoroutine(_spawnEnemysCorutione);

        _spawnEnemysCorutione = StartCoroutine(SpawnEnemys(_waves[_currentWaveNumber]));
    }

    private IEnumerator SpawnEnemys(Wave wave)
    {
        var spawned = 0;

        var dellay = new WaitForSeconds(wave.Delay);

        while (wave.TryGetNextFactory(out Factory factory))
        {
            while(TimeExtenstions.IsTimeStoped())
            {
                yield return null;
            }

            var enemy = factory.Spawn(_spawnPoint.position, _container);

            enemy.Dying += OnEnemyDying;

            spawned++;
            EnemyCountChanged?.Invoke(spawned, wave.Count);

            yield return dellay;
        }

        AllEnemySpawned?.Invoke();
    }

    private void OnEnemyDying(ReadOnlyEnemy enemy)
    {
        enemy.Dying -= OnEnemyDying;

        _inventory.AddMoney(enemy.Reward);
    }
}

[System.Serializable]
public class Wave
{
    [SerializeField] private List<Factory> _enemyFactorys;
    [SerializeField] private UFloat _delay;
    [SerializeField] private uint _count;

    public float Delay => _delay;
    public int Count => (int)_count;

    public bool TryGetNextFactory(out Factory factory)
    {
        factory = default(Factory);

        if (_enemyFactorys.Count <= 0)
            return factory;

        factory = _enemyFactorys[0];
        _enemyFactorys.RemoveAt(0);

        return true;
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

public class RobberSpawner : MonoBehaviour
{
    [SerializeField] private Transform _container;

    [SerializeField] private Robber _robberPrefab;

    [SerializeField] private Transform[] _spawnPoints;

    private List<Robber> _spawnedRobbers;

    public IEnumerable<Robber> Robbers => _spawnedRobbers ?? new List<Robber>();

    private void Awake()
    {
        _spawnedRobbers = new List<Robber>();   

        SpawnRobbers();
    }

    private void SpawnRobbers()
    {
        if (_robberPrefab == null)
            throw new NullReferenceException();

        foreach (var spawnPoint in _spawnPoints)
        {
            var newRobber = SpawnRobber(spawnPoint.position);
            _spawnedRobbers.Add(newRobber);
        }
    }

    private Robber SpawnRobber(Vector2 spawnPoint)
    {
        return Instantiate(_robberPrefab, spawnPoint, Quaternion.identity, _container);
    }
}

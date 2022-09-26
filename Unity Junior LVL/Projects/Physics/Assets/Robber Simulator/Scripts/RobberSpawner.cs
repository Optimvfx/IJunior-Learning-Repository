using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RobberSpawner : MonoBehaviour
{
    [SerializeField] private Robber _spawnable;

    [SerializeField] private Transform _cotainer;

    [SerializeField] private List<Transform> _spawnPoints;

    private List<Robber> _spawnedRobbers;

    public IEnumerable<Robber> SpawnedRobbers => _spawnedRobbers;

    private void Start()
    {
       SpawnRobbers();
    }

    private void SpawnRobbers()
    {
        if (_spawnable == null)
            throw new NullReferenceException("No spawnable prefab!");

        _spawnedRobbers = new List<Robber>();

        foreach(var robberSpawnPoint in _spawnPoints)
        {
            var newRobber = SpawnRobber(robberSpawnPoint.position);

            _spawnedRobbers.Add(newRobber);
        }
    }

    private Robber SpawnRobber(Vector3 spawnPoint)
    {
        return Instantiate(_spawnable,  spawnPoint, Quaternion.identity, _cotainer);
    }
}

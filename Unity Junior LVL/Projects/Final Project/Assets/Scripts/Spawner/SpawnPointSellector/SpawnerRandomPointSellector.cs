using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnerRandomPointSellector : SpawnerSpawnPositionSellector
{
    [SerializeField] private Transform _center;
    [SerializeField] private UFloat _minDistance;
    [SerializeField] private UFloat _distanceRange;

    private List<Vector3> _allredySpawnedPoints;

    public override Vector3 GetNextSpawnPosition()
    {
        if (_center == null)
            throw new NullReferenceException();

        if (_allredySpawnedPoints == null)
            _allredySpawnedPoints = new List<Vector3>();

        var nextSpawnPoint = UnityEngine.Random.insideUnitCircle.normalized * (_minDistance + UnityEngine.Random.Range(-_distanceRange, _distanceRange));

        return transform.position + new Vector3(nextSpawnPoint.x, 0, nextSpawnPoint.y);
    }
}

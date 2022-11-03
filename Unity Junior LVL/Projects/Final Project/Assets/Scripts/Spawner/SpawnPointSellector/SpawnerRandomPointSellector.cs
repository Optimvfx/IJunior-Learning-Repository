using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnerRandomPointSellector : SpawnerSpawnPositionSellector
{
    [SerializeField] private Transform _center;
    [SerializeField] private UFloat _minDistance;
    [SerializeField] private UFloat _distanceRange;

    public override Vector3 GetNextSpawnPosition()
    {
        if (_center == null)
            throw new NullReferenceException();

        var nextSpawnPoint = UnityEngine.Random.insideUnitCircle.normalized * (_minDistance + UnityEngine.Random.Range(-_distanceRange, _distanceRange));

        return transform.position + new Vector3(nextSpawnPoint.x, 0, nextSpawnPoint.y);
    }
}

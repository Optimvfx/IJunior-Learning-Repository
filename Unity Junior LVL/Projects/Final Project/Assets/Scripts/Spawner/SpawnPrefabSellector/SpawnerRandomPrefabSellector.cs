using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Extensions;

public class SpawnerRandomPrefabSellector : SpawnerNextSpawnableSellector
{ 
    [SerializeField] private List<Decoration> _prefabs;

    public override GameObject GetNextSpawnablePrefab()
    {
        return _prefabs.GetRandomElement().gameObject;
    }
}

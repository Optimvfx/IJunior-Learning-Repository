using System;
using UnityEngine;

public class StandartEnemyFactory : Factory
{
    [SerializeField] private StandartEnemy _prefab;

    [SerializeField] private Player _target;

    protected override ReadOnlyEnemy GetEnemy()
    {
        if (_target == null || _prefab == null)
            throw new NullReferenceException();

        StandartEnemy enemy = Instantiate(_prefab);

        enemy.Init(new StandartEnemyInitArguments(_target));

        return enemy;
    }
}

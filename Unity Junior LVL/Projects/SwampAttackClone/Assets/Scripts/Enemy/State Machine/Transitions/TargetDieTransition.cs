using UnityEngine;

[RequireComponent(typeof(StandartEnemy))]
public class TargetDieTransition : Transition
{
    private StandartEnemy _standartEnemy;

    private void Awake()
    {
        _standartEnemy = GetComponent<StandartEnemy>();
    }

    public override bool NeedTransit => _standartEnemy.Target == null;
}
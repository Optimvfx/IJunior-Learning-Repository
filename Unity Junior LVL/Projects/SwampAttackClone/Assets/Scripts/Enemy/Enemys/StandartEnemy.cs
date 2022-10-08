using UnityEngine;

public class StandartEnemy : Enemy<StandartEnemyInitArguments>
{
    private Player _target;

    public Player Target => _target;

    public override void Init(StandartEnemyInitArguments initArguments)
    {
        _target = initArguments.Player;
    }
}

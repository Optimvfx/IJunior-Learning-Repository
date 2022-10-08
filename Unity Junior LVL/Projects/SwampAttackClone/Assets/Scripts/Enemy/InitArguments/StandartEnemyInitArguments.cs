using UnityEngine;
using System;

public class StandartEnemyInitArguments : EnemyInitArguments
{
    private Player _player;

    public Player Player => _player;

    public StandartEnemyInitArguments(Player player)
    {
        if (player == null)
            throw new ArgumentException();

        _player = player;
    }
}

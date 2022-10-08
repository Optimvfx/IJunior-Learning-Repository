public abstract class Enemy <InitArguments> : ReadOnlyEnemy
        where InitArguments : EnemyInitArguments
{
    public abstract void Init(InitArguments initArguments);
}

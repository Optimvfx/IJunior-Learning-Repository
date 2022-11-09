using System;

public class Weapon : Damager<Player>
{
    public int DamagePerShoot { get; private set; }
    public int BulletCount { get; private set; }

    public Weapon(int damage, int bulletCount)
    {
        if (damage < 0 || bulletCount < 0)
            throw new ArgumentException();

        DamagePerShoot = damage;
        BulletCount = bulletCount;
    }

    public void Shoot(Player player)
    {
        Damage(player, DamagePerShoot);
        BulletCount -= 1;
    }
}

public class Player : IDamagable
{
    public int Health { get; private set; }

    public bool IsDie => Health <= 0;

    public event Action OnDie;

    public Player(int health)
    {
        if (health < 0)
            throw new ArgumentException();

        Health = health;
    }

    public void TakeDamage(int damage)
    {
        if (IsDie)
            return;

        if (damage < 0)
            throw new ArgumentException();

        Health -= damage;

        if (IsDie)
            OnDie?.Invoke();
    }
}

public class Bot
{
    private Weapon _weapon;

    public Bot(Weapon weapon)
    {
        _weapon = new Weapon(weapon.DamagePerShoot, weapon.BulletCount);
    }

    public void OnSeePlayer(Player player)
    {
        _weapon.Shoot(player);
    }
}

public class Damager<Target>
    where Target : IDamagable
{
    protected void Damage(Target target, int damage)
    {
        if (damage < 0)
            throw new ArgumentException();

        target.TakeDamage(damage);
    }
}

public interface IDamagable
{
    void TakeDamage(int damage);
}
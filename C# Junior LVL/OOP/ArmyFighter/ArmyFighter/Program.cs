using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArmyFighter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var firstArmyWariors = new List<ArmyFightSimulator.Army.ClonableWarior>();

            firstArmyWariors.Add(new ArmyFightSimulator.Army.Infantryman(200, 15, 4));
            firstArmyWariors.Add(new ArmyFightSimulator.Army.Infantryman(200, 15, 6));
            firstArmyWariors.Add(new ArmyFightSimulator.Army.Pusher(400, 40, 20));

            var secondArmyWariors = new List<ArmyFightSimulator.Army.ClonableWarior>();

            secondArmyWariors.Add(new ArmyFightSimulator.Army.Pusher(400, 30, 20));
            secondArmyWariors.Add(new ArmyFightSimulator.Army.Pusher(500, 20, 30));
            secondArmyWariors.Add(new ArmyFightSimulator.Army.Pusher(600, 10, 40));
            secondArmyWariors.Add(new ArmyFightSimulator.Army.Pusher(200, 5, 10));
            secondArmyWariors.Add(new ArmyFightSimulator.Army.Infantryman(200, 15, 4));
            secondArmyWariors.Add(new ArmyFightSimulator.Army.Infantryman(200, 20, 4));

            ArmyFightSimulator armyFightSimulator = new ArmyFightSimulator(firstArmyWariors, secondArmyWariors);
            armyFightSimulator.StartFigth();
        }
    }

    public static class ListExtention
    {
        private static readonly Random _random;

        static ListExtention()
        {
            _random = new Random();
        }

        public static bool TrySellectRandom<T>(IReadOnlyList<T> array, out T randomElement)
        {
            randomElement = default(T);

            if (array == null || array.Count == 0)
                return false;

            var randomIndex = _random.Next(0, array.Count);

            randomElement = array[randomIndex];

            return true;
        }
    }

    public class ArmyFightSimulator
    {
        private readonly Army _firstArmy;
        private readonly Army _secondArmy;

        public ArmyFightSimulator(IEnumerable<Army.ClonableWarior> firstArmyWariors, IEnumerable<Army.ClonableWarior> secondArmyWariors)
        {
            _firstArmy = new Army(firstArmyWariors);
            _secondArmy = new Army(secondArmyWariors);
        }

        public void StartFigth()
        {
            int step = 1;

            while(_firstArmy.IsAnyWariorAlive && _secondArmy.IsAnyWariorAlive)
            {
                Console.WriteLine($"Step {step}:");
                SeeArmys();

                _firstArmy.TryDamage(_secondArmy);
                _secondArmy.TryDamage(_firstArmy);

                step++;

                Console.ReadKey(true);
            }

            if(_firstArmy.IsAnyWariorAlive == false && _secondArmy.IsAnyWariorAlive == false)
            {
                Console.WriteLine("No winer!");
            }    
            if(_secondArmy.IsAnyWariorAlive == false)
            {
                Console.WriteLine("First army win!");
            }
            else
            {
                Console.WriteLine("Second army win!");
            }

            Console.ReadKey(true);
        }

        private void SeeArmys()
        {
            Console.WriteLine("\nFirst army:" +
                $"\n{_firstArmy.GetInfo()}" +
                $"\n\nSecond army:" +
                $"\n{_secondArmy.GetInfo()}");
        }

        public class Army : Damager<Army.Warior>, IToInfoConvertable
        {
            private readonly IReadOnlyList<Warior> _wariors;

            public bool IsAnyWariorAlive => _wariors.Any(warior => warior.IsDead == false);

            public Army(IEnumerable<ClonableWarior> wariors)
            {
                _wariors = wariors.Select(warior => warior.Clone()).ToList();
            }

            public bool TryDamage(Army damagableArmy)
            {
                if (IsAnyWariorAlive == false)
                    return false;

                foreach (var warior in GetAlliweWariors())
                {
                    if (damagableArmy.TryGetAnyAlliweWarior(out Warior damagableWarior))
                    {
                        warior.GiveDamage(damagableWarior);
                    }
                }
                
                return false;
            }

            public string GetInfo()
            {
                StringBuilder stringBuilder = new StringBuilder();

                foreach (var warior in _wariors)
                {
                    stringBuilder.AppendLine(warior.GetInfo());
                }

                return stringBuilder.ToString();
            }

            private bool TryGetAnyAlliweWarior(out Warior anyWarior)
            {
                anyWarior = default(Warior);

                if (IsAnyWariorAlive == false)
                    return false;

                return ListExtention.TrySellectRandom<Warior>(_wariors.Where(warior => warior.IsDead == false).ToList(), out anyWarior);
            }

            private IEnumerable<Warior> GetAlliweWariors()
            {
                return _wariors.Where(warior => warior.IsDead == false);
            }

            public class Pusher : ClonableWarior
            {
                private readonly int _extraDamageForPassedShots;

                private int _shotCount;

                public Pusher(int health, int damageForce, int extraDamageForPassedShots) : base(health, damageForce)
                {
                    _extraDamageForPassedShots = Math.Max(extraDamageForPassedShots, 0);
                    _shotCount = 0;
                }

                public override ClonableWarior Clone()
                {
                    return new Pusher(Health, DamageForce, _extraDamageForPassedShots);
                }

                public override string GetInfo()
                {
                    return $"Pusher: {GetBaseInfo()}.";
                }

                protected override int ApplayDamageModifier(int damage)
                {
                    return damage;
                }

                protected override int ApplayTakeDamageModifier(int damage)
                {
                    var modifiedDamage = damage + _shotCount * _extraDamageForPassedShots;

                    _shotCount++;

                    return modifiedDamage;
                }

                protected override int GetShotCount()
                {
                    return DefaultShotCount;
                }
            }

            public class Infantryman : ClonableWarior
            {
                private readonly int _shotCount;

                public Infantryman(int health, int damageForce, int shotCount) : base(health, damageForce)
                {
                    _shotCount = Math.Max(shotCount, 0);
                }

                public override ClonableWarior Clone()
                {
                    return new Infantryman(Health, DamageForce, _shotCount);
                }

                public override string GetInfo()
                {
                    return $"Infantryman: {GetBaseInfo()} , Shot count {_shotCount}.";
                }

                protected override int ApplayDamageModifier(int damage)
                {
                    return damage;
                }

                protected override int ApplayTakeDamageModifier(int damage)
                {
                    return damage;
                }

                protected override int GetShotCount()
                {
                    return _shotCount;
                }
            }

            public abstract class ClonableWarior : Warior, ICloneable<ClonableWarior>
            {
                public ClonableWarior(int health, int damageForce) : base(health, damageForce)
                {
                }

                public abstract ClonableWarior Clone();
            }

            public abstract class Warior : Damager<Warior>, IDamagable, IToInfoConvertable
            {
                public static readonly int DefaultShotCount = 1; 
                   
                public readonly int DamageForce;
                
                public int Health { get; private set; }

                public bool IsDead => Health <= 0;

                public Warior(int health, int damageForce)
                {
                    Health = Math.Max(health, 0);

                    DamageForce = Math.Max(damageForce, 0);
                }

                public bool TryTakeDamage(int damage)
                {
                    if (IsDead)
                        return false;

                    var modifiedDamage = ApplayTakeDamageModifier(damage);

                    if (modifiedDamage <= 0)
                        return false;

                    Health -= modifiedDamage;

                    return true;
                }

                public void GiveDamage(Warior damagable)
                {
                    var modifiedDamage = ApplayDamageModifier(DamageForce);

                    for (int i = 0; i < GetShotCount(); i++)
                    {
                        if (TryDamage(damagable, modifiedDamage) == false)
                        {
                            return;
                        }
                    }
                }

                protected string GetBaseInfo()
                {
                    return $"MAXHP, {Health} HP, {DamageForce} DMG, Is dead {IsDead}";
                }

                public abstract string GetInfo();

                protected abstract int GetShotCount();

                protected abstract int ApplayDamageModifier(int damage);

                protected abstract int ApplayTakeDamageModifier(int damage);
            }
        }
    }

    public class Damager<Damagable>
        where Damagable : IDamagable
    {
        protected bool TryDamage(Damagable damagable, int damage)
        {
            if (damage <= 0)
                return false;

            return damagable.TryTakeDamage(damage);
        }
    }

    public interface IDamagable
    {
        bool TryTakeDamage(int damage);
    }

    public interface IToInfoConvertable
    {
        string GetInfo();
    }

    public interface ICloneable<T>
    {
        T Clone();
    }
}

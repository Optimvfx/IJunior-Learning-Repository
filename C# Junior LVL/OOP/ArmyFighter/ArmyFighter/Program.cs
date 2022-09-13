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

            firstArmyWariors.Add(new ArmyFightSimulator.Army.Infantryman(20, 15, 4));
            firstArmyWariors.Add(new ArmyFightSimulator.Army.Infantryman(200, 15, 6));
            firstArmyWariors.Add(new ArmyFightSimulator.Army.Pusher(400, 40, 20));

            var secondArmyWariors = new List<ArmyFightSimulator.Army.ClonableWarior>();

            secondArmyWariors.Add(new ArmyFightSimulator.Army.Pusher(400, 30, 20));
            secondArmyWariors.Add(new ArmyFightSimulator.Army.Pusher(500, 20, 30));
            secondArmyWariors.Add(new ArmyFightSimulator.Army.Pusher(600, 10, 40));
            secondArmyWariors.Add(new ArmyFightSimulator.Army.Pusher(200, 5, 10));
            secondArmyWariors.Add(new ArmyFightSimulator.Army.Infantryman(2000, 15, 4));
            secondArmyWariors.Add(new ArmyFightSimulator.Army.Infantryman(200, 20, 4));

            ArmyFightSimulator armyFightSimulator = new ArmyFightSimulator(firstArmyWariors, secondArmyWariors);
            armyFightSimulator.StartFigth();
        }
    }

    public class ListExtention
    {
        private readonly Random _random;

        public ListExtention()
        {
            _random = new Random();
        }

        public bool TrySellectRandom<T>(IReadOnlyList<T> array, out T randomElement)
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

                _firstArmy.TryDamageArmy(_secondArmy);
                _secondArmy.TryDamageArmy(_firstArmy);

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

        public class Army : DamagableArmy
        {
            public Army(IEnumerable<ClonableWarior> wariors) : base(wariors)
            {
            }

            public bool TryDamageArmy(Army damagableArmy)
            {
                if (IsAnyWariorAlive == false)
                    return false;

                foreach (var warior in GetAlliweWariors())
                {
                    if (warior.TryGiveDamage(this) == false)
                    {
                        return false;
                    }
                }

                return true;
            }

            public class Pusher : ClonableWarior
            {
                private readonly int _extraDamagePerPassedShot;

                private int _passedShotCount;

                public Pusher(int health, int damageForce, int extraDamagePerPassedShot) : base(health, damageForce)
                {
                    _extraDamagePerPassedShot = Math.Max(extraDamagePerPassedShot, 0);
                    _passedShotCount = 0;
                }

                public override ClonableWarior Clone()
                {
                    return new Pusher(Health, DamageForce, _extraDamagePerPassedShot);
                }

                public override string GetInfo()
                {
                    return $"Pusher: {GetBaseInfo()}, extra damage per passed shots {_extraDamagePerPassedShot}, passed shots {_passedShotCount}.";
                }

                protected override int ApplayDamageModifier(int damage)
                {
                    return damage;
                }

                protected override int ApplayTakeDamageModifier(int damage)
                {
                    var modifiedDamage = damage + _passedShotCount * _extraDamagePerPassedShot;

                    _passedShotCount++;

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

            public abstract class Warior : Damager<DamagableArmy>, IDamagable, IToInfoConvertable
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

                public bool TryGiveDamage(DamagableArmy damagable)
                {
                    if (IsDead)
                        return false;  

                    var modifiedDamage = ApplayDamageModifier(DamageForce);

                    for (int i = 0; i < GetShotCount(); i++)
                    {
                        if (TryDamage(damagable, modifiedDamage) == false)
                        {
                            return false;
                        }
                    }
                    
                    return true;
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

        public class DamagableArmy : IDamagable, IToInfoConvertable
        {
            private readonly IReadOnlyList<Army.Warior> _wariors;

            public bool IsAnyWariorAlive => _wariors.Any(warior => warior.IsDead == false);

            public DamagableArmy(IEnumerable<Army.ClonableWarior> wariors)
            {
                _wariors = wariors.Select(warior => warior.Clone()).ToList();
            }

            public bool TryTakeDamage(int damage)
            {
                if (TryGetAnyAlliweWarior(out Army.Warior damagableWarior))
                {
                    return damagableWarior.TryTakeDamage(damage);
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

            protected bool TryGetAnyAlliweWarior(out Army.Warior anyWarior)
            {
                anyWarior = default(Army.Warior);

                if (IsAnyWariorAlive == false)
                    return false;

                return new ListExtention().TrySellectRandom<Army.Warior>(_wariors.Where(warior => warior.IsDead == false).ToList(), out anyWarior);
            }

            protected IEnumerable<Army.Warior> GetAlliweWariors()
            {
                return _wariors.Where(warior => warior.IsDead == false);
            }
        }
    }

    public class Damager<Damagable>
       where Damagable : ArmyFighter.IDamagable
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

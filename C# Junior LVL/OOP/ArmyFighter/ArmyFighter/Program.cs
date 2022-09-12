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

        public class Army : Damager, IToInfoConvertable
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

                return ListExtention.TrySellectRandom<Warior>((IReadOnlyList<Warior>)_wariors.Select(warior => warior.IsDead == false).ToList(), out anyWarior);
            }

            private IEnumerable<Warior> GetAlliweWariors()
            {
                return _wariors.Where(warior => warior.IsDead == false);
            }

            public class Infantryman : ClonableWarior
            {
                public Infantryman(int health, int damageForce, int shotCount) : base(health, damageForce)
                {
                }

                public override ClonableWarior Clone()
                {
                    throw new NotImplementedException();
                }

                public override string GetInfo()
                {
                    throw new NotImplementedException();
                }

                protected override int ApplayDamageModifier(int damage)
                {
                    throw new NotImplementedException();
                }

                protected override int ApplayTakeDamageModifier(int damage)
                {
                    throw new NotImplementedException();
                }

                protected override int GetShotCount()
                {
                    throw new NotImplementedException();
                }
            }

            public abstract class ClonableWarior : Warior, ICloneable<ClonableWarior>
            {
                public ClonableWarior(int health, int damageForce) : base(health, damageForce)
                {
                }

                public abstract ClonableWarior Clone();
            }

            public abstract class Warior : Damager, Damager.IDamagable, IToInfoConvertable
            {
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

    public class Damager
    {
        protected bool TryDamage(IDamagable damagable, int damage)
        {
            if (damage <= 0)
                return false;

            return damagable.TryTakeDamage(damage);
        }

        public interface IDamagable
        {
            bool TryTakeDamage(int damage);
        }
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

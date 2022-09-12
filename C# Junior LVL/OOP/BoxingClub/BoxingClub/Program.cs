using System;
using System.Collections.Generic;
using System.Linq;

namespace BoxingClub
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var boxers = new List<BoxingTournament.ClonableBoxer>();

            boxers.Add(new BoxingTournament.DrugBoxer("Narkoman", 200, 10, 6, 20));
            boxers.Add(new BoxingTournament.VampairBoxer("Drakula", 400, 50, 30));
            boxers.Add(new BoxingTournament.PowerfullBoxer("Tike", 300, 40, 100));
            boxers.Add(new BoxingTournament.SpitefulBoxer("Monster", 200, 30, 2, 30));
            boxers.Add(new BoxingTournament.DodgyBoxer("Drih", 200, 20, new Procent(95)));

            var tournament = new BoxingTournament(boxers);
            tournament.Start();

            Console.ReadKey();
        }
    }

    public static class CreateRandom
    {
        private static readonly Random _random;

        static CreateRandom()
        {
            _random = new Random();
        }

        public static void Create(out Procent procent)
        {
            var randomProcent = _random.Next(0, (int)Procent.MaximalProcent);

            procent = new Procent(randomProcent);
        }
    }

    public class BoxingTournament
    {
        private readonly IReadOnlyList<ClonableBoxer> _boxers;

        public BoxingTournament(IEnumerable<ClonableBoxer> boxers)
        {
            _boxers = boxers.ToList();
        }

        public void Start()
        {
            ShowAllBoxers();

            Console.WriteLine("Enter first fighter index:");
            var firstBoxer = SellectBoxer();

            Console.WriteLine("Enter second fighter index:");
            var secondBoxer = SellectBoxer();

            StartFigth(firstBoxer, secondBoxer);
        }

        private void ShowAllBoxers()
        {
            Console.WriteLine("All boxers: ");

            for (int i = 0; i < _boxers.Count; i++)
            {
                Console.WriteLine($"{i} : {_boxers[i].GetInfo()}");
            }
        }

        private Boxer SellectBoxer()
        {
            int sellectedBoxerIndex;

            while (int.TryParse(Console.ReadLine(), out sellectedBoxerIndex) == false || InBoxersBounds(sellectedBoxerIndex) == false)
            {
                Console.WriteLine("Invalid input! Eneter boxer index:");
            }

            return _boxers[sellectedBoxerIndex].Clone();
        }

        private void StartFigth(Boxer first, Boxer second)
        {
            int step = 1;

            while (first.IsDead == false && second.IsDead == false)
            {
                Console.WriteLine($"\nStep {step}:");

                Console.WriteLine($"First: {first.GetInfo()}");
                Console.WriteLine($"Second: {second.GetInfo()}\n");

                var firstDamageSecondSeccess = first.TryDamage(second);
                Console.WriteLine($"First damage second is seccess is {firstDamageSecondSeccess}");

                var secondDamageSecondSeccess = second.TryDamage(first);
                Console.WriteLine($"First damage second is seccess is {secondDamageSecondSeccess}");

                Console.ReadKey(true);

                step++;
            }

            if (first.IsDead && second.IsDead)
            {
                Console.WriteLine("No one win!");
            }
            else if (second.IsDead)
            {
                Console.WriteLine("First win!");
            }
            else
            {
                Console.WriteLine("Second win!");
            }
        }

        private bool InBoxersBounds(int index)
        {
            return index >= 0 && index < _boxers.Count;
        }

        public class DrugBoxer : ClonableBoxer
        {
            private readonly int _extraDamagePerDrugEffectLevel;
            private int _drugEffectLevel;

            public DrugBoxer(string name, int health, int damage, int drugEffectLevel, int extraDamagePerDrugEffectLevel) : base(name, health, damage)
            {
                _extraDamagePerDrugEffectLevel = Math.Max(extraDamagePerDrugEffectLevel, 0);
                _drugEffectLevel = Math.Max(drugEffectLevel, 0);
            }

            public override Boxer Clone()
            {
                return new DrugBoxer(Name, Health, DamageForce, _drugEffectLevel, _extraDamagePerDrugEffectLevel);
            }

            public override string GetInfo()
            {
                return $"{GetBaseInfo()}, Drug effect level {_drugEffectLevel}.";
            }

            protected override int ApplayDamageModifier(int damage)
            {
                var drugEffectedDamage = damage + _extraDamagePerDrugEffectLevel * _drugEffectLevel;

                _drugEffectLevel--;
                _drugEffectLevel = Math.Max(_drugEffectLevel, 0);

                return drugEffectedDamage;
            }

            protected override int ApplayTakeDamageModifier(int damage)
            {
                return damage;
            }
        }

        public class VampairBoxer : ClonableBoxer
        {
            private readonly int _healPerHit;

            public VampairBoxer(string name, int health, int damage, int healPerHit) : base(name, health, damage)
            {
                _healPerHit = Math.Max(healPerHit, 0);
            }

            public override Boxer Clone()
            {
                return new VampairBoxer(Name, Health, DamageForce, _healPerHit);
            }

            public override string GetInfo()
            {
                return $"{GetBaseInfo()}, Heal per hit {_healPerHit}.";
            }

            protected override int ApplayDamageModifier(int damage)
            {
                Health += _healPerHit;
                return damage;
            }

            protected override int ApplayTakeDamageModifier(int damage)
            {
                return damage;
            }
        }

        public class SpitefulBoxer : ClonableBoxer
        {
            private readonly int _hitsForIncreasedDamage;
            private readonly int _increasedDamage;

            private int _givedHits;

            public SpitefulBoxer(string name, int health, int damage, int hitsForIncreasedDamage, int increasedDamage) : base(name, health, damage)
            {
                _hitsForIncreasedDamage = Math.Max(hitsForIncreasedDamage, 0);
                _increasedDamage = Math.Max(increasedDamage, 0);

                _givedHits = 0;
            }

            public override Boxer Clone()
            {
                return new SpitefulBoxer(Name, Health, DamageForce, _hitsForIncreasedDamage, _increasedDamage);
            }

            public override string GetInfo()
            {
                return $"{GetBaseInfo()}, Hit for increased damageForce {_hitsForIncreasedDamage}, increased damage force {_increasedDamage}.";
            }

            protected override int ApplayDamageModifier(int damage)
            {
                _givedHits++;

                if (_givedHits % _hitsForIncreasedDamage == 0)
                {
                    return damage + _increasedDamage;
                }
                else
                {
                    return damage;
                }
            }

            protected override int ApplayTakeDamageModifier(int damage)
            {
                return damage;
            }
        }

        public class PowerfullBoxer : ClonableBoxer
        {
            private readonly int _maximalExtraDamage;

            public PowerfullBoxer(string name, int Health, int damage, int maximalExtraDamage) : base(name, Health, damage)
            {
                _maximalExtraDamage = Math.Max(maximalExtraDamage, 0);
            }

            public override Boxer Clone()
            {
                return new PowerfullBoxer(Name, Health, DamageForce, _maximalExtraDamage);
            }

            public override string GetInfo()
            {
                return $"{GetBaseInfo()}, Maximal extra damageForce {_maximalExtraDamage}";
            }

            protected override int ApplayDamageModifier(int damage)
            {
                CreateRandom.Create(out Procent extraDamageProcent);
                return damage + (int)(extraDamageProcent.ToFloat() * _maximalExtraDamage);
            }

            protected override int ApplayTakeDamageModifier(int damage)
            {
                return damage;
            }
        }

        public class DodgyBoxer : ClonableBoxer
        {
            private static readonly int _damageWhenDodgeSuccess = 0;

            private readonly Procent _damageDodgeChange;

            public DodgyBoxer(string name, int Health, int damage, Procent damageDodgeChange) : base(name, Health, damage)
            {
                _damageDodgeChange = damageDodgeChange;
            }

            public override Boxer Clone()
            {
                return new DodgyBoxer(Name, Health, DamageForce, _damageDodgeChange);
            }

            public override string GetInfo()
            {
                return $"{GetBaseInfo()}, DamageForce reduction change {_damageDodgeChange}";
            }

            protected override int ApplayDamageModifier(int damage)
            {
                return damage;
            }

            protected override int ApplayTakeDamageModifier(int damage)
            {
                if (_damageDodgeChange.TryWithChance(new Random()))
                    return _damageWhenDodgeSuccess;

                return damage;
            }
        }

        public abstract class ClonableBoxer : Boxer, ICloneable<Boxer>
        {
            protected ClonableBoxer(string name, int maximalHealth, int damage) : base(name, maximalHealth, damage)
            {
            }

            public abstract Boxer Clone();
        }

        public abstract class Boxer : Damager<Boxer>, IDamagable, IToInfoConvertable
        {
            public readonly string Name;

            public readonly int DamageForce;

            public int Health { get; protected set; }

            public bool IsDead => Health <= 0;

            public Boxer(string name, int health, int damageForce)
            {
                Name = name;

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

            public bool TryDamage(Boxer damagable)
            {
                var modifiedDamage = ApplayDamageModifier(DamageForce);

                return TryDamage(damagable, modifiedDamage);
            }

            protected string GetBaseInfo()
            {
                return $"{Name}: {Health} HP, {DamageForce} DMG, Is dead {IsDead}";
            }

            public abstract string GetInfo();

            protected abstract int ApplayDamageModifier(int damage);

            protected abstract int ApplayTakeDamageModifier(int damage);
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

    public struct Procent : IToInfoConvertable
    {
        public readonly static float MaximalProcent = 100;

        public readonly float Value;

        public Procent(int procent)
        {
            Value = Math.Max(Math.Min(procent, MaximalProcent), 0);
        }

        public string GetInfo()
        {
            return $"Procent: {Value}";
        }

        public bool TryWithChance(Random random)
        {
            var randomProcent = random.NextDouble() * MaximalProcent;

            return Value >= randomProcent;
        }

        public float ToFloat()
        {
            return Value / MaximalProcent;
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

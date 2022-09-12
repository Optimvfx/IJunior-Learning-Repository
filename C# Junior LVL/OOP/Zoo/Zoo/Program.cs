using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zoo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var valers = new List<Zoo.Valer>();

            var valerArimals = new List<Zoo.Valer.Animal>();
            valerArimals.Add(new Zoo.Valer.Animal("Monky","Abu","Aaaaa!!!",Zoo.Valer.Animal.AnimalGender.Male));
            valerArimals.Add(new Zoo.Valer.Animal("Monky", "Kuri", "Aaaaa!!!", Zoo.Valer.Animal.AnimalGender.Female));
            valerArimals.Add(new Zoo.Valer.Animal("Monky", "Su", "Aaaaa!!!", Zoo.Valer.Animal.AnimalGender.Female));
            valers.Add(new Zoo.Valer("Monkys", valerArimals));

            valerArimals = new List<Zoo.Valer.Animal>();
            valerArimals.Add(new Zoo.Valer.Animal("Flamingo", "Abu", "Iaiaiaaiaiai!!!", Zoo.Valer.Animal.AnimalGender.Male));
            valerArimals.Add(new Zoo.Valer.Animal("Crab", "Kapaui", "Chrcrhchr!!!", Zoo.Valer.Animal.AnimalGender.Female));
            valerArimals.Add(new Zoo.Valer.Animal("Crab", "Kapa", "Chrcrhchr!!!", Zoo.Valer.Animal.AnimalGender.Male));
            valerArimals.Add(new Zoo.Valer.Animal("Semga", "Praki", "Bulbulbul!!!", Zoo.Valer.Animal.AnimalGender.Female));
            valers.Add(new Zoo.Valer("River", valerArimals));

            valerArimals = new List<Zoo.Valer.Animal>();
            valerArimals.Add(new Zoo.Valer.Animal("Lion", "Adam", "Rrrrrr!!!" , Zoo.Valer.Animal.AnimalGender.Male));
            valerArimals.Add(new Zoo.Valer.Animal("Lion", "Kari", "Rrrrrr!!!", Zoo.Valer.Animal.AnimalGender.Female));
            valerArimals.Add(new Zoo.Valer.Animal("Lion", "Kuvi", "Rrrrrr!!!", Zoo.Valer.Animal.AnimalGender.Female));
            valerArimals.Add(new Zoo.Valer.Animal("Lion", "Chuka", "Rrrrrr!!!", Zoo.Valer.Animal.AnimalGender.Female));
            valers.Add(new Zoo.Valer("Lions", valerArimals));

            valerArimals = new List<Zoo.Valer.Animal>();
            valerArimals.Add(new Zoo.Valer.Animal("Tirex", "Scar", "Arrr!!!", Zoo.Valer.Animal.AnimalGender.Male));

            valers.Add(new Zoo.Valer("Juristic", valerArimals));

            Zoo zoo = new Zoo(valers);
            zoo.Enter();
        }
    }

    public class Zoo
    {
        private readonly IReadOnlyList<Valer> _valers;

        public Zoo(IEnumerable<Valer> valers)
        {
            _valers = valers.Select(valer => valer.Clone()).ToList();
        }

        public void Enter()
        {
            if (_valers.Count == 0)
            {
                Console.WriteLine("No valers");
                return;
            }

            Console.WriteLine($"Valers count {_valers.Count}");
            Console.WriteLine("Sellect waler where to go:");
            Console.WriteLine($"Posible valere index from {0} to {_valers.Count - 1}");

            ShowValiersNames();

            while(int.TryParse(Console.ReadLine(), out int valerIndex) == false || TryShowValer(valerIndex) == false)
            {
                Console.WriteLine($"Invalid input, posible valere index from {0} to {_valers.Count - 1}");
            }

            Console.ReadKey();
        }
       
        public bool TryShowValer(int index)
        {
            if(InBounds(index) == false)
            {
                return false;
            }

            Console.WriteLine($"\n{_valers[index].GetInfo()}\n");

            return true;
        }

        private void ShowValiersNames()
        {
           for(int i = 0; i < _valers.Count; i++)
            {
                Console.WriteLine($"{i}: {_valers[i].Name}");
            }
        }

        private bool InBounds(int index)
        {
            return index >= 0 && index < _valers.Count;
        }

        public class Valer : ICloneable<Valer>, IToInfoConvertable
        {
            public readonly string Name;
            private readonly IReadOnlyList<Animal> _animals;

            public Valer(string name, IEnumerable<Animal> animals)
            {
                Name = name;

                _animals = animals.ToList();
            }

            public Valer Clone()
            {
                return new Valer(Name, _animals);
            }

            public string GetInfo()
            {
                StringBuilder stringBuilder = new StringBuilder();

                stringBuilder.AppendLine($"Valer {Name}:");
                stringBuilder.AppendLine($"Animal count: {_animals.Count}");
                stringBuilder.AppendLine($"Animals:");

                foreach(var animal in _animals)
                {
                    stringBuilder.AppendLine(animal.GetInfo());
                }

                return stringBuilder.ToString();
            }

            public struct Animal : IToInfoConvertable
            {
                private readonly string Kind;
                private readonly string Name;

                private readonly string Sound;

                private readonly AnimalGender Gender;

                public Animal(string kind, string name, string sound, AnimalGender gender)
                {
                    Kind = kind;
                    Name = name;

                    Sound = sound;

                    Gender = gender;
                }

                public string GetInfo()
                {
                    return $"Kind {Kind}, Name {Name}, Sound {Sound}, Gender {Gender}.";
                }

                public enum AnimalGender
                {
                    Male,
                    Female
                }
            }
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

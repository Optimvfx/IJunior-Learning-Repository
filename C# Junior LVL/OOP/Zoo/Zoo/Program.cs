using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int maximalFishCapacity = 5;

            AquariumTerminal aquariumTerminal = new AquariumTerminal(maximalFishCapacity);
            aquariumTerminal.Activate();
        }
    }

    public class CreatorByUserInput
    {
        public bool TryCreate(out AquariumTerminal.Aquarium.Fish fish)
        {
            fish = default(AquariumTerminal.Aquarium.Fish);

            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Life expectancy in years: ");

            if (int.TryParse(Console.ReadLine(), out int lifeExpectancyInYears))
            {
                fish = new AquariumTerminal.Aquarium.Fish(name, lifeExpectancyInYears);

                return true;
            }

            return false;
        }
    }

    public class AquariumTerminal
    {
        private readonly Aquarium _aquarium;

        public AquariumTerminal(int maximalFishCapacity)
        {
            _aquarium = new Aquarium(Math.Max(maximalFishCapacity, 0));
        }

        public void Activate()
        {
            const string AddFishCommand = "ADD";
            const string RemoveFishCommand = "REMOVE";
            const string SeeFishesCommand = "SEE";
            const string ExitCommand = "EXIT";

            bool isOpen = true;

            while (isOpen)
            {
                Console.WriteLine($"\nPosible coommands:" +
                    $"\n{AddFishCommand}" +
                    $"\n{RemoveFishCommand}" +
                    $"\n{SeeFishesCommand}" +
                    $"\n{ExitCommand}\n");

                var userInput = Console.ReadLine().ToUpper();

                switch (userInput)
                {
                    case AddFishCommand:
                        AddFish();
                        break;
                    case RemoveFishCommand:
                        RemoveFish();
                        break;
                    case SeeFishesCommand:
                        ShowAllFishes();
                        break;
                    case ExitCommand:
                        isOpen = false;
                        break;
                    default:
                        Console.WriteLine("Uncnovn command, you skip time!");
                        SkipTime();
                        break;
                }
            }
        }

        private void ShowAllFishes()
        {
            Console.WriteLine(_aquarium.GetInfo());
        }

        private void AddFish()
        {
            Aquarium.Fish fish;

            Console.WriteLine("Creat fish:");

            CreatorByUserInput createByUserInput = new CreatorByUserInput();

            while (createByUserInput.TryCreate(out fish) == false)
            {
                Console.WriteLine("Create fish unseccess, re create:");
            }

            if (_aquarium.TryAddFish(fish))
            {
                Console.WriteLine("Add fish is seccess.");
            }
            else
            {
                Console.WriteLine("Add fish is unseccess!");
            }
        }

        private void RemoveFish()
        {
            int removeIndex;

            Console.Write("Remove index:");

            while (int.TryParse(Console.ReadLine(), out removeIndex) == false)
            {
                Console.Write("Invalid index, remove index:");
            }

            if (_aquarium.TryRemoveFish(removeIndex))
            {
                Console.WriteLine("Remove fish is seccess.");
            }
            else
            {
                Console.WriteLine("Remove fish is unseccess!");
            }
        }

        private void SkipTime()
        {
            _aquarium.SkipTime();
        }

        public class Aquarium : IToInfoConvertable
        {
            private readonly int _maximalFishCapacity;

            private readonly List<Fish> _fishes;

            public Aquarium(int maximalFishCapacity)
            {
                _maximalFishCapacity = Math.Max(maximalFishCapacity, 0);

                _fishes = new List<Fish>();
            }

            public void SkipTime()
            {
                foreach (var fish in _fishes)
                {
                    fish.GrowOld();
                }
            }

            public bool TryAddFish(Fish fish)
            {
                if (_fishes.Count >= _maximalFishCapacity)
                    return false;

                _fishes.Add(fish);

                return true;
            }

            public bool TryRemoveFish(int index)
            {
                if (IsInBounds(index) == false)
                    return false;

                _fishes.RemoveAt(index);

                return true;
            }

            public string GetInfo()
            {
                StringBuilder stringBuilder = new StringBuilder();

                stringBuilder.AppendLine($"Maximal fish capacity: {_maximalFishCapacity}");
                stringBuilder.AppendLine($"Fishes in aquarium count: {_fishes.Count}");
                stringBuilder.AppendLine("Fishes in aquarium:");

                int numberOfFish = 0;

                foreach (Fish fish in _fishes)
                {
                    stringBuilder.AppendLine($"{numberOfFish}: {fish.GetInfo()}");

                    numberOfFish++;
                }

                return stringBuilder.ToString();
            }

            private bool IsInBounds(int index)
            {
                return index >= 0 && index < _fishes.Count;
            }

            public class Fish : IToInfoConvertable, ICloneable<Fish>
            {
                public readonly string Name;

                public readonly int LifeExpectancyInYears;

                public int AgeInYears { get; private set; }

                public bool IsDieByGrowOld => AgeInYears >= LifeExpectancyInYears;

                public Fish(string name, int lifeExpectancyInYear)
                {
                    Name = name;

                    LifeExpectancyInYears = Math.Max(lifeExpectancyInYear, 0);
                    AgeInYears = 0;
                }

                public void GrowOld()
                {
                    AgeInYears++;
                }

                public Fish Clone()
                {
                    return new Fish(Name, LifeExpectancyInYears);
                }

                public string GetInfo()
                {
                    return $"Name: {Name}, Life expectancy in years {LifeExpectancyInYears}, Age {AgeInYears}, Is dead {IsDieByGrowOld}.";
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

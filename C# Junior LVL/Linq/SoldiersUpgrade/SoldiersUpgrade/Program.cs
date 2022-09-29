using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoldiersUpgrade
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var searchingFirstNameChar = 'b';

            var firstSquadSoldiers = new List<Soldier>();

            firstSquadSoldiers.Add(new Soldier("Alex", "AK47", "Regular", 20));
            firstSquadSoldiers.Add(new Soldier("Bakar", "AK47", "Field Marshal", 20));
            firstSquadSoldiers.Add(new Soldier("Anvar", "MK 26.5 (21.5)", "General", 20));
            firstSquadSoldiers.Add(new Soldier("Oleg", "MK 26.5 (21.5)", "General", 20));
            firstSquadSoldiers.Add(new Soldier("Bin", "GLOK", "General", 20));
            firstSquadSoldiers.Add(new Soldier("Boku", "MK26", "Regular", 20));
            firstSquadSoldiers.Add(new Soldier("Bin", "GLOK", "General", 20));
            firstSquadSoldiers.Add(new Soldier("Boku", "MK26", "Regular", 20));

            var secondSquadSoldiers = new List<Soldier>();

            secondSquadSoldiers.Add(new Soldier("Alex", "AK47", "Regular", 20));
            secondSquadSoldiers.Add(new Soldier("Pasha", "AK47", "Field Marshal", 20));
            secondSquadSoldiers.Add(new Soldier("Oleg", "MK 26.5 (21.5)", "General", 20));
            secondSquadSoldiers.Add(new Soldier("Roma", "GLOK", "General", 20));
            secondSquadSoldiers.Add(new Soldier("Stan", "MK26", "Regular", 20));

            var firstSquad = new Squad(firstSquadSoldiers);
            var secondSquad = new Squad(secondSquadSoldiers);

            secondSquad.AddSoldiers(firstSquad.TakeSoldiersByFirstNameChar(searchingFirstNameChar));

            Console.WriteLine("First squad:");

            foreach(var soldier in firstSquad.GetAllSoldiers())
            {
                Console.WriteLine(soldier.GetInfo());
            }

            Console.WriteLine("Second squad:");

            foreach (var soldier in secondSquad.GetAllSoldiers())
            {
                Console.WriteLine(soldier.GetInfo());
            }

            Console.ReadKey();
        }
    }

    public class Squad
    {
        private IEnumerable<Soldier> _soldiers;

        public Squad(IEnumerable<Soldier> soldiers)
        {
            _soldiers = soldiers.ToList();
        }
        
        public IEnumerable<Soldier> GetAllSoldiers()
        {
            return _soldiers;
        }

        public IEnumerable<Soldier> TakeSoldiersByFirstNameChar(char firstNameChar)
        {
            var suitableSoldiers = _soldiers.Where(soldier => char.ToUpper(soldier.Name.FirstOrDefault()) == char.ToUpper(firstNameChar));

            _soldiers = _soldiers.Except(suitableSoldiers);

            return suitableSoldiers;
        }

        public void AddSoldiers(IEnumerable<Soldier> soldiers)
        {
            _soldiers = _soldiers.Concat(soldiers);
        }
    }

    public struct Soldier
    {
        public readonly string Name;

        public readonly string Weapon;

        public readonly string Rank;

        public readonly int ServiceLifeInMonth;

        public Soldier(string name, string weapon, string rank, int serviceLifeInMounth)
        {
            Name = name;
            Weapon = weapon;
            Rank = rank;

            ServiceLifeInMonth = Math.Max(serviceLifeInMounth, 0);
        }

        public string GetInfo()
        {
            return $"Name {Name} , Weapon {Weapon} , Rank {Rank} , Servic life {ServiceLifeInMonth}";
        }
    }
}

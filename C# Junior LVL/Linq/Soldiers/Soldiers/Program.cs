using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soldiers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var soldiers = new List<Soldier>();

            soldiers.Add(new Soldier("Alex", "AK47", "Regular", 20));
            soldiers.Add(new Soldier("Pasha", "AK47", "Field Marshal", 20));
            soldiers.Add(new Soldier("Oleg", "MK 26.5 (21.5)", "General", 20));
            soldiers.Add(new Soldier("Roma", "GLOK", "General", 20));
            soldiers.Add(new Soldier("Stan", "MK26", "Regular", 20));

            var soldierDatabase = new SoldierDatabase(soldiers);

            foreach(var pasport in soldierDatabase.GetSoldiersPasports())
            {
                Console.WriteLine(pasport.GetInfo());
            }

            Console.ReadKey();
        }
    }

    public class SoldierDatabase
    {
        private readonly IEnumerable<Soldier> _soldiers;

        public SoldierDatabase(IEnumerable<Soldier> soldiers)
        {
            _soldiers = soldiers;
        }

        public IEnumerable<Pasport> GetSoldiersPasports()
        {
            return _soldiers.Select(soldier => new Pasport(soldier.Name, soldier.Rank));
        }
    }

    public struct Pasport
    {
        readonly public string Name;
        readonly public string Rang;

        public Pasport(string name, string rang)
        {
            Name = name;
            Rang = rang;
        }

        public string GetInfo()
        {
            return $"{Name}, Rang {Rang}.";
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
    }
}

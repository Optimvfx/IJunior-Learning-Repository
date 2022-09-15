using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Amnesty
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var prisoners = new List<CriminalPerson>();

            prisoners.Add(new CriminalPerson(new FullName("Joo","Weeal","Lee"), CriminalPerson.CriminalWrongdoing.Marauding));
            prisoners.Add(new CriminalPerson(new FullName("Hoo", "Weeal", "Lee"), CriminalPerson.CriminalWrongdoing.Antigovernment));
            prisoners.Add(new CriminalPerson(new FullName("Paul", "Karr", "Lee"), CriminalPerson.CriminalWrongdoing.Marauding));
            prisoners.Add(new CriminalPerson(new FullName("Lopa", "Weeal", "Lee"), CriminalPerson.CriminalWrongdoing.Marauding));
            prisoners.Add(new CriminalPerson(new FullName("Kowar", "Karr", "Lee"), CriminalPerson.CriminalWrongdoing.Antigovernment));

            var prison = new Prison(prisoners);

            Console.WriteLine(prison.GetInfo());

            prison.Amnesty(CriminalPerson.CriminalWrongdoing.Antigovernment);

            Console.WriteLine(prison.GetInfo());

            Console.ReadKey(true);
        }
    }

    public class Prison
    {
        private IEnumerable<CriminalPerson> _prisoners;

        public Prison(IEnumerable<CriminalPerson> prisoners)
        {
            _prisoners = prisoners;
        }

        public void Amnesty(CriminalPerson.CriminalWrongdoing amnestyedWongdoing)
        {
            _prisoners = _prisoners.Where(prisoner => prisoner.Wrongdoing != amnestyedWongdoing);
        }

        public string GetInfo()
        {
            var stringBuilder = new StringBuilder();

            foreach(var prisoner in _prisoners)
            {
                stringBuilder.AppendLine(prisoner.GetInfo());
            }

            return stringBuilder.ToString();
        }
    }

    public struct CriminalPerson
    {
        public readonly FullName FullName;

        public readonly CriminalWrongdoing Wrongdoing;

        public CriminalPerson(FullName fullName, CriminalWrongdoing wrongdoing)
        {
            FullName = fullName;
            Wrongdoing = wrongdoing;
        }

        public string GetInfo()
        {
            return $"{FullName.GetInfo()} , Wrongdoing {Wrongdoing}.";
        }

        public enum CriminalWrongdoing
        {
            Marauding,
            Antigovernment
        }
    }

    public struct FullName
    {
        public readonly string Name;
        public readonly string Surname;
        public readonly string Patronymic;

        public FullName(string name, string surname, string patronymic)
        {
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
        }

        public string GetInfo()
        {
            return $"{Name} {Surname} {Patronymic}";
        }
    }
}

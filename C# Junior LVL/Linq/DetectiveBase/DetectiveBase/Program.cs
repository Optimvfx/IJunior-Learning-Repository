using System;
using System.Collections.Generic;
using System.Linq;

namespace DetectiveBase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var criminalPersons = new List<CriminalPerson>();

            criminalPersons.Add(new CriminalPerson(new FullName("Joo","Jower","Joler"), false, 200, 100, CriminalPerson.PersonNationality.Neger));
            criminalPersons.Add(new CriminalPerson(new FullName("Han", "Pat", "Cur"), false, 100, 50, CriminalPerson.PersonNationality.Neger));
            criminalPersons.Add(new CriminalPerson(new FullName("Han", "Pawar", "Curati"), false, 100, 50, CriminalPerson.PersonNationality.Australian));
            criminalPersons.Add(new CriminalPerson(new FullName("Hoo", "Wer", "Leur"), true, 200, 100, CriminalPerson.PersonNationality.Neger));
            criminalPersons.Add(new CriminalPerson(new FullName("Han", "Paku", "Deck"), true, 100, 50, CriminalPerson.PersonNationality.Neger));
            criminalPersons.Add(new CriminalPerson(new FullName("Dawe", "Danur", "Cuhii"), false, 100, 50, CriminalPerson.PersonNationality.Australian));

            var detectiveBaseTerminal = new DetectiveBaseTerminal(criminalPersons);
            detectiveBaseTerminal.Activate();

            Console.ReadKey(true);
        }
    }

    public class SellectByUserInput
    {
        public bool TrySellectPersonNationality(out CriminalPerson.PersonNationality sellectedPersonNationality)
        {
            sellectedPersonNationality = default(CriminalPerson.PersonNationality);

            Console.WriteLine("Posible nationalitys:");

            foreach (var personNationality in CriminalPerson.GetAllPersonNationalitys())
            {
                Console.WriteLine($"{(int)personNationality} : {personNationality}");
            }

            Console.WriteLine("Enter person nationality index:");

            if (int.TryParse(Console.ReadLine(), out int personNationalityIndex) &&
                  personNationalityIndex <= CriminalPerson.GetAllPersonNationalitys().Max(personNationality => (int)personNationality) &&
                  personNationalityIndex >= CriminalPerson.GetAllPersonNationalitys().Min(personNationality => (int)personNationality))
            {
                sellectedPersonNationality = (CriminalPerson.PersonNationality)personNationalityIndex;

                return true;
            }

            return false;
        }
    }

    public class DetectiveBaseTerminal
    {
        private readonly DetectiveBase _detectiveBase;

        public DetectiveBaseTerminal(IEnumerable<CriminalPerson> criminalPersons)
        {
            _detectiveBase = new DetectiveBase(criminalPersons);
        }

        public void Activate()
        {
            const string SearchCommand = "SEARCH";
            const string ExitCommand = "EXIT";

            bool isOpen = true;

            while (isOpen)
            {
                Console.WriteLine($"\nCommands:" +
                    $"\n{SearchCommand}" +
                    $"\n{ExitCommand}");

                var userCommand = Console.ReadLine().ToUpper();

                switch (userCommand)
                {
                    case SearchCommand:
                        Search();
                        break;
                    case ExitCommand:
                        isOpen = false;
                        break;
                    default:
                        Console.WriteLine("Invalid command!");
                        break;
                }
            }
        }

        private void Search()
        {
            Console.Write("Searching hight in centimeters: ");

            if (int.TryParse(Console.ReadLine(), out int searchingHightInCentimeters) && searchingHightInCentimeters >= 0)
            {
                Console.Write("Searching weight in kilograms: ");

                if (int.TryParse(Console.ReadLine(), out int searchingWeightInKilograms) && searchingWeightInKilograms >= 0)
                {
                    Console.WriteLine("Searching nationality: ");

                    if (new SellectByUserInput().TrySellectPersonNationality(out var searchingNationality))
                    {
                        var matchingToDescriptionCriminalPersons = _detectiveBase.SearchFree(searchingHightInCentimeters, searchingWeightInKilograms, searchingNationality);

                        ShowCriminalPersons(matchingToDescriptionCriminalPersons);
                        return;
                    }
                }
            }

            Console.WriteLine("Search un seccess!");
        }

        private void ShowCriminalPersons(IEnumerable<CriminalPerson> criminalPersons)
        {
            foreach (var criminalPerson in criminalPersons)
            {
                Console.WriteLine(criminalPerson.GetInfo());
            }
        }
    }

    public class DetectiveBase
    {
        private readonly IReadOnlyList<CriminalPerson> _criminalPersons;

        public DetectiveBase(IEnumerable<CriminalPerson> criminalPersons)
        {
            _criminalPersons = criminalPersons.ToList();
        }

        public IEnumerable<CriminalPerson> SearchFree(int searchingHightInCentimeters, int searchingWeightInKilograms, CriminalPerson.PersonNationality searchingNationality)
        {
            return _criminalPersons.Where(criminalPerson => criminalPerson.HeightInCentimeters == searchingHightInCentimeters)
                                   .Where(criminalPerson => criminalPerson.WeightInKilograms == searchingWeightInKilograms)
                                   .Where(criminalPerson => criminalPerson.Nationality == searchingNationality)
                                   .Where(CriminalPerson => CriminalPerson.IsInPrison == false);
        }
    }

    public struct CriminalPerson
    {
        public readonly FullName FullName;

        public readonly bool IsInPrison;

        public readonly int HeightInCentimeters;
        public readonly int WeightInKilograms;

        public readonly PersonNationality Nationality;

        public CriminalPerson(FullName fullName, bool isInPrison, int heightInCentimeters, int weightInKilograms, PersonNationality nationality)
        {
            FullName = fullName;
            IsInPrison = isInPrison;
            HeightInCentimeters = Math.Max(heightInCentimeters, 0);
            WeightInKilograms = Math.Max(weightInKilograms, 0);
            Nationality = nationality;
        }

        public static IEnumerable<PersonNationality> GetAllPersonNationalitys()
        {
            yield return PersonNationality.Neger;
            yield return PersonNationality.Asian;
            yield return PersonNationality.European;
            yield return PersonNationality.Australian;
        }

        public string GetInfo()
        {
            return $"{FullName.GetInfo()} , In prison {IsInPrison} , Hight {HeightInCentimeters} , Weight {WeightInKilograms} , Nationality {Nationality}.";
        }

        public enum PersonNationality
        {
            Neger,
            Asian,
            European,
            Australian
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

using System;
using System.Collections.Generic;
using System.Linq;

namespace Hospital
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var patients = new List<Patient>();

            patients.Add(new Patient(new FullName("Joo", "Jower", "Joler"), 12, "Appendicitis"));
            patients.Add(new Patient(new FullName("Ka", "Jower", "Joler"), 25, "Appendicitis"));
            patients.Add(new Patient(new FullName("Joo", "Lear", "Joler"), 32, "Appendicitis"));
            patients.Add(new Patient(new FullName("Joo", "Paku", "Damst"), 8, "Appendicitis"));
            patients.Add(new Patient(new FullName("Alkabet", "Wee", "Jowy"), 12, "knife wound"));
            patients.Add(new Patient(new FullName("Ka", "Tep", "Kawr"), 25, "knife wound"));
            patients.Add(new Patient(new FullName("Jui", "Lii", "Pads"), 32, "knife wound"));
            patients.Add(new Patient(new FullName("Davi", "Paku", "Damst"), 8, "knife wound"));
            patients.Add(new Patient(new FullName("Joo", "Lear", "Joler"), 32, "energy damage"));
            patients.Add(new Patient(new FullName("Maxim", "Darv", "Dasart"), 8, "energy damage"));

            var patientsDatabaseTerminal = new PatientsDatabaseTerminal(patients);
            patientsDatabaseTerminal.Activate();

            Console.ReadKey(true);
        }
    }

    public class PatientsDatabaseTerminal
    {
        private readonly PatientsDatabase _patientsDatabase;

        public PatientsDatabaseTerminal(IEnumerable<Patient> criminalPersons)
        {
            _patientsDatabase = new PatientsDatabase(criminalPersons);
        }

        public void Activate()
        {
            const string SortByNameCommand = "SORTNAME";
            const string SortByAgeCommand = "SORTAGE";
            const string SearchByDiseaseCommand = "SEARCHDISEASE";
            const string ExitCommand = "EXIT";

            bool isOpen = true;

            while (isOpen)
            {
                Console.WriteLine($"\nCommands:" +
                    $"\n{SortByNameCommand}" +
                    $"\n{SortByAgeCommand}" +
                    $"\n{SearchByDiseaseCommand}" +
                    $"\n{ExitCommand}");

                var userCommand = Console.ReadLine().ToUpper();

                switch (userCommand)
                {
                    case SortByNameCommand:
                        SortByFullName();
                        break;
                    case SortByAgeCommand:
                        SortByAge();
                        break;
                    case SearchByDiseaseCommand:
                        SearchByDisease();
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

        private void SortByFullName()
        {
            _patientsDatabase.SortByFullName();
            ShowPatients(_patientsDatabase.GetAllPatients());
        }

        private void SortByAge()
        {
            _patientsDatabase.SortByAge();
            ShowPatients(_patientsDatabase.GetAllPatients());
        }

        private void SearchByDisease()
        {
            Console.WriteLine("Searching disease: ");

            var searchingDisease = Console.ReadLine();

            ShowPatients(_patientsDatabase.SearchByDisease(searchingDisease));
        }

        private void ShowPatients(IEnumerable<Patient> criminalPersons)
        {
            foreach (var criminalPerson in criminalPersons)
            {
                Console.WriteLine(criminalPerson.GetInfo());
            }
        }
    }

    public class PatientsDatabase
    {
        private IEnumerable<Patient> _patients;

        public PatientsDatabase(IEnumerable<Patient> criminalPersons)
        {
            _patients = criminalPersons;
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            return _patients;
        }

        public void SortByFullName()
        {
            _patients = _patients.OrderBy(patients => patients.FullName.Name)
                                 .ThenBy(patients => patients.FullName.Surname)
                                 .ThenBy(patients => patients.FullName.Patronymic);
        }

        public void SortByAge()
        {
            _patients = _patients.OrderBy(patients => patients.Age);
        }

        public IEnumerable<Patient> SearchByDisease(string disease)
        {
            return _patients.Where(patient => patient.Disease.ToUpper() == disease.ToUpper());
        }
    }

    public struct Patient
    {
        public readonly FullName FullName;

        public int Age;

        public readonly string Disease;

        public Patient(FullName fullName, int age, string disease)
        {
            FullName = fullName;
            Age = Math.Max(age, 0);
            Disease = disease;
        }

        public string GetInfo()
        {
            return $"{FullName.GetInfo()} , Age {Age} , Disease {Disease}.";
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

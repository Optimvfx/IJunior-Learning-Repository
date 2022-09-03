using System;
using System.Linq;

namespace CompanyConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string AddWorkerCommand = "ADD";
            const string SeeAllWorkersCommand = "SEEALL";
            const string RemoveWorkerCommand = "REMOVE";
            const string SeeAllWorkersWitchSurnameCommand = "SEARCH";
            const string ExitCommand = "EXIT";

            var workersFullNames = new FullName[0];
            var workersPosts = new string[0];

            var isOpen = true;

            while (isOpen)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nPosible commands:" +
                    $"\n{AddWorkerCommand}" +
                    $"\n{SeeAllWorkersCommand}" +
                    $"\n{RemoveWorkerCommand}" +
                    $"\n{SeeAllWorkersWitchSurnameCommand}" +
                    $"\n{ExitCommand}");

                Console.ForegroundColor = ConsoleColor.Cyan;
                var userCommand = Console.ReadLine().ToUpper();

                switch (userCommand)
                {
                    case AddWorkerCommand:
                        AddWorker(ref workersFullNames, ref workersPosts);
                        break;
                    case SeeAllWorkersCommand:
                        WriteAllWorkersInfo(workersFullNames, workersPosts);
                        break;
                    case RemoveWorkerCommand:
                        RemoveWorker(ref workersFullNames, ref workersPosts);
                        break;
                    case SeeAllWorkersWitchSurnameCommand:
                        WriteWorkersWitchSurname(workersFullNames, workersPosts);
                        break;
                    case ExitCommand:
                        isOpen = false;
                        break;
                    default:
                        WriteExteption("Unknown command!");
                        break;
                }
            }
        }

        #region help
        private static void WriteExteption(string exteption)
        {
            var prewiusColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(exteption);
            Console.ForegroundColor = prewiusColor;
        }
        #endregion help

        #region add
        private class FullName
        {
            public string Name {get; private set; }
            public string Surname { get; private set; }
            public string Patronymic { get; private set; }

            public FullName(string name, string surname, string patronymic)
            {
                Name = name;
                Surname = surname;
                Patronymic = patronymic;
            }

            public static FullName GetFullNameByUserInput()
            {
                Console.Write("Enter new worker name: ");
                var name = Console.ReadLine();

                Console.Write("Enter new worker surname: ");
                var surname = Console.ReadLine();

                Console.Write("Enter new worker patronymic: ");
                var patronymic = Console.ReadLine();

                return new FullName(name,surname,patronymic);
            }

            public bool SurfNameIsEquals(string surname)
            {
                return Surname.ToUpper() == surname.ToUpper();
            }

            public override string ToString()
            {
                return $"{Name} {Surname} {Patronymic}";
            }
        }

        private static void AddWorker(ref FullName[] workersFullNames, ref string[] workersPosts)
        {
            var newWorkerFullName = FullName.GetFullNameByUserInput();

            Console.Write("Enter new worker post: ");
            var newWorkerPost = Console.ReadLine();

            AddElementInArray(ref workersFullNames, newWorkerFullName);
            AddElementInArray(ref workersPosts, newWorkerPost);
        }

        private static void AddElementInArray<T>(ref T[] array, T element)
        {
            var newArray = new T[array.Length + 1];

            for (int i = 0; i < array.Length; i++)
                newArray[i] = array[i];

            newArray[newArray.Length - 1] = element;
            array = newArray;
        }
        #endregion add

        #region remove
        private static void RemoveWorker(ref FullName[] workersFullNames, ref string[] workersPosts)
        {
            Console.Write($"Enter removing worker index: ");
            var removingWorkerIndex = Convert.ToInt32(Console.ReadLine());

            if (workersFullNames.Length <= removingWorkerIndex || removingWorkerIndex < 0)
            {
                WriteExteption($"Cant remove worker at removing index {removingWorkerIndex}!");
            }
            else
            {
                RemoveElementInArray(ref workersFullNames, removingWorkerIndex);
                RemoveElementInArray(ref workersPosts, removingWorkerIndex);
            }
        }

        private static void RemoveElementInArray<T>(ref T[] array, int removeIndex)
        {
            var croppedArray = new T[array.Length - 1];
            int croppedArrayIndex = 0;

            for (int arrayIndex = 0; arrayIndex < array.Length; arrayIndex++)
            {
                if (removeIndex!= arrayIndex)
                {
                    croppedArray[croppedArrayIndex] = array[arrayIndex];
                    croppedArrayIndex++;
                }
            }

            array = croppedArray;
        }
        #endregion remove

        #region write
        private static void WriteAllWorkersInfo(FullName[] workersFullNames, string[] workersPosts)
        {
            var workersInfo = new string[workersFullNames.Length];

            for (int i = 0; i < workersFullNames.Length; i++)
            {
                Console.WriteLine($"#{i} // {workersFullNames[i]} - {workersPosts[i]}");
            }
        }

        private static void WriteWorkersWitchSurname(FullName[] workersFullNames, string[] workersPosts)
        {
            Console.Write("Search by surfname: ");
            var searchingSurfname = Console.ReadLine();

            Console.WriteLine($"All workers witch surfname {searchingSurfname}: ");

            for (int i = 0; i < workersFullNames.Length; i++)
            {
                if (workersFullNames[i].SurfNameIsEquals(searchingSurfname))
                {
                    Console.WriteLine($"#{i} // {workersFullNames[i]} - {workersPosts[i]}");
                }
            }
        }
        #endregion write
    }
}

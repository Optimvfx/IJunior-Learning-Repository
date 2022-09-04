using System;

namespace CompanyConsole
{
    internal class Program
    {
        private const char fullnameSplitChar = '-';

        static void Main(string[] args)
        {
            const string AddWorkerCommand = "ADD";
            const string SeeAllWorkersCommand = "SEEALL";
            const string RemoveWorkerCommand = "REMOVE";
            const string SeeAllWorkersWitchSurnameCommand = "SEARCH";
            const string ExitCommand = "EXIT";

            var workersFullNames = new string[0];
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

        #region fullname
        private static string GenerateFullName()
        {
            Console.Write("Enter new worker name: ");
            var name = Console.ReadLine();

            Console.Write("Enter new worker surname: ");
            var surname = Console.ReadLine();

            Console.Write("Enter new worker patronymic: ");
            var patronymic = Console.ReadLine();

            return $"{name}{fullnameSplitChar}{surname}{fullnameSplitChar}{patronymic}";
        }

        private static string GetSurname(string fullname)
        {
            const int SurnameIndex = 1;

            Console.WriteLine(fullname.Split(fullnameSplitChar)[0]);
            return fullname.Split(fullnameSplitChar)[SurnameIndex];
        }
        #endregion fullname

        #region add
        private static void AddWorker(ref string[] workersFullNames, ref string[] workersPosts)
        {
            var newWorkerFullName = GenerateFullName();

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
        private static void RemoveWorker(ref string[] workersFullNames, ref string[] workersPosts)
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
        private static void WriteAllWorkersInfo(string[] workersFullNames, string[] workersPosts)
        {
            for (int i = 0; i < workersFullNames.Length; i++)
            {
                Console.WriteLine($"#{i} // {workersFullNames[i]} - {workersPosts[i]}");
            }
        }

        private static void WriteWorkersWitchSurname(string[] workersFullNames, string[] workersPosts)
        {
            Console.Write("Search by surfname: ");
            var searchingSurfname = Console.ReadLine().ToUpper();

            Console.WriteLine($"All workers witch surfname {searchingSurfname}: ");

            for (int i = 0; i < workersFullNames.Length; i++)
            {
                if (GetSurname(workersFullNames[i]).ToUpper() == searchingSurfname)
                {
                    Console.WriteLine($"#{i} // {workersFullNames[i]} - {workersPosts[i]}");
                }
            }
        }
        #endregion write
    }
}

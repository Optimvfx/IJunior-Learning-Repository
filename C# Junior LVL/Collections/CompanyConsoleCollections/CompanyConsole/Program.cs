using System;
using System.Collections.Generic;

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
            const string ExitCommand = "EXIT";

            var workersFullNames = new List<string>();
            var workersPosts = new List<string>();

            var isOpen = true;

            while (isOpen)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nPosible commands:" +
                    $"\n{AddWorkerCommand}" +
                    $"\n{SeeAllWorkersCommand}" +
                    $"\n{RemoveWorkerCommand}" +
                    $"\n{ExitCommand}");

                Console.ForegroundColor = ConsoleColor.Cyan;
                var userCommand = Console.ReadLine().ToUpper();

                switch (userCommand)
                {
                    case AddWorkerCommand:
                        AddWorker(workersFullNames, workersPosts);
                        break;
                    case SeeAllWorkersCommand:
                        WriteAllWorkersInfo(workersFullNames, workersPosts);
                        break;
                    case RemoveWorkerCommand:
                        RemoveWorker(workersFullNames, workersPosts);
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
        #endregion fullname

        #region add
        private static void AddWorker(List<string> workersFullNames, List<string> workersPosts)
        {
            var newWorkerFullName = GenerateFullName();

            Console.Write("Enter new worker post: ");
            var newWorkerPost = Console.ReadLine();

            workersFullNames.Add(newWorkerFullName);
            workersPosts.Add(newWorkerPost);
        }
        #endregion add

        #region remove
        private static void RemoveWorker(List<string> workersFullNames, List<string> workersPosts)
        {
            Console.Write($"Enter removing worker index: ");
            var removingWorkerIndex = Convert.ToInt32(Console.ReadLine());

            if (workersFullNames.Count <= removingWorkerIndex || removingWorkerIndex < 0)
            {
                WriteExteption($"Cant remove worker at removing index {removingWorkerIndex}!");
            }
            else
            {
                workersFullNames.RemoveAt(removingWorkerIndex);
                workersPosts.RemoveAt(removingWorkerIndex);
            }
        }
        #endregion remove

        #region write
        private static void WriteAllWorkersInfo(List<string> workersFullNames, List<string> workersPosts)
        {
            for (int i = 0; i < workersFullNames.Count; i++)
            {
                Console.WriteLine($"#{i} // {workersFullNames[i]} - {workersPosts[i]}");
            }
        }
        #endregion write
    }
}

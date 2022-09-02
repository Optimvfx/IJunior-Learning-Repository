using System;

namespace CompanyConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var workersNames = new string[0];
            var workersPosts = new string[0];

            var isOpen = true;

            const string AddWorkerCommand = "ADD";
            const string SeeAllWorkersCommand = "SEEALL";
            const string RemoveWorkerCommand = "REMOVE";
            const string ExitCommand = "EXIT";

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
                        Console.Write("Enter new worker name: ");
                        var newWorkerName = Console.ReadLine();

                        Console.Write("Enter new worker post: ");
                        var newWorkerPost = Console.ReadLine();

                        AddWorker(ref workersNames, ref workersPosts, newWorkerName, newWorkerPost);
                        break;
                    case SeeAllWorkersCommand:
                        WriteAllWorkersInfo(workersNames, workersPosts);
                        break;
                    case RemoveWorkerCommand:
                        Console.Write("Removing worker removing Index: ");
                        var removingWorkerIndex = Convert.ToInt32(Console.ReadLine());

                        if (TryRemoveWorker(ref workersNames, ref workersPosts, removingWorkerIndex) == false)
                        {
                            WriteExteption($"Cant remove worker at removing Index {removingWorkerIndex}!");
                        }
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

        public static void WriteExteption(string exteption)
        {
            var prewiusColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(exteption);
            Console.ForegroundColor = prewiusColor;
        }

        public static void AddWorker(ref string[] workersNames,ref string[] workersPosts,string newWorkerName,string newWorkerPost)
        {
            AddElementInArray(ref workersNames, newWorkerName);
            AddElementInArray(ref workersPosts, newWorkerPost);
        }

        public static void AddElementInArray(ref string[] array, string element)
        {
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = element;
        }

        public static bool TryRemoveWorker(ref string[] workersNames,ref string[] workersPosts, int removingWorkerIndex)
        {
            if (workersNames.Length != workersPosts.Length)
                return false;

            if(workersNames.Length <= removingWorkerIndex || removingWorkerIndex < 0)
                return false;

            RemoveElementInArray(ref workersNames, removingWorkerIndex);
            RemoveElementInArray(ref workersPosts, removingWorkerIndex);

            return true;
        }

        public static void RemoveElementInArray(ref string[] array, int removeIndex)
        {
            var removeResult = new string[array.Length - 1];
            int j = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (removeIndex!= i)
                {
                    removeResult[j] = array[i];
                    j++;
                }
            }
            array = removeResult;
        }

        public static void WriteAllWorkersInfo(string[] workersNames, string[] workersPosts)
        {
            var workersInfo = GetAllWorkersInfo(workersNames, workersPosts);

            foreach (var workerInfo in workersInfo)
                Console.WriteLine(workerInfo);
        }

        public static string[] GetAllWorkersInfo(string[] workersNames, string[] workersPosts)
        {
            if (workersNames.Length != workersPosts.Length)
                return null;

            var workersInfo = new string[workersNames.Length];

            for (int i = 0; i < workersNames.Length; i++)
            {
                workersInfo[i] = $"#{i} // {workersNames[i]} - {workersPosts[i]}";
            }

            return workersInfo;
        }
    }
}

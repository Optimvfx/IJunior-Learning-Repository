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
                        ApplyAddWorkerCommand(ref workersNames, ref workersPosts);
                        break;
                    case SeeAllWorkersCommand:
                        WriteAllWorkersInfo(workersNames, workersPosts);
                        break;
                    case RemoveWorkerCommand:
                        ApplayRemoveWorkerCommand(ref workersNames, ref workersPosts);
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
        public static void WriteExteption(string exteption)
        {
            var prewiusColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(exteption);
            Console.ForegroundColor = prewiusColor;
        }
        #endregion help

        #region add
        public static void ApplyAddWorkerCommand(ref string[] workersNames, ref string[] workersPosts)
        {
            Console.Write("Enter new worker name: ");
            var newWorkerName = Console.ReadLine();

            Console.Write("Enter new worker post: ");
            var newWorkerPost = Console.ReadLine();

            AddWorker(ref workersNames, ref workersPosts, newWorkerName, newWorkerPost);
        }

        public static void AddWorker(ref string[] workersNames,ref string[] workersPosts,string newWorkerName,string newWorkerPost)
        {
            AddElementInArray(ref workersNames, newWorkerName);
            AddElementInArray(ref workersPosts, newWorkerPost);
        }

        public static void AddElementInArray(ref string[] array, string element)
        {
            var newArray = new string[array.Length + 1];
    
            for(int i = 0; i < array.Length; i++)
                newArray[i] = array[i];

           newArray[newArray.Length - 1] = element;
            array = newArray;
        }
        #endregion add

        #region remove
        public static void ApplayRemoveWorkerCommand(ref string[] workersNames, ref string[] workersPosts)
        {
            Console.Write("Removing worker removing Index: ");
            var removingWorkerIndex = Convert.ToInt32(Console.ReadLine());

            if (TryRemoveWorker(ref workersNames, ref workersPosts, removingWorkerIndex) == false)
            {
                WriteExteption($"Cant remove worker at removing Index {removingWorkerIndex}!");
            }
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
            var croppedArray = new string[array.Length - 1];
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

        #region writeAll
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
        #endregion writeAll
    }
}

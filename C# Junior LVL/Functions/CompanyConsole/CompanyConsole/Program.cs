using System;

namespace CompanyConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string AddWorkerCommand = "ADD";
            const string SeeAllWorkersCommand = "SEEALL";
            const string RemoveWorkerCommand = "REMOVE";
            const string ExitCommand = "EXIT";

            var workersFio = new string[0];
            var workersPosts = new string[0];

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
                        ApplyAddWorkerCommand(ref workersFio, ref workersPosts);
                        break;
                    case SeeAllWorkersCommand:
                        WriteAllWorkersInfo(workersFio, workersPosts);
                        break;
                    case RemoveWorkerCommand:
                        ApplayRemoveWorkerCommand(ref workersFio, ref workersPosts);
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
        private static void ApplyAddWorkerCommand(ref string[] workersFio, ref string[] workersPosts)
        {
            Console.Write("Enter new worker FIO: ");
            var newWorkerName = Console.ReadLine();

            Console.Write("Enter new worker post: ");
            var newWorkerPost = Console.ReadLine();

            AddWorker(ref workersFio, ref workersPosts, newWorkerName, newWorkerPost);
        }

        private static void AddWorker(ref string[] workersFio, ref string[] workersPosts,string newWorkerFio, string newWorkerPost)
        {
            AddElementInArray(ref workersFio, newWorkerFio);
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
        private static void ApplayRemoveWorkerCommand(ref string[] workersFio, ref string[] workersPosts)
        {
            Console.Write("Removing worker removing Index: ");
            var removingWorkerIndex = Convert.ToInt32(Console.ReadLine());

            if (TryRemoveWorker(ref workersFio, ref workersPosts, removingWorkerIndex) == false)
            {
                WriteExteption($"Cant remove worker at removing Index {removingWorkerIndex}!");
            }
        }

        private static bool TryRemoveWorker(ref string[] workersFio, ref string[] workersPosts, int removingWorkerIndex)
        {
            if (workersFio.Length != workersPosts.Length)
                return false;

            if(workersFio.Length <= removingWorkerIndex || removingWorkerIndex < 0)
                return false;

            RemoveElementInArray(ref workersFio, removingWorkerIndex);
            RemoveElementInArray(ref workersPosts, removingWorkerIndex);

            return true;
        }

        private static void RemoveElementInArray(ref string[] array, int removeIndex)
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
        private static void WriteAllWorkersInfo(string[] workersFio, string[] workersPosts)
        {
            var workersInfo = GetAllWorkersInfo(workersFio, workersPosts);

            foreach (var workerInfo in workersInfo)
                Console.WriteLine(workerInfo);
        }

        private static string[] GetAllWorkersInfo(string[] workersFio, string[] workersPosts)
        {
            if (workersFio.Length != workersPosts.Length)
                return null;

            var workersInfo = new string[workersFio.Length];

            for (int i = 0; i < workersFio.Length; i++)
            {
                workersInfo[i] = $"#{i} // {workersFio[i]} - {workersPosts[i]}";
            }

            return workersInfo;
        }
        #endregion writeAll
    }
}

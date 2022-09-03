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
        private static void AddWorker(ref string[] workersFullNames, ref string[] workersPosts)
        {
            Console.Write("Enter new worker full name: ");
            var newWorkerName = Console.ReadLine();

            Console.Write("Enter new worker post: ");
            var newWorkerPost = Console.ReadLine();

            AddElementInArray(ref workersFullNames, newWorkerName);
            AddElementInArray(ref workersPosts, newWorkerPost);
        }

        private static void AddElementInArray(ref string[] array, string element)
        {
            var newArray = new string[array.Length + 1];
    
            for(int i = 0; i < array.Length; i++)
                newArray[i] = array[i];

           newArray[newArray.Length - 1] = element;
            array = newArray;
        }
        #endregion add

        #region remove
        private static void RemoveWorker(ref string[] workersFullNames, ref string[] workersPosts)
        {
            const string RemoveByIndexCommand = "INDEX";
            const string RemoveByFullNameCommand = "FULLNAME";
            const string RemoveByPostCommand = "POST";

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nPosible remove commands:" +
                $"\n{RemoveByIndexCommand}" +
                $"\n{RemoveByFullNameCommand}" +
                $"\n{RemoveByPostCommand}");
            Console.ForegroundColor = ConsoleColor.Cyan;

            var removeCommand = Console.ReadLine().ToUpper();

            switch (removeCommand)
            {
                case RemoveByIndexCommand:
                    RemoveWorkerByIndex(ref workersFullNames, ref workersPosts);
                    break;
                case RemoveByFullNameCommand:
                    RemoveWorkerByFullName(ref workersFullNames, ref workersPosts);
                    break;
                case RemoveByPostCommand:
                    RemoveWorkerByPost(ref workersFullNames, ref workersPosts);
                    break;

                default:
                    WriteExteption("Uncnovn command!");
                    break; 
            }
        }

        private static void RemoveWorker(ref string[] workersFullNames, ref string[] workersPosts, int removingWorkerIndex)
        {
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

        private static void RemoveWorkerByIndex(ref string[] workersFullNames, ref string[] workersPosts)
        {
            Console.Write("Removing worker index: ");
            var removingWorkerIndex = Convert.ToInt32(Console.ReadLine());

            RemoveWorker(ref workersFullNames, ref workersPosts,removingWorkerIndex);
        }

        private static void RemoveWorkerByFullName(ref string[] workersFullNames, ref string[] workersPosts)
        {
            Console.Write("Removing worker full name: ");
            var removingWorkerFullName = Console.ReadLine();

            for (int i = 0; i < workersFullNames.Length; i++)
            {
                if (workersFullNames[i] == removingWorkerFullName)
                {
                    RemoveWorker(ref workersFullNames, ref workersPosts, i);
                    return;
                }
            }

            WriteExteption($"No worker vs full name {removingWorkerFullName}!");
        }

        private static void RemoveWorkerByPost(ref string[] workersFullNames, ref string[] workersPosts)
        {
            Console.Write("Removing worker post: ");
            var removingWorkerPost = Console.ReadLine();

            for (int i = 0; i < workersPosts.Length; i++)
            {
                if (workersPosts[i] == removingWorkerPost)
                {
                    RemoveWorker(ref workersFullNames, ref workersPosts, i);
                    return;
                }
            }

            WriteExteption($"No worker vs post {removingWorkerPost}!");
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
        private static void WriteAllWorkersInfo(string[] workersFullNames, string[] workersPosts)
        {
            var workersInfo = GetAllWorkersInfo(workersFullNames, workersPosts);

            foreach (var workerInfo in workersInfo)
                Console.WriteLine(workerInfo);
        }

        private static string[] GetAllWorkersInfo(string[] workersFullNames, string[] workersPosts)
        {
            if (workersFullNames.Length != workersPosts.Length)
                return null;

            var workersInfo = new string[workersFullNames.Length];

            for (int i = 0; i < workersFullNames.Length; i++)
            {
                workersInfo[i] = $"#{i} // {workersFullNames[i]} - {workersPosts[i]}";
            }

            return workersInfo;
        }
        #endregion writeAll
    }
}

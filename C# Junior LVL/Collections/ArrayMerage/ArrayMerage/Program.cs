using System;
using System.Collections.Generic;

namespace ArrayMerage
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var first = new int[] { 3, 6, 1, 6, 1, 6, 4, 3 };
            var second = new int[] { 6,17,17,2,671,725,26,15};

            Console.WriteLine("First array:");

            ShowCollection(first);

            Console.WriteLine("Second array:");

            ShowCollection(second);

            Console.WriteLine("Mireged list:");
            var miragedList = RemovalRepetitions(MerageCollections(first, second));

            ShowCollection(miragedList);

            Console.ReadKey();
        }

        private static void ShowCollection<T>(IEnumerable<T> collection)
        {
            foreach (var element in collection)
                Console.WriteLine(element);
        }

        private static List<T> RemovalRepetitions<T>(IEnumerable<T> collection)
        {
            var individualElementsInCollections = new List<T>();    

            foreach(var element in collection)
            {
                if(individualElementsInCollections.Contains(element) == false)
                {
                    individualElementsInCollections.Add(element);
                }
            }

            return individualElementsInCollections;
        }

        private static List<T> MerageCollections<T>(IEnumerable<T> first,IEnumerable<T> second)
        {
            var meragedCollection = new List<T>();

            foreach(var element in first)
                meragedCollection.Add(element);

            foreach (var element in second)
                meragedCollection.Add(element);

            return meragedCollection;
        }
    }
}

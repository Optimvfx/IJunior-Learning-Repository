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
            var miragedList = new List<int>();

            Add(miragedList,first);
            Add(miragedList, second);

            ShowCollection(miragedList);

            Console.ReadKey();
        }

        private static void ShowCollection<T>(IEnumerable<T> collection)
        {
            foreach (var element in collection)
                Console.WriteLine(element);
        }

        private static void Add<T>(List<T> collection, IEnumerable<T> first)
        {
            var individualElementsInCollections = new List<T>();

            foreach(var element in collection)
            {
                individualElementsInCollections.Add(element);
            }

            foreach (var element in first)
            {
                if (individualElementsInCollections.Contains(element) == false)
                {
                    individualElementsInCollections.Add(element);
                    collection.Add(element);
                }
            }
        }
    }
}

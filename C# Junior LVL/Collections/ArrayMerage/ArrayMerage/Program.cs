﻿using System;
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

            foreach (var element in first)
               Console.WriteLine(element);

            Console.WriteLine("Second array:");

            foreach (var element in second)
                Console.WriteLine(element);

            Console.WriteLine("Mireged list:");
            var miragedList = MerageCollections(first, second);

            foreach (var element in miragedList)
                Console.WriteLine(element);

            Console.ReadKey();
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

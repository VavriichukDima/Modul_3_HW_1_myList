using System;
using System.Collections.Generic;

namespace Modul_3_HW_1_myList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var myList = new MyList<int>(5) { 1, 2, 3, 4, 5 };
            var array = new MyList<int> { 6, 7, 8, 9 };
            myList.AddRange(array);
            myList.Capacity = 2;
            Console.WriteLine("Before remove");
            Console.WriteLine($"Count = {myList.Count}; Capacity = {myList.Capacity}");
            foreach (var item in myList)
            {
                Console.Write($"{item}, ");
            }

            myList.Remove(4);
            myList.RemoveAt(4);
            myList.Sort(new IntComparer());
            Console.WriteLine("\n");
            Console.WriteLine("After remove & sort");
            Console.WriteLine($"Count = {myList.Count}; Capacity = {myList.Capacity}");

            foreach (var item in myList)
            {
                Console.Write($"{item}, ");
            }
        }
    }
}

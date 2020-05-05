using System;
using System.Collections.Generic;

namespace DataStructures
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] inputs = new int[] {7,8,9};
            GenericList<int> input = new GenericList<int>() { 23, 4234, 8 };
            GenericList<int> hej = new GenericList<int>();
            hej.Add(1);
            hej.Add(3);
           
            hej.Add(5);
           
            hej.Add(7);
            hej.Add(9);

           
            hej.Print();

            Console.WriteLine(hej.FindLast(x => x < -1));
            Console.WriteLine(hej.TrueForAll(x => x > 6));

            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericDataStructures
{
    class Program
    {
        static void Main(string[] args)
        {
            // just some simpe code showing how the structures can be used
            BST<int> tree = new BST<int>();

            tree.Add(3);
            tree.Add(2);
            tree.Add(9);
            tree.Add(8);
            tree.Delete(3);
            tree.Print(TraversalType.inOrder);

            Heap<int> heap = new Heap<int>(100, true);
            heap.Add(4);
            heap.Add(3);
            heap.Add(6);
            heap.Add(2);
            heap.Add(7);
            heap.Print();
            Console.WriteLine(heap.RemoveFirst());
        }
    }
}

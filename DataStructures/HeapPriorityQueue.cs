using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class HeapPriorityQueue<T> where T : IComparable
    {
        public int Count { get; private set; } = 0;
        private Heap<T> heap;

        public HeapPriorityQueue(int size, bool max = true)
        {
            heap = new Heap<T>(size, max);
        }

        public void Enqueue(T item)
        {
            heap.Add(item);
            Count++;
        }

        public T Dequeue()
        {
            Count--;
            return heap.RemoveFirst();
        }
       
        public void Print()
        {
            heap.Print();
        }

        public bool Contains(T value)
        {
            return heap.Contains(value);
        }       

        public void Clear()
        {
            for(int i = 0; i < Count; i++)
            {
                heap.RemoveFirst();
            }
        }

        public T Peek()
        {
            return heap.GetBaseNode();
        }

    }

}



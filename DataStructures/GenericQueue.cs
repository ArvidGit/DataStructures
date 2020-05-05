using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    //A basic Queue that is generic and uses a linked list
    public sealed class GenericQueue<T> : IEnumerable<T>
    {
        // Can use System.Collections.Generic.LinkedList but wanted to try my own implementation of linked list
        private GenericLinkedList<T> list = new GenericLinkedList<T>();
        public int Count { get; private set; } = 0;
        public GenericQueue()
        {
           
        }

        public void Enqueue(T item)
        {
            list.AddLast(item);
            Count++;
        }

        public T Dequeue()
        {
            if(Count == 0)
            {
                throw new Exception("The queue is empty");
            }
            Count--;
            T item = list.First.Value;
            list.RemoveFirst();
            return item;
        }
        public T Peek()
        {
            if(Count == 0)
            {
                throw new Exception("The queue is empty");
            }
            return list.First.Value;
        }

        public bool Contains(T value)
        {
            return list.Contains(value);
        }

        public void Clear()
        {
            list.Clear();
            Count = 0;
        }

        public void Print()
        {
            list.Print();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

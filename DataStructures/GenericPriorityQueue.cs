﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public sealed class GenericPriorityQueue<T>: IEnumerable<T> where T: IComparable
    {
        public int Count { get; private set; } = 0;

        private GenericLinkedList<T> list = new GenericLinkedList<T>();

        public void Enqueue(T item)
        {
            if (Count == 0)
            {
                list.AddFirst(item);
                Count++;
                return;
            }
            GenericLinkedNode<T> currentNode = list.First;
            int index = 0;
            while (index <= Count)
            {
                if (item.CompareTo(currentNode.Value) < 0)
                {
                    list.AddBefore(currentNode, item);
                    Count++;
                    return;
                }
                index++;
                currentNode = currentNode.NextNode;
            }
            list.AddLast(item);
            Count++;
            
        }

        public T Dequeue()
        {
            if (Count == 0)
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
            if (Count == 0)
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

        public void PrintQueue()
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

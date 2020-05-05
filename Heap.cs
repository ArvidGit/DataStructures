using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericDataStructures
{
    //A generic heap
    //Doesn't resize if it goes over max

    public sealed class Heap<T> where T : IComparable
    {
        T[] values;
        public int Count { get; private set; } = 0;
        bool maxHeap;

        public Heap(T first, int heapSize, bool maxHeap)
        {
            values = new T[heapSize];
            Count++;
            values[0] = first;
            this.maxHeap = maxHeap;
        }

        public Heap(int heapSize, bool maxHeap)
        {
            values = new T[heapSize];
            this.maxHeap = maxHeap;
        }

        public void Add(T element)
        {
            values[Count] = element;
            Count++;
            GoUp();
        }

        public T RemoveFirst()
        {
            if(Count == 0)
            {
                return default(T);
            }
            if (Count > 0)
            {
                Count--;
            }
            T returnItem = values[0];
            Swap(0, Count);
            GoDown();
            return returnItem;
        }

        public bool Contains(T value)
        {
            for(int i = 0; i < Count; i++)
            {
                if (value.Equals(values[i]))
                {
                    return true;
                }
            }
            return false;
        }

        public T GetBaseNode()
        {
            if(Count == 0)
            {
                throw new Exception("Heap is empty");
            }
            return values[0];
        }

        bool HasChildren(int index)
        {
            return HasLeftChild(index);
        }

        void GoUp()
        {
            int index = Count - 1;
            int currentIndex = index;
            int parentIndex;
            while (true)
            {
                if (!HasParent(currentIndex))
                {
                    return;
                }
                parentIndex = GetParentIndex(currentIndex);
                if (compare(currentIndex, parentIndex) > 0)
                {
                    Swap(currentIndex, parentIndex);
                    currentIndex = parentIndex;
                }
                else
                {
                    return;
                }

            }
        }

        void Swap(int a, int b)
        {
            T temp = values[a];
            values[a] = values[b];
            values[b] = temp;
        }

        void GoDown()
        {
            int currentIndex = 0;
            while (HasChildren(currentIndex))
            {
                int biggestChild = GetLeftChildIndex(currentIndex);
                if (HasRightChild(currentIndex) && compare(GetRightChildIndex(currentIndex), biggestChild) > 0)
                {
                    biggestChild = GetRightChildIndex(currentIndex);
                }
                if (compare(currentIndex, biggestChild) > 0)
                {
                    return;
                }
                else
                {
                    Swap(currentIndex, biggestChild);
                }
                currentIndex = biggestChild;
            }
        }

        int compare(int firstIndex, int secondIndex) //a and b are index
        {
            int result = values[firstIndex].CompareTo(values[secondIndex]);
            if (!maxHeap)
            {
                return -result;
            }
            return result;
        }

        T GetParent(int index)
        {
            return values[GetParentIndex(index)];
        }

        T GetLeftChild(int index)
        {
            return values[GetLeftChildIndex(index)];
        }

        T GetRightChild(int index)
        {
            return values[GetRightChildIndex(index)];
        }

        bool HasRightChild(int index)
        {
            return GetRightChildIndex(index) < Count;
        }

        bool HasLeftChild(int index)
        {
            return GetLeftChildIndex(index) < Count;
        }

        bool HasParent(int index)
        {
            return index != 0;
        }

        int GetParentIndex(int index)
        {
            return (index - 1) / 2;
        }

        int GetLeftChildIndex(int index)
        {
            return (index * 2) + 1;
        }

        int GetRightChildIndex(int index)
        {
            return (index * 2) + 2;
        }

        public void Print()
        {
            for (int i = 0; i < Count; i++)
            {
                Console.WriteLine(values[i]);
            }
            Console.WriteLine("");
        }

    }
}


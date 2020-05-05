using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    public class GenericList<T> : IEnumerable<T>
    {
        private T[] array;
        public int Count { get; private set; }
        public int Capacity { get; private set; } = 1;

        public T this[int index] {
            get => array[index];
            set => array[index] = value; 
        }

        public GenericList()
        {
            CreateArray();
        }

        public GenericList(IEnumerable<T> collection)
        {
            CreateArray();
            AddFullCollection(collection);
        }

        public GenericList(int Capacity)
        {
            if (Capacity > 0)
            {
                this.Capacity = Capacity;
            }
            CreateArray();
        }

        private void CreateArray()
        {
            array = new T[Capacity];
        }

        private void AddFullCollection(IEnumerable<T> collection)
        {
            foreach (var t in collection)
            {
                Add(t);
            }
        }

        public void Add(T value)
        {
            array[Count] = value;
            Count++;
            CheckForResize();
        }

        public void Insert( int index, T item)
        {
            if (index > Count || index < 0)
            {
                throw new IndexOutOfRangeException();
            }
            if(index == Count)
            {
                Add(item);
                return;
            }
            Count++;
            CheckForResize();
            
            for (int i = Count-1; i > index; i--)
            {
                array[i] = array[i-1];
            }
            array[index] = item;
        }

        public void InsertRange(int index, IEnumerable<T> collection)
        {
            if (index > Count || index < 0)
            {
                throw new IndexOutOfRangeException();
            }
            if (index == Count)
            {
                AddFullCollection(collection);
                return;
            }
            int collectionCount = 0;
            foreach(var t in collection)
            {
                collectionCount++;
            }
            Count += collectionCount;
            CheckForResize();
            for (int i = Count - 1; i > index+collectionCount-1; i--)
            {
                array[i] = array[i - collectionCount];
            }
            int temp = index;
            foreach(var t in collection)
            {
                array[temp] = t;
                temp++;
            }
        }

        public void AddRange(IEnumerable<T> collection)
        {
            AddFullCollection(collection);
        }

        public int IndexOf(T item)
        {
            return IndexOf(item, 0, Count-1);
        }

        public int IndexOf(T item, int start)
        {
            return IndexOf(item, start, Count-1);
        }

        public int IndexOf(T item, int start, int end)
        {
            if(start < 0 || end < start || end > Count)
            {
                throw new IndexOutOfRangeException();
            }
            for(int i = start; i <= end; i++)
            {
                if(Equals(item, array[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        public T Find(Predicate<T> predicate)
        {
            foreach(var t in this)
            {
                if (predicate(t))
                {
                    return t;
                }
            }
            return default;
        }

        public T FindLast(Predicate<T> predicate)
        {
            for (int i = Count - 1; i >= 0; i--)
            {
                if (predicate(array[i]))
                {
                    return array[i];
                }
            }
            return default;
        }

        public int FindLastIndex(Predicate<T> predicate)
        {
            return FindLastIndex(0, Count - 1, predicate);
        }

        public int FindLastIndex(int start, Predicate<T> predicate)
        {
            return FindLastIndex(start, Count - 1, predicate);
        }

        public int FindLastIndex(int start, int end, Predicate<T> predicate)
        {
            for (int i = end; i >= start; i--)
            {
                if (predicate(array[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        public GenericList<T> FindAll(Predicate<T> predicate)
        {
            GenericList<T> tempList = new GenericList<T>();
            foreach (var t in this)
            {
                if (predicate(t))
                {
                    tempList.Add(t);
                }
            }
            return tempList;
        }

        public int FindIndex(Predicate<T> predicate)
        {
            return FindIndex(0, Count - 1, predicate);
        }

        public int FindIndex(int start, Predicate<T> predicate)
        {
            return FindIndex(start, Count - 1, predicate);
        }

        public int FindIndex(int start, int end, Predicate<T> predicate)
        {
            if (start < 0 || end < start || end > Count)
            {
                throw new IndexOutOfRangeException();
            }
            for (int i = start; i <= end; i++)
            {
                if (predicate(array[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        public T[] ToArray()
        {
            T[] temp = new T[Count];
            Array.Copy(array,temp, Count);
            return temp;
        }

        public bool TrueForAll(Predicate<T> predicate)
        {
            foreach(var t in this)
            {
                if (!predicate(t))
                {
                    return false;
                }
            }
            return true;
        }

        public GenericList<T> GetRange(int start, int count)
        {
            if(start < 0 || start + count > Count)
            {
                throw new IndexOutOfRangeException();
            }
            GenericList<T> tempList = new GenericList<T>();
            for(int i = start; i < count+start; i++)
            {
                tempList.Add(array[i]);
            }
            return tempList;
        }

        void CheckForResize()
        {
            if(Count >= Capacity)
            {
                Capacity *= 2;
                Array.Resize(ref array, Capacity);
            }
        }

        public void RemoveAt(int index)
        {
            if(index >= Count || index < 0)
            {
                throw new IndexOutOfRangeException();
            }
            RemoveFrom(index);
        }

        public void Remove(T item)
        {
            for(int i = 0; i < Count; i++)
            {
                if (EqualityComparer<T>.Default.Equals(item, array[i]))
                {
                    RemoveFrom(i);
                    break;
                }
            }
        }

        public void RemoveAll(Predicate<T> pred)
        {
            for (int i = 0; i < Count; i++)
            {
                if (pred(array[i]))
                {
                    RemoveFrom(i);
                    i--;
                }
            }
        }

        public void RemoveRange(int start, int end)
        {
            if(start < 0 || end < start || end > Count - 1)
            {
                throw new IndexOutOfRangeException();
            }
            int dif = end - start + 1;
            for(int i = start; i < Count-dif; i++)
            {
                array[i] = array[i + dif];
            }

            Count -= dif;
        }

        private void RemoveFrom(int index)
        {
            for (int i = index; i < Count - 1; i++)
            {
                array[i] = array[i + 1];
            }
            Count--;
        }

        public void Reverse()
        {
            for(int i = 0; i < Count/2; i++)
            {
                Swap(i, Count - 1 - i);
            }
        }

        public void Reverse(int start, int end)
        {
            if(start < 0 || end >= Count|| start > end)
            {
                throw new Exception();
            }
            int iterations = (end - start + 1)/2;
            for(int i = 0; i < iterations; i++)
            {
                Swap(i+ start, end-i);
            }
        }

        private void Swap(int a, int b)
        {
            T temp = array[a];
            array[a] = array[b];
            array[b] = temp;
        }

        public void Foreach(Action<T> action)
        {
            foreach(var t in this)
            {
                action(t);
            }
        }

        public int BinarySearch(T item)
        {
            return BinarySearch(item, 0, Count - 1, Count, Comparer<T>.Default);
        }

        public int BinarySearch(T item, Comparer<T> comparer)
        {
            return BinarySearch(item, 0, Count - 1, Count, comparer);
        }

        private int BinarySearch(T item, int left, int right,int greatest, Comparer<T> comparer)
        {
            int middle = left+( right - left) / 2;
            int compare = comparer.Compare(item, array[middle]);
            if (right >= left )
            {
                if (compare == 0)
                {
                    return middle;
                }
                if (compare > 0)
                {
                    return BinarySearch(item, middle+1, right, greatest, comparer);
                }

                return BinarySearch(item, left, middle-1, middle, comparer);
            }
            
            return -greatest;
        }

        bool Equals(T a, T b)
        {
            return EqualityComparer<T>.Default.Equals(a, b);
        }

        public bool IsEmpty()
        {
            return Count == 0;
        }

        public void Clear()
        {
            Count = 0;
        }

        public bool Contains(T item)
        {
            foreach(var t in this)
            {
                if (Equals(t, item))
                {
                    return true;
                }
            } 
            return false;
        }

        public bool Exists(Predicate<T> pred)
        {
            foreach(var t in this)
            {
                if (pred(t))
                {
                    return true;
                }
            }
            return false;
        }

        public void Print()
        {
            foreach(var t in this)
            {
                Console.WriteLine(t);
            }
            Console.WriteLine();
        }

        public IEnumerator<T> GetEnumerator()
        {
            for(int i = 0; i < Count; i++)
            {
                yield return array[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

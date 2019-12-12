using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericDataStructures
{
    public sealed class GenericLinkedList<T>
    {
       
        public int Count
        {
            get; private set;
        }

        public GenericLinkedNode<T> Last
        {
            get; private set;
        }
      
        public GenericLinkedNode<T> First
        {
            get; private set;
        }

        public GenericLinkedList()
        {
           
        }

        #region Search functions

        public bool Contains(T value)
        {
            GenericLinkedNode<T> currentNode = First;
            int index = 0;
            while(index < Count)
            {
                if(value.Equals(currentNode.Value))
                {
                    return true;
                }
                else
                {
                    index++;
                    currentNode = currentNode.NextNode;
                }
            }
            return false;
        }

        public GenericLinkedNode<T> Find(T value)
        {
            GenericLinkedNode<T> currentNode = First;
            int index = 0;
            while (index < Count)
            {
                if (value.Equals(currentNode.Value))
                {
                    return currentNode;
                }
                else
                {
                    index++;
                    currentNode = currentNode.NextNode;
                }
            }
            return null;
        }
        #endregion

        #region Remove functions
        public void RemoveLast()
        {
            if(Count == 0)
            {
                return;
            }
            if (Count == 1)
            {
                First = null;
                Last = null;
                Count--;
            }
            else
            {
                GenericLinkedNode<T> temp = Last.PreviousNode;
                temp.NextNode = First;
                First.PreviousNode = temp;
                Last = temp;
                Count--;
            }
        }

        public void RemoveFirst()
        {
            if (Count == 0)
            {
                return;
            }
            if (Count == 1)
            {
                First = null;
                Last = null;
                Count--;
            }
            else
            {
                GenericLinkedNode<T> temp = First.NextNode;
                temp.PreviousNode = Last;
                Last.NextNode = temp;
                First = temp;
                Count--;
            }
        }
        #endregion

        #region Add functions
        public void AddFirst(T value)
        {
            if(First == null)
            {
                AddTheFirstNode(value);
            }
            else
            {
                GenericLinkedNode<T> node = new GenericLinkedNode<T>(value);
                node.PreviousNode = First.PreviousNode;
                node.NextNode = First;
                First.PreviousNode = node;
                First = node;
                Count++;
            }
        }

        public void AddAfter(GenericLinkedNode<T> node, T value)
        {
            GenericLinkedNode<T> newNode = new GenericLinkedNode<T>(value);
            newNode.PreviousNode = node;
            newNode.NextNode = node.NextNode;
            node.NextNode = newNode;
            node.PreviousNode = newNode;
            if(Count == 1)
            {
                Last = newNode;
            }
            else if (node == Last)
            {
                Last = newNode;
            }
            Count++;
        }
        public void AddBefore(GenericLinkedNode<T> node, T value)
        {
            GenericLinkedNode<T> newNode = new GenericLinkedNode<T>(value);
            newNode.NextNode = node;
            newNode.PreviousNode = node.PreviousNode;
            node.PreviousNode = newNode;
            if(node == First)
            {
                First = newNode;
            }
            Count++;
        }

        void AddTheFirstNode(T value)
        {
            GenericLinkedNode<T> node = new GenericLinkedNode<T>(value);
            First = node;
            Last = node;
            node.NextNode = node;
            node.PreviousNode = node;
            Count++;
        }
       

        public void AddLast(T value)
        {
            if (First == null)
            {
                AddTheFirstNode(value);
            }
            else
            {
                GenericLinkedNode<T> node = new GenericLinkedNode<T>(value);
                node.PreviousNode = Last;
                node.NextNode = Last.NextNode;
                Last.NextNode = node;
                Last = node;
                Count++;
            }
        }

        #endregion

        public void Clear()
        {
            Count = 0;
            First = null;
            Last = null;
        }

        public void Print()
        {
            if (Count == 0)
            {
                Console.WriteLine("The list is empty");
                return;
            }
            PrintList(0, First);
        }

        void PrintList(int i, GenericLinkedNode<T> node)
        {
            if(i == Count-1)
            {
                return;
            }
            else
            {
                Console.WriteLine(node.Value);
                PrintList(i+=1, node.NextNode);
            }
        }

    }
}

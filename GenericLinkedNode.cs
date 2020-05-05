using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericDataStructures
{
    public sealed class GenericLinkedNode<T>
    {
        public T Value { get; internal set; }

        public GenericLinkedNode<T> NextNode { get; internal set;}

        public GenericLinkedNode<T> PreviousNode { get; internal set;}

        public GenericLinkedNode(T value)
        {
            this.Value = value;
        }

        public GenericLinkedNode(T value, GenericLinkedNode<T> nextValue, GenericLinkedNode<T> previousValue)
        {
            this.Value = value;
            this.NextNode = nextValue;
            this.PreviousNode = previousValue;
        }

    }

}

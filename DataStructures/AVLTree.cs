﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace DataStructures
{
    public class AVLTree<T> where T : IComparable<T>
    {
        private Node baseNode;

        public void Add(T value)
        {
            Node newNode = new Node { Value = value };
            if (baseNode == null)
            {
                baseNode = newNode;
            }
            else
            {
                Insert(baseNode, newNode);
            }
        }

        private void LeftRotate(Node node)
        {
            Node child = node.Right;
            child.Parent = node.Parent;
            if (child.Parent == null)
            {
                baseNode = child;
            }
            else if (child.Parent.Right == node)
            {
                child.Parent.Right = child;
            }
            else
            {
                child.Parent.Left = child;
            }
            node.Parent = child;
            node.Right = child.Left;
            child.Left = node;
            if (node.Right != null)
            {
                node.Right.Parent = node;
            }
        }

        private void RightRotate(Node node)
        {
            Node child = node.Left;
            child.Parent = node.Parent;
            if (child.Parent == null)
            {
                baseNode = child;
            }
            else if (child.Parent.Left == node)
            {
                child.Parent.Left = child;
            }
            else
            {
                child.Parent.Right = child;
            }
            node.Parent = child;
            node.Left = child.Right;
            child.Right = node;
            if (node.Left != null)
            {
                node.Left.Parent = node;
            }
        }

        private void Balance(Node node)
        {
            if (node == null)
            {
                return;
            }
            int balanceFactor = GetBalanceFactor(node);
            if (balanceFactor > 1)
            {
                if (GetBalanceFactor(node.Right) > 0)
                {
                    LeftRotate(node);
                }
                else
                {
                    RightRotate(node.Right);
                    LeftRotate(node);
                }
            }
            else if (balanceFactor < -1)
            {
                if (GetBalanceFactor(node.Left) < 0)
                {
                    RightRotate(node);
                }
                else
                {
                    LeftRotate(node.Left);
                    RightRotate(node);
                }
            }
        }

        public bool Contains(T key)
        {
            return Contains(key, baseNode);
        }

        private bool Contains(T key, Node node)
        {
            if (node == null)
            {
                return false;
            }
            if (key.CompareTo(node.Value) < 0)
            {
                return Contains(key, node.Left);
            }
            else if (key.CompareTo(node.Value) > 0)
            {
                return Contains(key, node.Right);
            }
            return true;
        }

        public int Count()
        {
            return Count(baseNode);
        }

        private int Count(Node node)
        {
            GenericQueue<Node> nodes = new GenericQueue<Node>();
            nodes.Enqueue(node);
            int count = 0;
            while (nodes.Count != 0)
            {
                Node temp = nodes.Dequeue();
                count++;
                if (temp.Left != null)
                {
                    nodes.Enqueue(temp.Left);
                }
                if (temp.Right != null)
                {
                    nodes.Enqueue(temp.Right);
                }
            }
            return count;
        }

        private int Height(Node node)
        {
            if (node == null)
            {
                return 0;
            }

            return 1 + (int)MathF.Max(Height(node.Left), Height(node.Right));
        }

        private int GetBalanceFactor(Node node)
        {
            return Height(node.Right) - Height(node.Left);
        }

        void Insert(Node currentNode, Node newNode)
        {
            if (Compare(newNode, currentNode) < 0)
            {
                if (currentNode.Left != null)
                {
                    Insert(currentNode.Left, newNode);
                }
                else
                {
                    newNode.Parent = currentNode;
                    currentNode.Left = newNode;
                }
            }
            else
            {
                if (currentNode.Right != null)
                {
                    Insert(currentNode.Right, newNode);
                }
                else
                {
                    newNode.Parent = currentNode;
                    currentNode.Right = newNode;
                }
            }
            Balance(currentNode);

        }

        public void Delete(T key)
        {
            if (baseNode == null)
            {
                throw new KeyNotFoundException("AVL is empty");
            }
            Deletion(key, baseNode);
        }

        void Deletion(T key, Node node)
        {
            if (node == null)
            {
                return;
            }
            if (key.CompareTo(node.Value) < 0)
            {
                Deletion(key, node.Left);
            }
            else if (key.CompareTo(node.Value) > 0)
            {
                Deletion(key, node.Right);
            }
            else
            {
                if (node.HasNoChildren())
                {
                    if (node == baseNode)
                    {
                        baseNode = null;
                    }
                    if (node.Parent.Left == node)
                    {
                        node.Parent.Left = null;
                    }
                    else
                    {
                        node.Parent.Right = null;
                    }

                }
                else if (node.HasOneChild())
                {
                    Node child = node.Left ?? node.Right;
                    child.Parent = node.Parent;
                    if (node == baseNode)
                    {
                        baseNode = child;
                    }
                    else if (node.Parent.Left == node)
                    {
                        node.Parent.Left = child;

                    }
                    else
                    {
                        node.Parent.Right = child;
                    }



                }
                else
                {
                    Node min = FindMin(node.Right);

                    if (min.Parent.Left != null && min.Parent.Left == min)
                    {
                        min.Parent.Left = null;
                    }
                    min.Parent = node.Parent;
                    if (node.Parent == null)
                    {
                        baseNode = min;
                    }
                    else if (node.Parent.Left == node)
                    {
                        node.Parent.Left = min;
                    }
                    else
                    {
                        node.Parent.Right = min;
                    }
                    min.Left = node.Left;
                    if (min.Left.Parent != null)
                    {
                        min.Left.Parent = min;
                    }
                    if (node.Right != min)
                    {
                        min.Right = node.Right;
                        min.Right.Parent = min;
                    }



                }
            }
            if (node.Parent != null)
            {
                Balance(node.Parent);
            }
        }

        Node FindMin(Node node)
        {
            Node currentNode = node;
            while (currentNode.Left != null)
            {
                currentNode = currentNode.Left;
            }
            return currentNode;
        }

        public void Print()
        {
            if (baseNode != null)
            {
                Print(baseNode);
                Console.WriteLine($"Base node is {baseNode.Value}");
            }
        }

        void Print(Node node)
        {
            if (node != null)
            {
                Print(node.Left);


                Console.WriteLine(node.Value);
                Print(node.Right);
            }
        }

        int Compare(Node a, Node b)
        {
            return a.Value.CompareTo(b.Value);
        }

        internal class Node
        {
            public T Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public Node Parent { get; set; }

            public bool HasNoChildren()
            {
                return Left == null && Right == null;
            }

            public bool HasOneChild()
            {
                return (Left == null && Right != null) || (Left != null && Right == null);
            }
        }

    }
}

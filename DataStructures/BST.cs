﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    //A simple binary search tree with basic self balancing
    public sealed class BST<T> where T : IComparable<T>
    {
        private Node baseNode;

        public int Count { get; private set; }

        public BST(T value)
        {
            baseNode = new Node(value);
        }

        public BST()
        {

        }

        public void Add(T value)
        {
            Node newObj = new Node(value);
            if (baseNode == null)
            {
                baseNode = newObj;
            }
            else
            {
                Add(baseNode, newObj);
            }
            Count++;
            Rebalance();
        }

        void Add(Node parentNode, Node newNode)
        {
            if (parentNode.value.CompareTo(newNode.value) == 0)
            {
                parentNode.value = newNode.value;
            }
            else if (parentNode.value.CompareTo(newNode.value) > 0)
            {
                if (parentNode.leftChild != null)
                {
                    Add(parentNode.leftChild, newNode);
                }
                else
                {
                    parentNode.leftChild = newNode;
                }
            }
            else
            {
                if (parentNode.rightChild != null)
                {
                    Add(parentNode.rightChild, newNode);
                }
                else
                {
                    parentNode.rightChild = newNode;
                }
            }
        }

        public void Rebalance()
        {
           
            if(FindDeepestNode()-1 <= FindShallowestNode())
            {
                return;
            }
            GenericList<Node> list = new GenericList<Node>();
            ToList(list, baseNode);

            int middle = (list.Count-1) / 2;
            baseNode = list[middle];
            Rebalance(baseNode,list, 0, list.Count-1);


        }

        void Rebalance(Node currentNode,GenericList<Node> list, int start, int end)
        {
            if (currentNode == null)
            {
                return;
            }
            int middle = (start + end) / 2;
            if (start <= middle - 1)
            {
                currentNode.leftChild = list[(start + middle - 1) / 2];
                Rebalance(currentNode.leftChild, list, start, middle - 1);
            }
            else
            {
                currentNode.leftChild = null;
            }
            if (middle + 1 <= end)
            {
                currentNode.rightChild = list[(middle + 1 + end) / 2];
                Rebalance(currentNode.rightChild, list, middle + 1, end);
            }
            else
            {
                currentNode.rightChild = null;
            }
     
        }

        void ToList(GenericList<Node> list, Node currentNode)
        {
            if(currentNode != null)
            {
                ToList(list, currentNode.leftChild);
                list.Add(currentNode);
                ToList(list, currentNode.rightChild);
            }
        }

        public int FindShallowestNode()
        {
            int lowest = 1000000;

            void ShallowestLevel(Node currentNode, int currentLevel = 0)
            {
                if (currentNode != null)
                {
                    ShallowestLevel(currentNode.leftChild, currentLevel + 1);
                    ShallowestLevel(currentNode.rightChild, currentLevel + 1);
                 
                    if (currentLevel < lowest && !currentNode.HasBothChildren())
                    {
                        lowest = currentLevel;
                    }
                }
            }
            ShallowestLevel(baseNode);
            return lowest;
        }

        public int FindDeepestNode()
        {
            int deepest = 0;

            void DeepestLevel(Node currentNode, int currentLevel = 0)
            {
                if(currentNode != null)
                {
                    DeepestLevel(currentNode.leftChild, currentLevel + 1);
                    DeepestLevel(currentNode.rightChild, currentLevel + 1);
                    if(currentLevel > deepest)
                    {
                        deepest = currentLevel;
                    }
                 
                }
            }
            DeepestLevel(baseNode);
            return deepest;
        }

        public void Delete(T key)
        {
            if (baseNode == null)
            {
                throw new KeyNotFoundException("BST is empty");
            }
            Count--;
            Delete(key, baseNode);
        }


        Node Delete(T key, Node node)
        {
            if (node == null)
            {
                return null;
            }
            else if (key.CompareTo(node.value) < 0)
            {
                node.leftChild = Delete(key, node.leftChild);
            }
            else if (key.CompareTo(node.value) > 0)
            {
                node.rightChild = Delete(key, node.rightChild);
            }
            else
            {
                if (node.HasNoChildren())
                {
                    return null;
                }
                else if (node.HasOneChild())
                {
                    Node childNode = node.leftChild ?? node.rightChild;
                    if (node.leftChild != null)
                    {
                        node = node.leftChild;
                    }
                    else
                    {
                        node = node.rightChild;
                    }
                    return childNode;
                }
                else // Has two children
                {
                    Node min = FindMin(node.rightChild);
                    node.value = min.value;
                    node.value = min.value;
                    node.rightChild = Delete(min.value, node.rightChild);
                }
            }
            return node;
        }

        Node FindMin(Node node)
        {
            Node currentNode = node;
            while (currentNode.leftChild != null)
            {
                currentNode = currentNode.leftChild;
            }
            return currentNode;
        }

        public T Find(T value)
        {
            if (baseNode == null)
            {
                throw new Exception("No basenode exists");
            }
            return Find(value, baseNode);
        }

        public bool ValueExists(T value)
        {
            if (baseNode == null)
            {
                return false;
            }
            return Find(value, baseNode) != null;

        }

        T Find(T value, Node node)
        {
            if (node == null)
            {
                throw new KeyNotFoundException();
            }
            else if (value.CompareTo(node.value) == 0)
            {
                return node.value;
            }
            else if (value.CompareTo(node.value) < 0)
            {
                return (Find(value, node.leftChild));
            }
            else
            {
                return (Find(value, node.rightChild));
            }

        }

        public void Print(TraversalType type)
        {
            switch (type)
            {
                case TraversalType.inOrder:
                    PrintInOrder(baseNode);
                    break;
                case TraversalType.postOrder:
                    PrintPostOrder(baseNode);
                    break;
                case TraversalType.preOrder:
                    PrintPreOrder(baseNode);
                    break;
                default:
                    PrintInOrder(baseNode);
                    break;
            }
            Console.WriteLine("");
        }

        void PrintPreOrder(Node node)
        {
            if (node != null)
            {
                Console.WriteLine(node.value);
                PrintPreOrder(node.leftChild);
                PrintPreOrder(node.rightChild);
            }
        }

        void PrintPostOrder(Node node)
        {
            if (node != null)
            {
                PrintPostOrder(node.leftChild);
                PrintPostOrder(node.rightChild);
                Console.WriteLine(node.value);
            }
        }

        void PrintInOrder(Node node)
        {
            if (node != null)
            {
                PrintInOrder(node.leftChild);
                Console.WriteLine(node.value);
                PrintInOrder(node.rightChild);
            }
        }

        internal class Node
        {
            public Node leftChild;
            public Node rightChild;
            public T value;

            public Node(T value)
            {
                this.value = value;
            }

            public bool HasNoChildren()
            {
                return leftChild == null && rightChild == null;
            }

            public bool HasOneChild()
            {
                return (leftChild == null && rightChild != null) || (leftChild != null && rightChild == null);
            }

            public bool HasBothChildren()
            {
                return leftChild != null && rightChild != null;
            }

            public bool IsRightChild(Node child)
            {
                return child.value.CompareTo(rightChild.value) == 0;
            }
        }

    }
}



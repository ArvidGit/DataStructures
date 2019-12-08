using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericDataStructures
{
    // A Binary search tree that has a key for accessing the values
    public sealed class DictionaryBST<T, Q> where T : IComparable<T>
    {
        Node baseNode;

        public DictionaryBST(T Key, Q value)
        {
            baseNode = new Node(Key, value);
        }

        public DictionaryBST()
        {

        }

        public void Add(T key, Q value)
        {
            Node newObj = new Node(key, value);
            if (baseNode == null)
            {
                baseNode = newObj;
            }
            else
            {
                Add(baseNode, newObj);
            }
        }

        void Add(Node parentNode, Node newNode)
        {
            if (parentNode.key.CompareTo(newNode.key) == 0)
            {
                parentNode.value = newNode.value;
            }
            else if (parentNode.key.CompareTo(newNode.key) > 0)
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

        public void Delete(T key)
        {
            if (baseNode == null)
            {
                throw new KeyNotFoundException("BST is empty");
            }
            Delete(key, baseNode);
        }


        Node Delete(T key, Node node)
        {
            if (node == null)
            {
                return null;
            }
            else if (key.CompareTo(node.key) < 0)
            {
                node.leftChild = Delete(key, node.leftChild);
            }
            else if (key.CompareTo(node.key) > 0)
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
                    node.key = min.key;
                    node.value = min.value;
                    node.rightChild = Delete(min.key, node.rightChild);
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

        public Q Find(T key)
        {
            if (baseNode == null)
            {
                throw new KeyNotFoundException("No basenode exists");
            }
            return Find(key, baseNode);
        }

        Q Find(T key, Node node)
        {
            if (node == null)
            {
                throw new KeyNotFoundException();
            }
            else if (key.CompareTo(node.key) == 0)
            {
                return node.value;
            }
            else if (key.CompareTo(node.key) < 0)
            {
                return (Find(key, node.leftChild));
            }
            else
            {
                return (Find(key, node.rightChild));
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

        class Node
        {
            public Node leftChild;
            public Node rightChild;
            public T key;
            public Q value;

            public Node(T key, Q value)
            {
                this.key = key;
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

            public bool IsRightChild(Node child)
            {
                return child.key.CompareTo(rightChild.key) == 0;
            }
        }

    }
}

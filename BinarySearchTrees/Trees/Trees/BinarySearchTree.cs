using System;
using System.Collections.Generic;

public class BinarySearchTree<T> where T : IComparable<T>
{
    Node<T> Root { get; set; }

    public class Node<T>
    {
        public Node(T element)
        {
            this.Value = element;
        }

        public T Value { get; private set; }
        internal Node<T> LeftChild { get; set; }
        internal Node<T> RightChild { get; set; }
    }

    public void Insert(T element)
    {
        if (this.Root == null)
        {
            this.Root = new Node<T>(element);
            return;
        }

        Node<T> currentNode = this.Root;
        Node<T> parrentNode = null;

        while (currentNode != null)
        {
            parrentNode = currentNode;

            int compareElementToValue = element.CompareTo(currentNode.Value);

            if (compareElementToValue == 0)
            {
                return;
            }
            if (compareElementToValue == 1)
            {
                currentNode = currentNode.RightChild;
            }
            else // (isValueBigger == -1)
            {
                currentNode = currentNode.LeftChild;
            }
        }

        currentNode = new Node<T>(element);

        if (element.CompareTo(parrentNode.Value) > 0)
        {
            parrentNode.RightChild = currentNode;
        }
        else
        {
            parrentNode.LeftChild = currentNode;
        }
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this.Root, action);
    }

    private void EachInOrder(Node<T> node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }
        EachInOrder(node.LeftChild, action);
        action(node.Value);
        EachInOrder(node.RightChild, action);
    }

    public bool Contains(T element)
    {
        Node<T> currentNode = this.Root;
        while (currentNode != null)
        {
            int compareElementToValue = element.CompareTo(currentNode.Value);
            if (compareElementToValue == 0)
            {
                return true;
            }
            else if (compareElementToValue == -1)
            {
                currentNode = currentNode.LeftChild;
            }
            else
            {
                currentNode = currentNode.RightChild;
            }
        }

        return false;
    }

    public void DeleteMin()
    {
        throw new NotImplementedException();
    }

    public BinarySearchTree<T> Search(T item)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<T> Range(T startRange, T endRange)
    {
        throw new NotImplementedException();
    }

}

public class Launcher
{
    public static void Main()
    {
        BinarySearchTree<int> bst = new BinarySearchTree<int>();
        bst.Insert(2);
        bst.Insert(1);
        bst.Insert(4);
        bst.Insert(3);

        bst.Insert(50);
        bst.Insert(47);
        bst.Insert(49);
        bst.Insert(48);
        bst.Insert(46);


        List<int> nodes = new List<int>();
        bst.EachInOrder(nodes.Add);
    }
}

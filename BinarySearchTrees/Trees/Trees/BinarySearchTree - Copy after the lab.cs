using System;
using System.Collections.Generic;
using System.Linq;

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

    public BinarySearchTree<T> Search(T element)
    {
        Node<T> currentNode = this.Root;

        while (currentNode != null)
        {
            int compareElementToValue = element.CompareTo(currentNode.Value);

            if (compareElementToValue == 0)
            {
                return new BinarySearchTree<T>(currentNode);
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

        return new BinarySearchTree<T>();
    }

    public BinarySearchTree()
    {

    }

    private BinarySearchTree(Node<T> root)
    {
        this.Copy(root);
    }

    private void Copy(Node<T> node)
    {
        if (node == null)
        {
            return;
        }

        this.Insert(node.Value);
        this.Copy(node.LeftChild);
        this.Copy(node.RightChild);
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
        if (this.Root == null)
        {
            return;
        }

        Node<T> minimumNode = this.Root;
        Node<T> parrentNode = null;

        while (minimumNode.LeftChild != null)
        {
            parrentNode = minimumNode;
            minimumNode = minimumNode.LeftChild;
        }

        if (parrentNode == null)
        {
            this.Root = minimumNode.RightChild;
        }
        else
        {
            parrentNode.LeftChild = minimumNode.RightChild;
        }
    }

    public IEnumerable<T> Range(T startRange, T endRange)
    {
        Queue<T> results = new Queue<T>();

        this.Range(this.Root, startRange, endRange, results);
        return results;
    }

    private IEnumerable<T> Range(Node<T> node, T startRange, T endRange, Queue<T> queue)
    {
        if (node == null)
        {
            return queue;
        }

        int compareStartrangeToValue = startRange.CompareTo(node.Value);
        int compareEndrangeToValue = endRange.CompareTo(node.Value);

        if (compareStartrangeToValue < 0)
        {
            Range(node.LeftChild, startRange, endRange, queue);
        }

        if (compareStartrangeToValue <= 0 && compareEndrangeToValue >= 0)
        {
            queue.Enqueue(node.Value);
        }

        if (compareEndrangeToValue > 0)
        {
            Range(node.RightChild, startRange, endRange, queue);
        }

        return queue;
    }

    public IEnumerable<T> RangeWithYield(T startRange, T endRange)
    {
        return this.RangeWithYield(this.Root, startRange, endRange);
    }

    private IEnumerable<T> RangeWithYield(Node<T> node, T startRange, T endRange)
    {
        if (node == null)
        {
            yield break;
        }

        int compareStartrangeToValue = startRange.CompareTo(node.Value);
        int compareEndrangeToValue = endRange.CompareTo(node.Value);

        if (compareStartrangeToValue < 0)
        {
            foreach (T element in RangeWithYield(node.LeftChild, startRange, endRange))
            {
                yield return element;
            }
        }

        if (compareStartrangeToValue <= 0 && compareEndrangeToValue >= 0)
        {
            yield return node.Value;
        }

        if (compareEndrangeToValue > 0)
        {
            foreach (T element in RangeWithYield(node.RightChild, startRange, endRange))
            {
                yield return element;
            }
        }

        yield break;
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

        var a = bst.Range(4, 48);
        var b = bst.RangeWithYield(4, 48);
    }
}

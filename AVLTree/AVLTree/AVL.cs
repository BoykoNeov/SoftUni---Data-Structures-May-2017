using System;

public class AVL<T> where T : IComparable<T>
{
    private Node<T> root;

    public Node<T> Root
    {
        get
        {
            return this.root;
        }
    }

    public bool Contains(T item)
    {
        var node = this.Search(this.root, item);
        return node != null;
    }

    public void Insert(T item)
    {
        this.root = this.Insert(this.root, item);
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this.root, action);
    }

    private Node<T> Insert(Node<T> node, T item)
    {
        if (node == null)
        {
            return new Node<T>(item);
        }

        int cmp = item.CompareTo(node.Value);
        if (cmp < 0)
        {
            node.Left = this.Insert(node.Left, item);
        }
        else if (cmp > 0)
        {
            node.Right = this.Insert(node.Right, item);
        }

        node = BalanceNode(node);
        UpdateHeight(node);
        return node;
    }

    private Node<T> BalanceNode(Node<T> node)
    {
        int balanceFactor = Height(node.Left) - Height(node.Right);

        if (balanceFactor > 1)
        {
            if (Height(node.Left.Left) - Height(node.Left.Right) < 0)
            {
                node.Left = RotateLeft(node.Left);
            }

            node = RotateRight(node);
            return node;
        }

        if (balanceFactor < -1)
        {
            if (Height(node.Right.Left) - Height(node.Right.Right) > 0)
            {
                node.Right = RotateRight(node.Right);
            }

            node = RotateLeft(node);
        }

        return node;
    }

    private int Height(Node<T> node)
    {
        if (node == null)
        {
            return 0;
        }
        else
        {
            return node.Height;
        }
    }

    private void UpdateHeight(Node<T> node)
    {
        //  node.Height = Math.Max((node.Left?.Height ?? 0), (node.Right?.Height ?? 0)) + 1;
        node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
    }

    private Node<T> RotateRight(Node<T> node)
    {
        Node<T> leftChild = node.Left;
        node.Left = leftChild.Right;
        leftChild.Right = node;

        UpdateHeight(node);

        return leftChild;
    }

    private Node<T> RotateLeft(Node<T> node)
    {
        Node<T> rightChild = node.Right;
        node.Right = rightChild.Left;
        rightChild.Left = node;

        UpdateHeight(node);

        return rightChild;
    }

    private Node<T> Search(Node<T> node, T item)
    {
        if (node == null)
        {
            return null;
        }

        int cmp = item.CompareTo(node.Value);
        if (cmp < 0)
        {
            return Search(node.Left, item);
        }
        else if (cmp > 0)
        {
            return Search(node.Right, item);
        }

        return node;
    }

    private void EachInOrder(Node<T> node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Value);
        this.EachInOrder(node.Right, action);
    }
}
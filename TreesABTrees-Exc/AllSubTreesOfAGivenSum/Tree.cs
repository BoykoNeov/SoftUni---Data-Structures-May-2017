using System;
using System.Collections.Generic;

internal class Tree<T>
{
    public T Value { get; set; }
    public IList<Tree<T>> Children { get; private set; }
    internal Tree<T> Parent { get; set; }

    internal Tree(T value, params Tree<T>[] children)
    {
        this.Value = value;
        this.Children = new List<Tree<T>>();

        foreach (Tree<T> child in children)
        {
            Children.Add(child);
            child.Parent = this;
        }
    }

    internal void Print(int indent = 0)
    {
        Console.Write(new string(' ', indent * 2));
        Console.WriteLine(this.Value);
        foreach (Tree<T> child in this.Children)
        {
            child.Print(indent + 1);
        }
    }

    internal void Each(Action<T> action)
    {
        action(this.Value);

        foreach (Tree<T> child in this.Children)
        {
            child.Each(action);
        }
    }

    internal IEnumerable<T> OrderDFS()
    {
        List<T> result = new List<T>();
        this.OrderDFSsubmethod(this, result);
        return result;
    }

    internal void OrderDFSsubmethod(Tree<T> tree, List<T> results)
    {
        foreach (Tree<T> child in tree.Children)
        {
            child.OrderDFSsubmethod(child, results);
        }

        results.Add(tree.Value);
    }

    internal IEnumerable<T> PreOrderDFS()
    {
        List<T> result = new List<T>();
        this.PreOrderDFSsubmethod(this, result);
        return result;
    }

    internal void PreOrderDFSsubmethod(Tree<T> tree, List<T> results)
    {
        results.Add(tree.Value);
        foreach (Tree<T> child in tree.Children)
        {
            child.PreOrderDFSsubmethod(child, results);
        }
    }

    internal IEnumerable<T> OrderBFS()
    {
        List<T> result = new List<T>();
        Queue<Tree<T>> treeQueue = new Queue<Tree<T>>();
        treeQueue.Enqueue(this);

        while (treeQueue.Count != 0)
        {
            Tree<T> currentTree = treeQueue.Dequeue();
            result.Add(currentTree.Value);

            foreach (Tree<T> child in currentTree.Children)
            {
                treeQueue.Enqueue(child);
            }
        }

        return result;
    }
}
using System;
using System.Collections.Generic;
using Wintellect.PowerCollections;
using System.Linq;

public class FirstLastList<T> : IFirstLastList<T> where T : IComparable<T>
{
    private LinkedList<T> byInsert = new LinkedList<T>();
    private OrderedBag<LinkedListNode<T>> byOrder;

    public FirstLastList()
    {
        this.byInsert = new LinkedList<T>();
        this.byOrder = new OrderedBag<LinkedListNode<T>>((x, y) => x.Value.CompareTo(y.Value));
    }

    public int Count
    {
        get
        {
            return this.byInsert.Count;
        }
    }

    public void Add(T element)
    {
        LinkedListNode<T> newNode = new LinkedListNode<T>(element);

        this.byOrder.Add(newNode);
        this.byInsert.AddLast(newNode);
    }

    public void Clear()
    {
        this.byInsert.Clear();
        this.byOrder.Clear();
    }

    public IEnumerable<T> First(int count)
    {
        ValidateCount(count);

        LinkedListNode<T> currentNode = this.byInsert.First;

        for (int i = 0; i < count; i++)
        {
            yield return currentNode.Value;
            currentNode = currentNode.Next;
        }
    }

    public IEnumerable<T> Last(int count)
    {
        ValidateCount(count);

        LinkedListNode<T> currentNode = this.byInsert.Last;

        for (int i = 0; i < count; i++)
        {
            yield return currentNode.Value;
            currentNode = currentNode.Previous;
        }
    }

    public IEnumerable<T> Max(int count)
    {
        ValidateCount(count);

        foreach (var node in byOrder.OrderByDescending(x => x.Value).Take(count))
        {
            yield return node.Value;
        }
    }

    public IEnumerable<T> Min(int count)
    {
        ValidateCount(count);

        foreach (var node in byOrder.Take(count))
        {
            yield return node.Value;
        }
    }

    public int RemoveAll(T element)
    {
        LinkedListNode<T> nodeToRemove = new LinkedListNode<T>(element);

        var range = byOrder.Range(nodeToRemove, true, nodeToRemove, true);
        
        foreach (LinkedListNode<T> node in range)
        {
            byInsert.Remove(node);
        }

        int removedCount = byOrder.RemoveAllCopies(nodeToRemove);
        return removedCount;
    }

    private void ValidateCount(int count)
    {
        if (count > this.byInsert.Count)
        {
            throw new ArgumentOutOfRangeException();
        }
    }
}
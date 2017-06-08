using System;
using System.Collections.Generic;
using System.Linq;

public class PriorityQueue<T> where T : IComparable<T>
{
    private List<T> heap;

    public PriorityQueue()
    {
        this.heap = new List<T>();
    }

    public int Count
    {
        get { return this.heap.Count; }
    }

    public void Enqueue(T item)
    {
        this.heap.Add(item);
        this.HeapifyUp(this.heap.Count - 1);
    }

    public T Peek()
    {
        return this.heap[0];
    }

    public T Dequeue()
    {
        if (this.Count <= 0)
        {
            throw new InvalidOperationException();
        }

        T item = this.heap[0];

        this.Swap(0, this.heap.Count() - 1);
        this.heap.RemoveAt(this.heap.Count() - 1);
        this.HeapifyDown(0);

        return item;
    }

    /// <summary>
    /// Heapifies up and element, which value was decreased after addition to the queue
    /// </summary>
    /// <param name="item">item with decreased value</param>
    public void DecreaseKey(T item)
    {
        for (int i = 0; i < heap.Count; i++)
        {
            if (heap[i].Equals(item))
            {
                HeapifyUp(i);
            }
        }
    }

    /// <summary>
    /// moves an element up, to its correct position in the heap
    /// </summary>
    /// <param name="index">index in the heap of element to be moved up</param>
    private void HeapifyUp(int index)
    {
        while (index > 0 && IsLess(index, Parent(index)))
        {
            this.Swap(index, Parent(index));
            index = Parent(index);
        }
    }

    private void HeapifyDown(int index)
    {
        while (index < this.heap.Count / 2)
        {
            int child = Left(index);
            if (HasChild(child + 1) && IsLess(child + 1, child))
            {
                child = child + 1;
            }

            if (IsLess(index, child))
            {
                break;
            }

            this.Swap(index, child);
            index = child;
        }
    }

    private bool HasChild(int child)
    {
        return child < this.heap.Count;
    }

    private static int Parent(int index)
    {
        return (index - 1) / 2;
    }

    /// <summary>
    /// returns index of left child
    /// </summary>
    /// <param name="index">index of parent</param>
    /// <returns>index of left child</returns>
    private static int Left(int index)
    {
        return 2 * index + 1;
    }

    /// <summary>
    /// returns index of right child
    /// </summary>
    /// <param name="index">index of parent</param>
    /// <returns>index of right child</returns>
    private static int Right(int index)
    {
        return Left(index) + 1;
    }

    private bool IsLess(int a, int b)
    {
        return this.heap[a].CompareTo(this.heap[b]) < 0;
    }

    /// <summary>
    /// Swaps two elements in the heap
    /// </summary>
    /// <param name="a">index of first element</param>
    /// <param name="b">index of second element</param>
    private void Swap(int a, int b)
    {
        T temp = this.heap[a];
        this.heap[a] = this.heap[b];
        this.heap[b] = temp;
    }
}
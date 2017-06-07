using System;
using System.Collections.Generic;

public class BinaryHeap<T> where T : IComparable<T>
{
    private List<T> heap;

    public BinaryHeap()
    {
        this.heap = new List<T>();
    }

    public int Count
    {
        get
        {
            return this.heap.Count;
        }
    }

    public void Insert(T item)
    {
        this.heap.Add(item);
        this.HeapifyUp(this.heap.Count - 1);
    }

    /// <summary>
    /// Heapifies up an element
    /// </summary>
    /// <param name="index">element to be heapified up</param>
    private void HeapifyUp(int index)
    {
        int parentIndex = (index - 1) / 2;

        while (index >= 0 && index > parentIndex)
        {
            if (this.heap[index].CompareTo(this.heap[parentIndex]) > 0)
            {
                Swap(index, parentIndex);
                index = parentIndex;
                parentIndex = (index - 1) / 2;
            }
            else
            {
                break;
            }
        }
    }

    /// <summary>
    /// swaps two elements in the heap
    /// </summary>
    /// <param name="firstIndex"></param>
    /// <param name="secondIndex"></param>
    private void Swap(int firstIndex, int secondIndex)
    {
        T temp;
        temp = this.heap[secondIndex];
        this.heap[secondIndex] = this.heap[firstIndex];
        this.heap[firstIndex] = temp;
    }

    /// <summary>
    /// Returns the top element of the binary heap, without removing it
    /// </summary>
    /// <returns>Top element of the heap</returns>
    public T Peek()
    {
        return this.heap[0];
    }

    /// <summary>
    /// Returns and removes the top element of the binary heap
    /// </summary>
    /// <returns>Top element of the binary heap</returns>
    public T Pull()
    {
        if (this.Count <= 0)
        {
            throw new InvalidOperationException("Heap is empty");
        }

        T result = this.heap[0];

        this.heap[0] = this.heap[Count - 1];
        this.heap.RemoveAt(Count - 1);
        this.HeapifyDown(0);


        return result;
    }

    /// <summary>
    /// Heapifies down an element in the binary heap
    /// </summary>
    /// <param name="index">index of element to be heapified down</param>
    private void HeapifyDown(int index)
    {
        while (index < heap.Count / 2)
        {
            int leftChildIndex = (index * 2) + 1;
            int rightChildIndex = (index * 2) + 2;
            int childSwapIndex = 0;

            if (leftChildIndex >= this.Count)
            {
                return;
            }
            else if (rightChildIndex >= this.Count)
            {
                childSwapIndex = leftChildIndex;
            }
            else
            {
                childSwapIndex = BiggerOfTwo(leftChildIndex, rightChildIndex);
            }

            if (this.heap[index].CompareTo(this.heap[childSwapIndex]) > 0)
            {
                return;
            }

            Swap(index, childSwapIndex);
            index = childSwapIndex;
        }
    }

    /// <summary>
    /// Returns the index of a bigger of the two elements in the heap
    /// </summary>
    /// <param name="leftChildIndex"></param>
    /// <param name="rightChildIndex"></param>
    /// <returns></returns>
    private int BiggerOfTwo(int leftChildIndex, int rightChildIndex)
    {
        if (this.heap[leftChildIndex].CompareTo(this.heap[rightChildIndex]) > 0)
        {
            return leftChildIndex;
        }
        else
        {
            return rightChildIndex;
        }
    }
}

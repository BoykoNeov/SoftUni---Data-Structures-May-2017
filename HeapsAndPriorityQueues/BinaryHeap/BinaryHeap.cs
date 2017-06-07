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

    private void Swap(int firstIndex, int secondIndex)
    {
        T temp;
        temp = this.heap[secondIndex];
        this.heap[secondIndex] = this.heap[firstIndex];
        this.heap[firstIndex] = temp;
    }

    public T Peek()
    {
        return this.heap[0];
    }

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

    private void HeapifyDown(int index)
    {
        while (true)
        {
            int leftChildIndex = (index * 2) + 1;
            if (leftChildIndex >= this.Count)
            {
                return;
            }

            int rightChildIndex = (index * 2) + 2;
            int childSwapIndex = 0;

            if (rightChildIndex >= this.Count)
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

    private int BiggerOfTwo(int leftChildIndex, int rightChildIndex)
    {
        if (this.heap[leftChildIndex].CompareTo(this.heap[rightChildIndex]) > 1)
        {
            return leftChildIndex;
        }
        else
        {
            return rightChildIndex;
        }
    }
}

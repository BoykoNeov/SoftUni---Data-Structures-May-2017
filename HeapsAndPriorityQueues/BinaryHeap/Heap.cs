using System;

/// <summary>
/// Performs a heap sort on an array
/// </summary>
/// <typeparam name="T">array on which heap sort is performed</typeparam>
public static class Heap<T> where T : IComparable<T>
{
    public static void Sort(T[] arr)
    {
        int n = arr.Length;
        for (int i = n / 2 - 1 ; i >= 0; i--)
        {
            HeapifyDown(i, arr.Length, arr);
        }

        for (int i = 0; i < n; i++)
        {
            T temp;
            temp = arr[0];
            arr[0] = arr[arr.Length - 1 - i];
            arr[arr.Length -1 - i] = temp;

            HeapifyDown(0, arr.Length -1 - i, arr);
        }
    }

    /// <summary>
    /// Heapify down an element
    /// </summary>
    /// <param name="index">index of heapified down element</param>
    /// <param name="border">limit of heapify in current array</param>
    /// <param name="arr">array in which the method works</param>
    public static void HeapifyDown(int index, int border, T[] arr)
    {
        while (true)
        {
            int leftChildIndex = (index * 2) + 1;
            int rightChildIndex = (index * 2) + 2;
            int childSwapIndex = 0;



            if (leftChildIndex >= border)
            {
                return;
            }
            else if (rightChildIndex >= border)
            {
                childSwapIndex = leftChildIndex;
            }
            else
            {
                if (arr[leftChildIndex].CompareTo(arr[rightChildIndex]) > 0)
                {
                    childSwapIndex = leftChildIndex;
                }
                else
                {
                    childSwapIndex = rightChildIndex;
                }

            }

            if (arr[index].CompareTo(arr[childSwapIndex]) > 0)
            {
                return;
            }

            T temp;
            temp = arr[index];
            arr[index] = arr[childSwapIndex];
            arr[childSwapIndex] = temp;

           index = childSwapIndex;
        }

    }
}
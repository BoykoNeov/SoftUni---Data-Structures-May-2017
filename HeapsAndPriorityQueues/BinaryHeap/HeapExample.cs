using System;

public class HeapExample
{
    static void Main()
    {
        Console.WriteLine("Created an empty heap.");
        var heap = new BinaryHeap<int>();
        heap.Insert(5);
        heap.Insert(8);
        heap.Insert(1);
        heap.Insert(3);
        heap.Insert(12);
        heap.Insert(-4);

        Console.WriteLine("Heap elements (max to min):");
        while (heap.Count > 0)
        {
            var max = heap.Pull();
            Console.WriteLine(max);
        }

        int[] array = new int[]
        {
            -10, 5, 10, 12, 15, 16, 20 , 21, 2, 0, 30, 50, 1, 100, 80, 90, 95, 11, 31, 200, 201, 202, 203, 204
        };

        Heap<int>.Sort(array);
    }
}

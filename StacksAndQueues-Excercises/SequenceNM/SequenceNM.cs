/// <summary>
/// We are given numbers n and m, and the following operations:
/// n = n + 1
/// n = n + 2
/// n = n * 2
/// Write a program that finds the shortest sequence of operations from the list 
/// above that starts from n and finishes in m.If several shortest sequences exist, find the first one of them.
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
public class SequenceNM
{

    public class Item
    {
        public int Value { get; set; }
        public Item PreviousItem { get; set; }

        public Item(int value, Item previousItem)
        {
            this.Value = value;
            this.PreviousItem = previousItem;
        }
    }

    static void Main()
    {
        Queue<Item> queue = new Queue<Item>();
        int[] inputs = Console.ReadLine().Split().Select(int.Parse).ToArray();
        int n = inputs[0];
        int m = inputs[1];

        queue.Enqueue(new Item(n, null));

        while (queue.Count > 0)
        {
            Item e = queue.Dequeue();
            if (e.Value < m)
            {
                queue.Enqueue(new Item(e.Value + 1, e));
                queue.Enqueue(new Item(e.Value + 2, e));
                queue.Enqueue(new Item(e.Value * 2, e));
            }
            if (e.Value == m)
            {
                PrintResult(e);
                break;
            }
        }
    }

    public static void PrintResult(Item currentItem)
    {
        List<int> output = new List<int>();
        while (currentItem != null)
        {
            output.Add(currentItem.Value);
            currentItem = currentItem.PreviousItem;
        }

        output.Reverse();
        Console.WriteLine(string.Join(" -> ", output));
    }
}
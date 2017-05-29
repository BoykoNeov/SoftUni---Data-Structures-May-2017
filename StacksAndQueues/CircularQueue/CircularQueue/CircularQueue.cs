using System;

public class CircularQueue<T>
{
    private const int DefaultCapacity = 4;
    private T[] elements;
    private int startIndex;
    private int endIndex;


    public int Count { get; private set; }

    public CircularQueue(int capacity = DefaultCapacity)
    {
        this.elements = new T[capacity];
    }

    public void Enqueue(T element)
    {
        if (this.Count >= this.elements.Length)
        {
            this.GrowDouble();
        }

        this.elements[endIndex] = element;
        this.endIndex = (this.endIndex + 1) % this.elements.Length;
        this.Count++;
    }

    public T Dequeue()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("Queue is empty");
        }

        T result = this.elements[startIndex];
        this.startIndex = (this.startIndex + 1) % this.elements.Length;
        this.Count--;
        return result;
    }

    private void GrowDouble()
    {
        T[] biggerArray = Copy(this.Count*2);
        elements = biggerArray;
    }

    private T[] Copy(int arrayCapacity)
    {
        if (this.Count > arrayCapacity)
        {
            throw new ArgumentOutOfRangeException("The new array can't be smaller than current elemenets count");
        }

        T[] copiedArray = new T[arrayCapacity];
        for (int i = 0; i < this.Count; i++)
        {
            copiedArray[i] = this.elements[(startIndex + i) % this.elements.Length];
        }

        this.startIndex = 0;
        this.endIndex = this.Count;
        return copiedArray;
    }

    public T[] ToArray()
    {
        T[] toArray = Copy(this.Count);
        return toArray;
    }
}


public class Example
{
    public static void Main()
    {

        CircularQueue<int> queue = new CircularQueue<int>();

        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        queue.Enqueue(4);
        queue.Enqueue(5);
        queue.Enqueue(6);

        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        int first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        queue.Enqueue(-7);
        queue.Enqueue(-8);
        queue.Enqueue(-9);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        queue.Enqueue(-10);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");
    }
}
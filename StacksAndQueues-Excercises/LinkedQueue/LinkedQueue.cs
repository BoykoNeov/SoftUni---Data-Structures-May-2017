using System;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Linked Queue
/// </summary>
/// <typeparam name="T"> Queue entries</typeparam>
public class LinkedQueue<T> : IEnumerable<T>
{
    private class QueueNode<T>
    {
        public T Value { get; private set; }

        public QueueNode<T> NextNode { get; set; }
        public QueueNode<T> PrevNode { get; set; }

        public QueueNode(T value)
        {
            this.Value = value;
        }
    }

    private QueueNode<T> head;
    private QueueNode<T> tail;

    public int Count { get; private set; }

    public void Enqueue(T element)
    {
        if (this.Count == 0)
        {
            this.head = this.tail = new QueueNode<T>(element);
        }
        else
        {
            var newHead = new QueueNode<T>(element)
            {
                NextNode = this.head
            };
            this.head.PrevNode = newHead;
            this.head = newHead;
        }

        this.Count++;
    }

    public T Dequeue()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("Queue is empty");
        }

        T lastElement = this.tail.Value;

        this.tail = this.tail.PrevNode;
        if (this.tail != null)
        {
            this.tail.NextNode = null;
        }
        else
        {
            this.head = null;
        }

        this.Count--;
        return lastElement;
    }

    public void ForEach(Action<T> action)
    {
        LinkedQueue<T>.QueueNode<T> currentNode = this.tail;
        while (currentNode != null)
        {
            action(currentNode.Value);
            currentNode = currentNode.PrevNode;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        LinkedQueue<T>.QueueNode<T> curretNode = this.tail;
        while (curretNode != null)
        {
            yield return curretNode.Value;
            curretNode = curretNode.PrevNode;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public T[] ToArray()
    {
        T[] arr = new T[this.Count];
        int index = 0;
        LinkedQueue<T>.QueueNode<T> currentNode = this.head;

        while (currentNode != null)
        {
            arr[index++] = currentNode.Value;
            currentNode = currentNode.NextNode;
        }

        return arr;
    }
}


class Example
{
    static void Main()
    {
        var list = new LinkedQueue<int>();

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");

        list.Enqueue(5);
        list.Enqueue(3);
        list.Enqueue(2);
        list.Enqueue(10);
        list.Enqueue(11);
        list.Enqueue(12);
        list.Enqueue(13);
        list.Enqueue(15);
        Console.WriteLine("Count = {0}", list.Count);

        Console.WriteLine("--------------------");
        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");
        Console.WriteLine(list.Dequeue());
        Console.WriteLine(list.Dequeue());
        Console.WriteLine(list.Dequeue());
        Console.WriteLine(list.Dequeue());
        Console.WriteLine(list.Dequeue());
        Console.WriteLine(list.Dequeue());
        Console.WriteLine(list.Dequeue());
        Console.WriteLine(list.Dequeue());
        Console.WriteLine("Count = " + list.Count);
        Console.WriteLine("--------------------");

        for (int i = 0; i < 500; i++)
        {
            list.Enqueue(i);
        }

        for (int i = 0; i < 500; i++)
        {
            Console.Write(list.Dequeue() + " ");
        }
        //list.Enqueue(100);
        //list.Enqueue(105);
        //list.Enqueue(110);
        //list.Enqueue(120);
        //list.Enqueue(200);
        //list.Enqueue(300);
        //list.Enqueue(400);
        //list.Enqueue(500);
        //Console.WriteLine("Count = " + list.Count);
        //list.ForEach(Console.WriteLine);
        //Console.WriteLine("--------------------");

        //list.Dequeue();
        //list.Dequeue();
        //list.Dequeue();
        //list.Dequeue();
        //list.Enqueue(5000);


        //list.ForEach(Console.WriteLine);
        //Console.WriteLine("--------------------");
    }
}
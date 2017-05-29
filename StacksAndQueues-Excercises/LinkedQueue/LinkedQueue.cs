using System;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Linked Queue
/// </summary>
/// <typeparam name="T"> Queue entries</typeparam>
public class LinkedQueue<T> : IEnumerable
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
        LinkedQueue<T>.QueueNode<T> currentNode = this.head;
        while (currentNode != null)
        {
            action(currentNode.Value);
            currentNode = currentNode.NextNode;
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
        LinkedQueue<T>.QueueNode<T> currentNode = this.tail;

        while (currentNode != null)
        {
            arr[index++] = currentNode.Value;
            currentNode = currentNode.PrevNode;
        }

        return arr;
    }
}
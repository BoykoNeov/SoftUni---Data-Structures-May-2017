
using System;
using System.Collections;
using System.Collections.Generic;

public class DoublyLinkedListExpanded<T> : IEnumerable<T>
{
    private class ListNode<T>
    {
        public T Value { get; set; }

        public ListNode<T> NextNode { get; set; }

        public ListNode<T> PrevNode { get; set; }

        public ListNode(T value)
        {
            this.Value = value;
        }

    }

    private ListNode<T> head;
    private ListNode<T> tail;

    public int Count { get; private set; }

    public T this[int index]
    {
        get
        {
            if (index >= this.Count || index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            int currentPosition = 0;
            DoublyLinkedListExpanded<T>.ListNode<T> currentNode = this.head;

            while (currentPosition < index)
            {
                currentNode = currentNode.NextNode;
                currentPosition++;
            }
            // must implement a test
            return currentNode.Value;
        }

        set
        {
            if (index >= this.Count || index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            int currentPosition = 0;
            DoublyLinkedListExpanded<T>.ListNode<T> currentNode = this.head;

            while (currentPosition < index)
            {
                currentNode = currentNode.NextNode;
                currentPosition++;
            }
            // must implement a test
            currentNode.Value = value;
        }
    }

    public void AddFirst(T element)
    {
        if (this.Count == 0)
        {
            this.head = this.tail = new ListNode<T>(element);
        }
        else
        {
            var newHead = new ListNode<T>(element)
            {
                NextNode = this.head
            };
            this.head.PrevNode = newHead;
            this.head = newHead;
        }

        this.Count++;
    }

    public void AddLast(T element)
    {
        if (this.Count == 0)
        {
            this.head = this.tail = new ListNode<T>(element);
        }
        else
        {
            var newTail = new ListNode<T>(element)
            {
                PrevNode = this.tail
            };
            this.tail.NextNode = newTail;
            this.tail = newTail;
        }

        this.Count++;
    }

    // must test
    public void InsertAt(T element, int position)
    {
        if (position < 0 || position >= this.Count)
        {
            throw new ArgumentOutOfRangeException("Cannot insert elements before the start of the list or at its end");
        }
        else if (position == 0)
        {
            this.AddFirst(element);
        }
        else
        {
            int index = 0;
            DoublyLinkedListExpanded<T>.ListNode<T> currentNode = this.head;
            while (index < position)
            {
                currentNode = currentNode.NextNode;
                index++;
            }

            ListNode<T> newNode = new ListNode<T>(element);
            currentNode.PrevNode.NextNode = newNode;
            currentNode.PrevNode = newNode;

            newNode.PrevNode = currentNode.PrevNode;
            newNode.NextNode = currentNode;

            this.Count++;
        }
    }

    public T RemoveFirst()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("List empty");
        }

        T firstElement = this.head.Value;
        this.head = this.head.NextNode;
        if (this.head != null)
        {
            this.head.PrevNode = null;
        }
        else
        {
            this.tail = null;
        }

        this.Count--;
        return firstElement;
    }

    public T RemoveLast()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("List empty");
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
        DoublyLinkedListExpanded<T>.ListNode<T> currentNode = this.head;
        while (currentNode != null)
        {
            action(currentNode.Value);
            currentNode = currentNode.NextNode;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        DoublyLinkedListExpanded<T>.ListNode<T> curretNode = this.head;
        while (curretNode != null)
        {
            yield return curretNode.Value;
            curretNode = curretNode.NextNode;
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
        DoublyLinkedListExpanded<T>.ListNode<T> currentNode = this.head;

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
        var list = new DoublyLinkedListExpanded<int>();

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");

        list.AddLast(5);
        list.AddFirst(3);
        list.AddFirst(2);
        list.AddLast(10);
        Console.WriteLine("Count = {0}", list.Count);

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");

        list.RemoveFirst();
        list.RemoveLast();
        list.RemoveFirst();

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");

        list.RemoveLast();

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");
        Console.WriteLine("2---------2--------2");
        list.AddLast(1);
        list.AddLast(2);
        list.AddLast(3);
        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");
        list[0] = 5;
        list[1] = 6;
        list[2] = 7;
        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");
        list.InsertAt(10, 2);
        list.ForEach(Console.WriteLine);
    }
}

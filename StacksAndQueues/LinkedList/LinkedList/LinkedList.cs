using System;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Implement a data structure LinekdList<T> that holds a sequence of elements of generic type T. It should hold a sequence of items in a sequence of linked nodes. The list should support the following operations:
/// int Count  returns the number of elements in the structure
/// void AddFirst(T item)  adds an element to the start of the sequence
/// void AddLast(T item)  adds an element to the end of the sequence
/// T RemoveFirst()  removes an element from the start of the sequence and returns the element
/// T RemoveLast()  removes an element from the end of the sequence and returns the element
/// IEnumerable<T>  implement interface
/// RemoveFirst() and RemoveLast() methods should throw InvalidOperationException if the list is empty
/// </summary>
/// <typeparam name="T">The element type of the linked list</typeparam>
public class LinkedList<T> : IEnumerable<T>
{
    public class Node
    {
        public Node (T Value)
        {
            this.Value = Value;
        }

        public T Value { get; set; }
        public Node Next { get; set; }
    }

    public int Count { get; private set; }
    public Node Head { get; private set; }
    public Node Tail { get; private set; }

    public bool IsEmpty()
    {
        return (this.Count == 0);
    }

    //public Node FindLastNodeBeforeTail()
    //{
    //    Node currentNode = this.Head;

    //    while (currentNode.Next != null)
    //    {
    //        currentNode = currentNode.Next;
    //    }

    //    return currentNode;
    //}

    public void AddFirst(T item)
    {
        Node newNode = new Node(item);

        if (IsEmpty())
        {
            this.Head = this.Tail = newNode;
        }
        else
        {
            newNode.Next = this.Head;
            this.Head = newNode;
        }

        Count++;
    }

    public void AddLast(T item)
    {
        Node newNode = new Node(item);

        if (IsEmpty())
        {
            this.Head = this.Tail = newNode;
        }
        else
        {
            this.Tail.Next = newNode;
            this.Tail = newNode;
        }

        Count++;
    }

    public T RemoveFirst()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("List is IsEmpty");
        }
        else
        {
            Node oldHead = this.Head;
            this.Head = this.Head.Next;
            Count--;
            return oldHead.Value;
        }
    }

    public T RemoveLast()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("List is IsEmpty");
        }
        else if (Count == 1)
        {
            T nodeValue = this.Head.Value;
            this.Head = this.Tail = null;
            Count--;
            return nodeValue;
        }
        else
        {
            Node nodeBeforeTail = this.Head;

            while (nodeBeforeTail.Next != this.Tail)
            {
                nodeBeforeTail = nodeBeforeTail.Next;
            }

            T nodeValue = this.Tail.Value;
            nodeBeforeTail.Next = null;
            this.Tail = nodeBeforeTail;
            Count--;
            return nodeValue;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        Node currentNode = this.Head;
        while (currentNode != null)
        {
            yield return currentNode.Value;
            currentNode = currentNode.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
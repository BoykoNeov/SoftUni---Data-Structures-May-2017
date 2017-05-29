using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class LinkedStack<T>
{
    public class Node<T>
    {
        public Node<T> NextNode { get; set; }

        public T value;

        public Node(T element)
        {
            this.value = element;
            this.NextNode = null;
        }

        public T Value
        {
            get { return this.value; }
            private set { this.value = value; }
        }
    }

    public int Count { get; private set; }

    public Node<T> FirstNode { get; private set; }

    public void Push(T element)
    {
        Node<T> newNode = new Node<T>(element);
        newNode.NextNode = this.FirstNode;
        this.FirstNode = newNode;
        this.Count++;
    }

    public T Pop()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        T headValue = this.FirstNode.Value;
        this.FirstNode = this.FirstNode.NextNode;
        this.Count--;
        return headValue;
    }

    public T[] ToArray()
    {
        T[] array = new T[this.Count];
        Node<T> currentNode = this.FirstNode;
        int index = 0;

        while (currentNode != null)
        {
            array[index] = currentNode.Value;
            currentNode = currentNode.NextNode;
            index++;
        }

        return array;
    }
}
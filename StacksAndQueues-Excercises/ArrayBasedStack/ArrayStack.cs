using System;
public class ArrayStack<T>
{
    private const int DefaultCapacity = 4;
    private T[] elements;

    public int Count { get; private set; }

    public ArrayStack(int capacity = DefaultCapacity)
    {
        this.elements = new T[capacity];
    }

    public void Push(T element)
    {
        if (this.Count == this.elements.Length)
        {
            this.GrowDouble();
        }

        this.elements[this.Count] = element;
        this.Count++;
    }

    public T Pop()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("Stack is empty");
        }

        this.Count--;
        T result = this.elements[Count];
        return result;
    }

    private void GrowDouble()
    {
        T[] biggerArray = Copy(this.Count * 2);
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
            copiedArray[i] = this.elements[i];
        }

        return copiedArray;
    }

    public T[] ToArray()
    {
        // Judge tests pass with both variants, but this one seems more correct - when ToArray() for the stack
        // is called, to return the new array in the order of how elements would have been popped
        T[] toArray = Copy(this.Count);
        T[] newAr = new T[toArray.Length];
        for (int i = 0; i < toArray.Length; i++)
        {
            newAr[i] = toArray[toArray.Length - i - 1];
        }

        return newAr;
    }
}


public class Example
{
    public static void Main()
    {
        ArrayStack<int> newStack = new ArrayStack<int>();
        //for (int i = 0; i < 50; i++)
        //{
        //    newStack.Push(i);
        //}

        //var a = newStack.ToArray();
        //for (int i = 0; i < 50; i++)
        //{
        //    Console.WriteLine(newStack.Pop());
        //}
    }
}
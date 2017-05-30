using System;
/// <summary>
/// Follow the concepts from the CircularQueue<T> class from the exercises in class. The stack is simpler than the circular queue, so you will need to follow the same logic, but more simplified. Some hints:
/// The stack capacity is this.elements.Length
/// Keep the stack size (number of elements) in this.Count
/// Push(element) just saves the element in elements [this.Count]
/// and increases this.Count
/// Push(element) should invoke Grow() in case of this.Count == this.elements.Length
/// Pop() decreases this.Count and returns this.elements [this.Count]
/// Grow() allocates a new array newElements of size 2 * this.elements.Length and copies the first this.Count elements from this.elements to newElements.Finally, assign this.elements = newElements
/// ToArray() just creates and returns a sub-array of this.elements[0…this.Count - 1]
/// Pop() should throw InvalidOperationException (or UnsupportedOperationException) if the stack is empty
/// </summary>
/// <typeparam name="T">generic stack item</typeparam>
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
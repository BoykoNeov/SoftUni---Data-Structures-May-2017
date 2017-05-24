using System;

public class ArrayList<T>
{
    public int Count { get; private set; }

    private const int initialCapacity = 2;

    private T[] items;

    public ArrayList()
    {
        this.items = new T[initialCapacity];
    }




    public T this[int index]
    {
        get
        {
            if (index >= this.Count || index < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            return this.items[index];
        }

        set
        {
            if (index >= this.Count || index < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            this.items[index] = value;
        }
    }

    public void Add(T item)
    {
        if (this.Count == this.items.Length)
        {
            this.Resize();
        }

        this.items[this.Count++] = item;
    }

    private void Resize()
    {
        T[] copy = new T[this.items.Length * 2];
        for (int i = 0; i < this.items.Length; i++)
        {
            copy[i] = this.items[i];
        }

        this.items = copy;
    }

    public T RemoveAt(int index)
    {
        if (index >= this.Count || index < 0)
        {
            throw new ArgumentOutOfRangeException();
        }

        T element = this.items[index];
        this.items[index] = default(T);

        for (int i = index; i < this.Count; i++)
        {
            this.items[i] = this.items[i + 1];
        }

        this.Count--;
        this.items[Count] = default(T);


        if (this.Count <= this.items.Length / 4)
        {
            this.Shrink();
        }

        return element;
    }

    private void Shrink()
    {
        T[] copy = new T[this.items.Length / 2];
        for (int i = 0; i < this.Count; i++)
        {
            copy[i] = this.items[i];
        }

        this.items = copy;
    }
}
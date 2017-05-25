/// <summary>
/// Implement a data structure ReversedList<T> that holds a sequence of 
/// elements of generic type T. It should hold a sequence of items in reversed order. 
/// The structure should have some capacity that grows twice when it is filled, always starting at 2.
/// The reversed list should support the following operations:
/// Add(T item) adds an element to the sequence(grow twice the underlying array to extend its capacity in case the capacity is full)
/// Count returns the number of elements in the structure
/// Capacity returns the capacity of the underlying array holding the elements of the structure
/// this[index] the indexer should access the elements by index (in range 0 … Count-1) in the reverse order of adding
/// RemoveAt(index) removes an element by index (in range 0 … Count-1) in the reverse order of adding
/// IEnumerable<T> implement an enumerator to allow iterating over the elements in a foreach loop in a reversed order of their addition
/// Hint: you can keep the elements in the order of their adding, by access them in reversed order (from end to start).
/// </summary>
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ReversedList<T> : IEnumerable<T>
    {
        private const int InitialCapacity = 2;
        private T[] items;
        private int count;
        private int capacity;

        public ReversedList()
        {
            this.Count = 0;
            this.items = new T[InitialCapacity];
            this.Capacity = InitialCapacity;
        }

        public int Count
        {
            get
            {
                return this.count;
            }
            private set
            {
                this.count = value;
            }
        }

        public int Capacity
        {
            get
            {
                return this.capacity;
            }
           private set
            {
                this.capacity = value;
            }
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= this.Count)
                {
                    throw new ArgumentOutOfRangeException();
                }

                return this.items[Count - index - 1];
            }

            set
            {
                if (index < 0 || index >= this.Count)
                {
                    throw new ArgumentOutOfRangeException();
                }

                this.items[index] = value;
            }
        }

        public void Add(T item)
        {
            if (this.count == this.capacity)
            {
                DoubleCapacity();
            }

            this.items[count] = item;
            count++;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= this.count)
            {
                throw new ArgumentOutOfRangeException();
            }

            index = this.count - index - 1;
            for (int i = index; i < this.count - index; i++)
            {
                this.items[i] = this.items[i + 1];
            }

            this.count--;
        }

        private void DoubleCapacity()
        {
            capacity = capacity * 2;
            T[] newList = new T[this.capacity];

            for (int i = 0; i < this.count; i++)
            {
                newList[i] = items[i];
            }

            items = newList;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = this.count - 1; i >= 0; i--)
            {
                yield return items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
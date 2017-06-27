using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HashTable<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
{
    private const int DefaultCapacity = 16;
    private const float LoadFactor = 0.75f;
    private int maxElements;
    private LinkedList<KeyValue<TKey, TValue>>[] hashTable;


    public int Count { get; private set; }

    public int Capacity
    {
        get
        {
           return this.hashTable.Length;
        }
    }

    public HashTable(int capacity = DefaultCapacity)
    {
        this.hashTable = new LinkedList<KeyValue<TKey, TValue>>[capacity];
        this.maxElements = (int)(capacity * LoadFactor);
    }

    public void Add(TKey key, TValue value)
    {
        // Note: throw an exception on duplicated key
        this.CheckLoad();
        int hash = Math.Abs(key.GetHashCode()) % this.Capacity;

        if (this.hashTable[hash] == null)
        {
            this.hashTable[hash] = new LinkedList<KeyValue<TKey, TValue>>();
        }

        foreach (KeyValue<TKey,TValue> kvp in this.hashTable[hash])
        {
            if (kvp.Key.Equals(key))
            {
                throw new ArgumentException("Key already present!");
            }
        }

        KeyValue<TKey,TValue> keyValue = new KeyValue<TKey, TValue>(key, value);
        this.hashTable[hash].AddLast(keyValue);
        this.Count++;
    }

    private void CheckLoad()
    {
      if (this.Count >= this.maxElements)
        {
            this.Grow();
            this.maxElements = (int)(this.Capacity * LoadFactor);
        }
    }

    private void Grow()
    {
        HashTable<TKey, TValue> newTable = new HashTable<TKey, TValue>(this.Capacity * 2);
        foreach (LinkedList<KeyValue<TKey, TValue>> linkedList in this.hashTable)
        {
            if (linkedList != null)
            {
                foreach (KeyValue<TKey, TValue> keyValue in linkedList)
                {
                    newTable.Add(keyValue.Key, keyValue.Value);
                }
            }
        }

        this.hashTable = newTable.hashTable;
        this.Count = newTable.Count;
    }

    public bool AddOrReplace(TKey key, TValue value)
    {
        this.CheckLoad();
        int hash = Math.Abs(key.GetHashCode()) % this.Capacity;

        if (this.hashTable[hash] == null)
        {
            this.hashTable[hash] = new LinkedList<KeyValue<TKey, TValue>>();
            this.hashTable[hash].AddLast(new KeyValue<TKey, TValue>(key, value));
            Count++;
            return false;
        }
        else
        {
            foreach (KeyValue<TKey, TValue> kvp in this.hashTable[hash])
            {
                if (kvp.Key.Equals(key))
                {
                    kvp.Value = value;
                }
            }

            return true;
        }
    }

    public TValue Get(TKey key)
    {
        // Note: throw an exception on missing key
        KeyValue<TKey, TValue> kvp = this.Find(key);

        if (kvp == null)
        {
            throw new KeyNotFoundException();
        }
        else
        {
            return kvp.Value;
        }
    }

    public TValue this[TKey key]
    {
        get
        {
            // Note: throw an exception on missing key
            KeyValue<TKey, TValue> kvp = this.Find(key);
            if (kvp != null)
            {
                return kvp.Value;
            }
            else
            {
                throw new KeyNotFoundException();
            }

        }
        set => this.AddOrReplace(key, value);
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        KeyValue<TKey, TValue> kvp = this.Find(key);

        if (kvp != null)
        {
            value = kvp.Value;
            return true;
        }
        else
        {
            value = default(TValue);
            return false;
        }
    }

    public KeyValue<TKey, TValue> Find(TKey key)
    {
        // GetHashCode() can return negative results
        int hash = Math.Abs(key.GetHashCode()) % this.Capacity;

        if (this.hashTable[hash] != null)
        {
            foreach (KeyValue<TKey, TValue> keyValue in this.hashTable[hash])
            {
                if (keyValue.Key.Equals(key))
                {
                    return keyValue;
                }
            }
        }

        return null;
    }

    public bool ContainsKey(TKey key)
    {
        KeyValue<TKey, TValue> kvp = this.Find(key);
        return (kvp != null);
    }

    public bool Remove(TKey key)
    {
        int hash = Math.Abs(key.GetHashCode()) % this.Capacity;
        if (this.hashTable[hash] != null)
        {
            KeyValue<TKey, TValue> temp = this.Find(key);

            if (temp != null)
            {
                this.hashTable[hash].Remove(temp);
                this.Count--;
                return true;
            }
        }

        return false;
    }

    public void Clear()
    {
        this.hashTable = new LinkedList<KeyValue<TKey, TValue>>[DefaultCapacity];
        this.Count = 0;
        this.maxElements = (int)(this.Capacity * LoadFactor);
    }

    public IEnumerable<TKey> Keys => this.hashTable.Where(z => z != null).SelectMany(x => x.Select(y => y.Key));

    public IEnumerable<TValue> Values => this.hashTable.Where(z => z != null).SelectMany(x => x.Select(y => y.Value));

    public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
    {
        foreach (LinkedList<KeyValue<TKey,TValue>> linkedList in this.hashTable)
        {
            if (linkedList != null)
            {
                foreach (var keyValue in linkedList)
                {
                    yield return keyValue;
                }
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
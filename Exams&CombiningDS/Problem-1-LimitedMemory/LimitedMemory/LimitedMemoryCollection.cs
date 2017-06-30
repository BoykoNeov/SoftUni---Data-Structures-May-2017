using System.Collections.Generic;
using System.Collections;

namespace LimitedMemory
{
    public class LimitedMemoryCollection<K, V> : ILimitedMemoryCollection<K, V>
    {
        LinkedList<Pair<K, V>> byOrder;
        Dictionary<K, LinkedListNode<Pair<K, V>>> allPairs;

        public LimitedMemoryCollection(int capacity)
        {
            this.Capacity = capacity;
            byOrder = new LinkedList<Pair<K, V>>();
            allPairs = new Dictionary<K, LinkedListNode<Pair<K, V>>>();
        } 

        public IEnumerator<Pair<K, V>> GetEnumerator()
        {
            LinkedListNode<Pair<K,V>> mostRecentNode = byOrder.Last;

            while (mostRecentNode!= null)
            {
                yield return mostRecentNode.Value;
                mostRecentNode = mostRecentNode.Previous;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int Capacity { get; private set; }

        public int Count { get; private set; }

        public void Set(K key, V value)
        {
            if (this.allPairs.ContainsKey(key))
            {
                LinkedListNode<Pair<K, V>> currentNode = allPairs[key];
            
                this.byOrder.Remove(currentNode);
                currentNode.Value.Value = value;
                this.byOrder.AddLast(currentNode);        
            }
            else
            {
                Pair<K, V> newPair = new Pair<K, V>();
                newPair.Key = key;
                newPair.Value = value;
                LinkedListNode<Pair<K, V>> newNode = new LinkedListNode<Pair<K, V>>(newPair);

                if (this.Count < this.Capacity)
                {
                    this.allPairs.Add(key, newNode);
                    this.byOrder.AddLast(newNode);
                    this.Count++;
                }
                else
                {
                    LinkedListNode<Pair<K, V>> oldestNode = byOrder.First;
                    byOrder.RemoveFirst();

                    allPairs.Remove(oldestNode.Value.Key);
                    allPairs.Add(key, newNode);
                    byOrder.AddLast(newNode);
    
                }
            }

        }

        public V Get(K key)
        {
            LinkedListNode<Pair<K, V>> currentNode = allPairs[key];

            this.byOrder.Remove(currentNode);
            this.byOrder.AddLast(currentNode);

            return allPairs[key].Value.Value;
        }
    }
}

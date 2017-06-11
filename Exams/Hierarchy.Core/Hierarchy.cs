namespace Hierarchy.Core
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.Linq;

    public class Hierarchy<T> : IHierarchy<T>
    {
        private int count;
        Node<T> root;
        Dictionary<T, Node<T>> nodes;

        public Hierarchy(T rootValue)
        {
            this.nodes = new Dictionary<T, Node<T>>();
            Node<T> newRoot = new Node<T>(rootValue);
            this.nodes.Add(rootValue, newRoot);
            this.root = newRoot;
            this.count = 1;
        }

        internal class Node<T>
        {
            internal List<Node<T>> Children { get; set; }
            internal Node<T> Parent { get; set; }
            internal T Value { get; set; }

            public Node(T value)
            {
                this.Value = value;
                this.Children = new List<Node<T>>();
            }

            public override bool Equals(object obj)
            {
                var other = obj as Node<T>;
                return this.Value.Equals(other.Value);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    int hash = this.Value.GetHashCode();
                    return hash;
                }
            }
        }

        public int Count
        {
            get
            {
                return this.count;
            }
        }

        internal Node<T> GetParentNode(T parentValue)
        {
            return this.nodes[parentValue];
        }

        public void Add(T parent, T child)
        {
            if (!this.nodes.ContainsKey(parent))
            {
                throw new ArgumentException("Element not contained in Hierarchy");
            }
            else
            {
                var indexOfContainedElement = this.nodes[parent].Children.FindIndex(x => x.Value.Equals(child));
                if (indexOfContainedElement >= 0)
                {
                    throw new ArgumentException("Children is already present");
                }
                else
                {
                    Node<T> newNode = new Node<T>(child);
                    newNode.Parent = nodes[parent];

                    this.nodes[parent].Children.Add(newNode);
                    this.nodes.Add(child, newNode);
                    this.count++;
                }
            }
        }

        public void Remove(T element)
        {
            if (!nodes.ContainsKey(element))
            {
                throw new ArgumentException("element not present in hierarchy");
            }

            Node<T> currentNode = nodes[element];
            if (currentNode == this.root)
            {
                throw new InvalidOperationException("element is root");
            }

            Node<T> parentNode = currentNode.Parent;
            parentNode.Children.Remove(currentNode);
            
            foreach (Node<T> child in currentNode.Children)
            {
                child.Parent = parentNode;
            }

            parentNode.Children.AddRange(currentNode.Children);
            nodes.Remove(currentNode.Value);
            this.count--;
        }

        public IEnumerable<T> GetChildren(T item)
        {
            if (!this.nodes.ContainsKey(item))
            {
                throw new ArgumentException("item not present in Hierarchy");
            }

            Node<T> currentNode = nodes[item];
            foreach (Node<T> child in currentNode.Children)
            {
                yield return child.Value;
            }
        }

        public T GetParent(T item)
        {
            if (!this.nodes.ContainsKey(item))
            {
                throw new ArgumentException("No such item exists in Hierarchy");
            }

            Node<T> currentNode = nodes[item];
            if (currentNode.Parent == null)
            {
                return default(T);
            }
            else
            {
                return currentNode.Parent.Value;
            }
        }

        public bool Contains(T value)
        {
            return (this.nodes.ContainsKey(value));
        }

        public IEnumerable<T> GetCommonElements(Hierarchy<T> other)
        {
            List<T> firstHierarchyKeys = this.nodes.Keys.ToList();
            List<T> secondHierarchyKeys = other.nodes.Keys.ToList();
            var result = firstHierarchyKeys.Intersect(secondHierarchyKeys);
            return result;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Queue<Node<T>> queue = new Queue<Node<T>>();
            yield return this.root.Value;

            foreach (Node<T> child in this.root.Children)
            {
                queue.Enqueue(child);
            }

            while (queue.Count > 0)
            {
                Node<T> currentNode = queue.Dequeue();
                yield return currentNode.Value;

                foreach (Node<T> child in currentNode.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
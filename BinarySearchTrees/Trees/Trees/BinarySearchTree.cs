using System;
using System.Collections.Generic;

public class BinarySearchTree<T> where T : IComparable<T>
{
    Node<T> Root { get; set; }

    public class Node<T>
    {
        public Node(T element)
        {
            this.Value = element;
        }

        public T Value { get; internal set; }
        internal Node<T> LeftChild { get; set; }
        internal Node<T> RightChild { get; set; }
    }

    public BinarySearchTree<T> Search(T element)
    {
        Node<T> currentNode = this.Root;

        while (currentNode != null)
        {
            int compareElementToValue = element.CompareTo(currentNode.Value);

            if (compareElementToValue == 0)
            {
                return new BinarySearchTree<T>(currentNode);
            }
            else if (compareElementToValue == -1)
            {
                currentNode = currentNode.LeftChild;
            }
            else
            {
                currentNode = currentNode.RightChild;
            }
        }

        return new BinarySearchTree<T>();
    }

    public BinarySearchTree()
    {

    }

    /// <summary>
    /// Copies the subtree with the given element as root
    /// </summary>
    /// <param name="root">root of the new tree</param>
    private BinarySearchTree(Node<T> root)
    {
        this.Copy(root);
    }

    /// <summary>
    /// Private method for the "Copy" method
    /// </summary>
    private void Copy(Node<T> node)
    {
        if (node == null)
        {
            return;
        }

        this.Insert(node.Value);
        this.Copy(node.LeftChild);
        this.Copy(node.RightChild);
    }

    /// <summary>
    /// Inserts a given element in the tree, does nothing if the element is already contained
    /// </summary>
    /// <param name="element">Inserts this element in the tree</param>
    public void Insert(T element)
    {
        if (this.Root == null)
        {
            this.Root = new Node<T>(element);
            return;
        }

        Node<T> currentNode = this.Root;
        Node<T> parentNode = null;

        while (currentNode != null)
        {
            parentNode = currentNode;

            int compareElementToValue = element.CompareTo(currentNode.Value);

            if (compareElementToValue == 0)
            {
                return;
            }
            if (compareElementToValue == 1)
            {
                currentNode = currentNode.RightChild;
            }
            else // (isValueBigger == -1)
            {
                currentNode = currentNode.LeftChild;
            }
        }

        currentNode = new Node<T>(element);

        if (element.CompareTo(parentNode.Value) > 0)
        {
            parentNode.RightChild = currentNode;
        }
        else
        {
            parentNode.LeftChild = currentNode;
        }
    }

    /// <summary>
    /// Traverse the tree in order and perform an action on its elements
    /// </summary>
    /// <param name="action">Delegate</param>
    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this.Root, action);
    }

    /// <summary>
    /// Private method for EachInOrder method
    /// </summary>
    private void EachInOrder(Node<T> node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }
        EachInOrder(node.LeftChild, action);
        action(node.Value);
        EachInOrder(node.RightChild, action);
    }

    /// <summary>
    /// Returns if the given element is contained in the Binary Search Tree
    /// </summary>
    /// <param name="element"> checks if this element is contained in the tree </param>
    /// <returns></returns>
    public bool Contains(T element)
    {
        Node<T> currentNode = this.Root;
        while (currentNode != null)
        {
            int compareElementToValue = element.CompareTo(currentNode.Value);
            if (compareElementToValue == 0)
            {
                return true;
            }
            else if (compareElementToValue == -1)
            {
                currentNode = currentNode.LeftChild;
            }
            else
            {
                currentNode = currentNode.RightChild;
            }
        }

        return false;
    }

    /// <summary>
    /// Deletes the smallest element in the tree. Throws exception if the tree is empty.
    /// </summary>
    public void DeleteMin()
    {
        if (this.Root == null)
        {
            throw new InvalidOperationException("Tree is empty");
        }

        Node<T> minimumNode = this.Root;
        Node<T> parentNode = null;

        while (minimumNode.LeftChild != null)
        {
            parentNode = minimumNode;
            minimumNode = minimumNode.LeftChild;
        }

        if (parentNode == null)
        {
            this.Root = minimumNode.RightChild;
        }
        else
        {
            parentNode.LeftChild = minimumNode.RightChild;
        }
    }

    /// <summary>
    /// Deletes the minimum element in the BST and returns its value
    /// </summary>
    /// <returns></returns>
    public T DeleteMinWithReturn()
    {
        if (this.Root == null)
        {
            throw new ArgumentOutOfRangeException("Tree is empty");
        }

        Node<T> minimumNode = this.Root;
        Node<T> parentNode = null;

        while (minimumNode.LeftChild != null)
        {
            parentNode = minimumNode;
            minimumNode = minimumNode.LeftChild;
        }

        if (parentNode == null)
        {
            T result = this.Root.Value;
            this.Root = minimumNode.RightChild;
            return result;
        }
        else
        {
            T result = parentNode.LeftChild.Value;
            parentNode.LeftChild = minimumNode.RightChild;
            return result;
        }
    }

    /// <summary>
    /// IEnumerable<T> Range(T, T) – Returns collection with the elements found in the BST. Both borders are inclusive. - implemented with a queue
    /// </summary>
    /// <param name="startRange">Start range (inclusive)</param>
    /// <param name="endRange">End range (inclusive)</param>
    /// <returns>new BST in the given range</returns>
    public IEnumerable<T> Range(T startRange, T endRange)
    {

        Queue<T> results = new Queue<T>();

        this.Range(this.Root, startRange, endRange, results);
        return results;
    }

    /// <summary>
    /// Private method for the "Range" method
    /// </summary>
    private IEnumerable<T> Range(Node<T> node, T startRange, T endRange, Queue<T> queue)
    {
        if (node == null)
        {
            return queue;
        }

        int compareStartrangeToValue = startRange.CompareTo(node.Value);
        int compareEndrangeToValue = endRange.CompareTo(node.Value);

        if (compareStartrangeToValue < 0)
        {
            Range(node.LeftChild, startRange, endRange, queue);
        }

        if (compareStartrangeToValue <= 0 && compareEndrangeToValue >= 0)
        {
            queue.Enqueue(node.Value);
        }

        if (compareEndrangeToValue > 0)
        {
            Range(node.RightChild, startRange, endRange, queue);
        }

        return queue;
    }

    /// <summary>
    /// IEnumerable<T> Range(T, T) – Returns collection with the elements found in the BST. Both borders are inclusive. - implemented with "yield"
    /// </summary>
    /// <param name="startRange">Start range (inclusive)</param>
    /// <param name="endRange">End range (inclusive)</param>
    /// <returns>new BST in the given range</returns>
    public IEnumerable<T> RangeWithYield(T startRange, T endRange)
    {
        return this.RangeWithYield(this.Root, startRange, endRange);
    }

    /// <summary>
    /// Private method for the "RangeWithYield" method
    /// </summary>
    private IEnumerable<T> RangeWithYield(Node<T> node, T startRange, T endRange)
    {
        if (node == null)
        {
            yield break;
        }

        int compareStartrangeToValue = startRange.CompareTo(node.Value);
        int compareEndrangeToValue = endRange.CompareTo(node.Value);

        if (compareStartrangeToValue < 0)
        {
            foreach (T element in RangeWithYield(node.LeftChild, startRange, endRange))
            {
                yield return element;
            }
        }

        if (compareStartrangeToValue <= 0 && compareEndrangeToValue >= 0)
        {
            yield return node.Value;
        }

        if (compareEndrangeToValue > 0)
        {
            foreach (T element in RangeWithYield(node.RightChild, startRange, endRange))
            {
                yield return element;
            }
        }

        yield break;
    }

    /// <summary>
    /// Implement a method which returns the count of elements in the BST. 
    /// </summary>
    /// <returns>Count of elements in given tree</returns>
    public int Count()
    {
        List<T> allElements = new List<T>();
        this.EachInOrder(allElements.Add);
        return allElements.Count;
    }

    /// <summary>
    /// Implement a method which deletes the max element in a BST (Binary Search Tree). If the tree is empty it should throw exception. 
    /// </summary>
    public void DeleteMax()
    {
        if (this.Root == null)
        {
            throw new InvalidOperationException("Tree is empty");
        }

        Node<T> maximumNode = this.Root;
        Node<T> parentNode = null;

        while (maximumNode.RightChild != null)
        {
            parentNode = maximumNode;
            maximumNode = maximumNode.RightChild;
        }

        if (parentNode == null)
        {
            this.Root = maximumNode.LeftChild;
        }
        else
        {
            parentNode.RightChild = maximumNode.LeftChild;
        }
    }

    /// <summary>
    /// Implement a method which returns the count of elements smaller than a given value. 
    /// </summary>
    /// <param name="element"> count smaller elements before it </param>
    /// <returns> the count of elements smaller than "element" </returns>
    public int Rank(T element)
    {
        List<T> allElements = new List<T>();
        this.EachInOrder(allElements.Add);
        int smallerElementsCount = 0;

        foreach (T item in allElements)
        {
            if (item.CompareTo(element) < 0)
            {
                smallerElementsCount++;
            }
            else
            {
                break;
            }
        }

        return smallerElementsCount;
    }

    /// <summary>
    /// Implement a method which accepts a number (n) and returns the first element which has exactly n elements smaller than it. 
    /// </summary>
    /// <param name="smallerElementsCount"> the number of smaller elements count</param>
    /// <returns> The first element with "smallerElementsCount" smaller elements before it </returns>
    public T Select(int smallerElementsCount)
    {
        if (this.Root == null)
        {
            throw new InvalidOperationException("Tree is empty");
        }

        List<T> allElements = new List<T>();
        this.EachInOrder(allElements.Add);
        if (smallerElementsCount >= allElements.Count)
        {
            throw new InvalidOperationException("The given count is equal or larger than the number of elements");
        }
        else
        {
            return allElements[smallerElementsCount];
        }
    }

    /// <summary>
    /// Implement a method which finds (returns) the nearest smaller value than given in the BST
    /// </summary>
    /// <param name="element"> Searches for the nearest value before the given element </param>
    /// <returns></returns>
    public T Floor(T element)
    {
        if (this.Root == null)
        {
            throw new InvalidOperationException("Tree is empty");
        }

        List<T> allElements = new List<T>();
        this.EachInOrder(allElements.Add);

        T currentElement = default(T);

        for (int i = 0; i < allElements.Count; i++)
        {
            if (allElements[i].CompareTo(element) >= 0)
            {
                break;
            }

            currentElement = allElements[i];
        }

        return currentElement;
    }

    /// <summary>
    /// Implement a method which finds (returns) the nearest larger value than given in the BST. 
    /// </summary>
    /// <param name="element">Searches for the nearest value after the given element</param>
    /// <returns></returns>
    public T Ceiling(T element)
    {
        if (this.Root == null)
        {
            throw new InvalidOperationException("Tree is empty");
        }

        List<T> allElements = new List<T>();
        this.EachInOrder(allElements.Add);

        for (int i = 0; i < allElements.Count; i++)
        {
            if (allElements[i].CompareTo(element) > 0)
            {
                return allElements[i];
            }
        }

        throw new InvalidOperationException("No item in BST is larger than given value");
    }

    /// <summary>
    /// Returns the node in the three with a given value, return null if not found
    /// </summary>
    /// <param name="element">Value of seeked node</param>
    /// <returns>node with the given value</returns>
    public Node<T> Find(T element)
    {
        if (this.Root == null)
        {
            throw new InvalidOperationException("Tree is empty");
        }

        Node<T> currentNode = this.Root;
        Node<T> parentNode = null;

        while (currentNode.Value.CompareTo(element) != 0)
        {
            parentNode = currentNode;

            if (currentNode.Value.CompareTo(element) > 0)
            {
                currentNode = currentNode.LeftChild;
            }
            else
            {
                currentNode = currentNode.RightChild;
            }

            if (currentNode == null)
            {
                break;
            }
        }

        return currentNode;
    }

    /// <summary>
    /// Returns the parent node in the three with a given value, return null if child nose is root,
    /// throws argument exception if not found
    /// </summary>
    /// <param name="element">value of element whose parent node is seeked</param>
    /// <returns>Parent of node with input value</returns>
    public Node<T> FindParent(T element)
    {
        if (this.Root == null)
        {
            throw new InvalidOperationException("Tree is empty");
        }

        Node<T> currentNode = this.Root;
        Node<T> parentNode = null;

        while (currentNode.Value.CompareTo(element) != 0)
        {
            parentNode = currentNode;

            if (currentNode.Value.CompareTo(element) > 0)
            {
                currentNode = currentNode.LeftChild;
            }
            else
            {
                currentNode = currentNode.RightChild;
            }

            if (currentNode == null)
            {
                throw new ArgumentException("Node with a given value does not exist in the current Binary Search Tree");
            }
        }

        return parentNode;
    }

    /// <summary>
    /// Deletes a node with given value.
    /// </summary>
    /// <param name="element">Value of node to be deleted from the BST</param>
    public void Delete(T element)
    {
        Node<T> ParentNode = this.FindParent(element);
        // if ParentNode == null, node to be deleted is root
        if (ParentNode == null)
        {
            if (this.Root.LeftChild == null && this.Root.RightChild == null)
            {
                this.Root = null;
            }
            else if (this.Root.LeftChild != null && this.Root.RightChild == null)
            {
                this.Root = this.Root.LeftChild;
            }
            else if (this.Root.LeftChild == null && this.Root.RightChild != null)
            {
                this.Root = this.Root.RightChild;
            }
            else
            {
                // finds the minimum value in the right subtree
                BinarySearchTree<T> subtreeWithRootToBeDeleted = this.Search(this.Root.RightChild.Value);
                // Keeps the minimum value in the right subtree, successor to the root
                T minimumValueInTheSubtree = subtreeWithRootToBeDeleted.Select(0);
                // Deletes the successor
                this.Delete(minimumValueInTheSubtree);
                // Replaces the root with successor value
                this.Root.Value = minimumValueInTheSubtree;
            }

            return;
        }

        //node to be deleted is left child
        if (ParentNode.LeftChild.Value.CompareTo(element) == 0)
        {
            Node<T> nodeForDeletion = ParentNode.LeftChild;

            if (nodeForDeletion.LeftChild == null && nodeForDeletion.RightChild == null)
            {
                ParentNode.LeftChild = null;
            }
            else if (nodeForDeletion.LeftChild != null && nodeForDeletion.RightChild == null)
            {
                ParentNode.LeftChild = nodeForDeletion.LeftChild;
            }
            else if (nodeForDeletion.LeftChild == null && nodeForDeletion.RightChild != null)
            {
                ParentNode.LeftChild = nodeForDeletion.RightChild;
            }
            else
            {
                BinarySearchTree<T> subtreeWithRootToBeDeleted = this.Search(nodeForDeletion.Value);
                T minimumValueInTheSubtree = subtreeWithRootToBeDeleted.Select(0);
                this.Delete(minimumValueInTheSubtree);
                nodeForDeletion.Value = minimumValueInTheSubtree;
            }
        }

        // node to be deleted is right child
        else
        {
            Node<T> nodeForDeletion = ParentNode.RightChild;

            if (nodeForDeletion.LeftChild == null && nodeForDeletion.RightChild == null)
            {
                ParentNode.RightChild = null;
            }
            else if (nodeForDeletion.LeftChild != null && nodeForDeletion.RightChild == null)
            {
                ParentNode.RightChild = nodeForDeletion.LeftChild;
            }
            else if (nodeForDeletion.LeftChild == null && nodeForDeletion.RightChild != null)
            {
                ParentNode.RightChild = nodeForDeletion.RightChild;
            }
            else
            {
                BinarySearchTree<T> subtreeWithRootToBeDeleted = this.Search(nodeForDeletion.Value);
                T minimumValueInTheSubtree = subtreeWithRootToBeDeleted.Select(0);
                this.Delete(minimumValueInTheSubtree);
                nodeForDeletion.Value = minimumValueInTheSubtree;
            }
        }

    }
}



public class Launcher
{
    public static void Main()
    {
        BinarySearchTree<int> bst = new BinarySearchTree<int>();
        bst.Insert(2);
        bst.Insert(1);
        bst.Insert(4);
        bst.Insert(3);

        bst.Insert(50);
        bst.Insert(47);
        bst.Insert(49);
        bst.Insert(48);
        bst.Insert(46);

        Console.WriteLine("---------");
        Console.Write("bst count: ");
        Console.WriteLine(bst.Count());
        Console.WriteLine("---------");


//        bst.EachInOrder(Console.WriteLine);
        Console.WriteLine("---------");
        Console.WriteLine(bst.Floor(1));



    }
}
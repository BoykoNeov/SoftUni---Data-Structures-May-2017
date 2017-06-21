using System;
using System.Collections.Generic;

public class IntervalTree
{
    private class Node
    {
        internal Interval interval;
        internal double max;
        internal Node right;
        internal Node left;

        public Node(Interval interval)
        {
            this.interval = interval;
            this.max = interval.Hi;
        }
    }

    private Node root;

    public void Insert(double lo, double hi)
    {
        this.root = this.Insert(this.root, lo, hi);
    }

    public void EachInOrder(Action<Interval> action)
    {
        EachInOrder(this.root, action);
    }

    public Interval SearchAny(double lo, double hi)
    {
        Node current = this.root;

        while (current != null && !current.interval.Intersects(lo, hi))
        {
            if (current.left != null && current.left.max > lo)
            {
                current = current.left;
            }
            else
            {
                current = current.right;
            }
        }

        if (current == null)
        {
            return null;
        }

        return current.interval;
    }

    public IEnumerable<Interval> SearchAll(double lo, double hi)
    {
        List<Interval> results = new List<Interval>();
        SearchAll(this.root, lo, hi, results);
        return results;
    }

    private void SearchAll(Node node, double lo, double hi, List<Interval> results)
    {
        if (node == null)
        {
            return;
        }

        if (node.left != null && node.left.max > lo)
        {
            SearchAll(node.left, lo, hi, results);
        }

        if (node.interval.Intersects(lo, hi))
        {
            results.Add(node.interval);
        }

        if (node.right != null && node.right.interval.Lo < hi)
        {
            SearchAll(node.right, lo, hi, results);
        }
    }

    private void EachInOrder(Node node, Action<Interval> action)
    {
        if (node == null)
        {
            return;
        }

        EachInOrder(node.left, action);
        action(node.interval);
        EachInOrder(node.right, action);
    }

    private Node Insert(Node node, double lo, double hi)
    {
        if (node == null)
        {
            return new Node(new Interval(lo, hi));
        }

        int cmp = lo.CompareTo(node.interval.Lo);
        if (cmp < 0)
        {
            node.left = Insert(node.left, lo, hi);
        }
        else if (cmp > 0)
        {
            node.right = Insert(node.right, lo, hi);
        }

        UpdateMax(node);
        return node;
    }

    private void UpdateMax(Node node)
    {
        Node maxChild = GetMax(node.left, node.right);
        node.max = GetMax(maxChild, node).max;
    }

    private Node GetMax(Node left, Node right)
    {
        if (left == null)
        {
            return right;
        }

        if (right == null)
        {
            return left;
        }

        return (left.max > right.max) ? left : right;
    }
}
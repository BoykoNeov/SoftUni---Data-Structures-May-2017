/// <summary>
/// Write a program to read the tree and find all middle nodes (in increasing order):
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;

internal class MiddleNodes
{
    internal static Dictionary<int, Tree<int>> nodeByValue = new Dictionary<int, Tree<int>>();

    internal static void Main()
    {
        ReadTree();
        List<int> nodes = nodeByValue.Values
             .Where(x => x.Parent != null && x.Children.Count != 0)
             .Select(x => x.Value)
             .OrderBy(x => x)
             .ToList();

        Console.Write($"Middle nodes: {string.Join(" ", nodes)}");
    }

    internal static Tree<int> GetTreeNodeByValue(int value)
    {
        if (!nodeByValue.ContainsKey(value))
        {
            nodeByValue[value] = new Tree<int>(value);
        }

        return nodeByValue[value];
    }

    internal static void AddEdge(int parent, int child)
    {
        Tree<int> parentNode = GetTreeNodeByValue(parent);
        Tree<int> childNode = GetTreeNodeByValue(child);

        parentNode.Children.Add(childNode);
        childNode.Parent = parentNode;
    }

    internal static void ReadTree()
    {
        int nodeCount = int.Parse(Console.ReadLine());
        for (int i = 1; i < nodeCount; i++)
        {
            string[] edge = Console.ReadLine().Split();
            AddEdge(int.Parse(edge[0]), int.Parse(edge[1]));
        }
    }

    internal static Tree<int> GetRootNode()
    {
        return nodeByValue.Values.FirstOrDefault(x => x.Parent == null);
    }
}